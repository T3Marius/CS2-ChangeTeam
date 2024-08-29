using CounterStrikeSharp.API.Core.Translations;
using CounterStrikeSharp.API;
using System.Reflection;
using Tomlyn.Model;
using Tomlyn;

namespace ChangeTeams
{
    public static class Config_Config
    {
        public static Cfg Config { get; set; } = new Cfg();

        public static void Load()
        {
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name ?? "";
            string cfgPath = $"{Server.GameDirectory}/csgo/addons/counterstrikesharp/configs/plugins/{assemblyName}";

            LoadConfig($"{cfgPath}/config.toml");
        }

        private static void LoadConfig(string configPath)
        {
            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException($"Configuration file not found: {configPath}");
            }

            string configText = File.ReadAllText(configPath);
            TomlTable model = Toml.ToModel(configText);

            TomlTable tagTable = (TomlTable)model["Tag"];
            string config_tag = StringExtensions.ReplaceColorTags(tagTable["Tag"].ToString()!);

            TomlTable permissionsTable = (TomlTable)model["Permissions"];
            List<string> permissionsList = new();
            foreach (var permission in (TomlArray)permissionsTable["FLAGS"])
            {
                permissionsList.Add(permission!.ToString()!);
            }

            TomlTable commandsTable = (TomlTable)model["CommandsPlayer"];
            Config_Commands_Player config_playercommands = new()
            {
                Terrorist = GetTomlArray(commandsTable, "Terrorist"),
                CounterTerrorist = GetTomlArray(commandsTable, "CounterTerrorist"),
                Spectate = GetTomlArray(commandsTable, "Spectate")
            };

            TomlTable admincommandsTable = (TomlTable)model["CommandsAdmin"];
            Config_Commands_Admin config_admincommands = new()
            {
                MoveTerrorist = GetTomlArray(admincommandsTable, "MoveTerrorist"),
                MoveCounterTerrorist = GetTomlArray(admincommandsTable, "MoveCounterTerrorist"),
                MoveSpectate = GetTomlArray(admincommandsTable, "MoveSpectate"),
                Swapper = GetTomlArray(admincommandsTable, "Swapper")
            };

            Config = new Cfg
            {
                Tag = config_tag,
                Permissions = permissionsList,
                CommandsAdmin = config_admincommands,
                CommandsPlayer = config_playercommands
            };
        }

        private static string[] GetTomlArray(TomlTable table, string key)
        {
            if (table.TryGetValue(key, out var value) && value is TomlArray array)
            {
                return array.OfType<string>().ToArray();
            }
            return Array.Empty<string>();
        }

        public class Cfg
        {
            public string Tag { get; set; } = string.Empty;
            public List<string> Permissions { get; set; } = new();
            public Config_Commands_Player CommandsPlayer { get; set; } = new();
            public Config_Commands_Admin CommandsAdmin { get; set; } = new();
        }

        public class Config_Commands_Player
        {
            public string[] Terrorist { get; set; } = Array.Empty<string>();
            public string[] Spectate { get; set; } = Array.Empty<string>();
            public string[] CounterTerrorist { get; set; } = Array.Empty<string>();
        }

        public class Config_Commands_Admin
        {
            public string[] MoveTerrorist { get; set; } = Array.Empty<string>();
            public string[] MoveCounterTerrorist { get; set; } = Array.Empty<string>();
            public string[] MoveSpectate { get; set; } = Array.Empty<string>();
            public string[] Swapper { get; set; } = Array.Empty<string>();
        }
    }
}
