using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core.Translations;

namespace ChangeTeams
{
    public class ChangeTeams : BasePlugin, IPluginConfig<PluginConfig>
    {
        public override string ModuleAuthor => "Marius";
        public override string ModuleName => "ChangeTeams";
        public override string ModuleVersion => "0.0.4";
        public PluginConfig Config { get; set; } = new PluginConfig();
        public void OnConfigParsed(PluginConfig config)
        {
            config.Tag = StringExtensions.ReplaceColorTags(config.Tag);
            Config = config;
        }
        public static ChangeTeams Instance { get; set; } = new();

        public Random Random = new Random();
        public override void Load(bool hotReload)
        {
            Console.WriteLine("░█████╗░██╗░░██╗░█████╗░███╗░░██╗░██████╗░███████╗████████╗███████╗░█████╗░███╗░░░███╗");
            Console.WriteLine("██╔══██╗██║░░██║██╔══██╗████╗░██║██╔════╝░██╔════╝╚══██╔══╝██╔════╝██╔══██╗████╗░████║");
            Console.WriteLine("██║░░╚═╝███████║███████║██╔██╗██║██║░░██╗░█████╗░░░░░██║░░░█████╗░░███████║██╔████╔██║");
            Console.WriteLine("██║░░██╗██╔══██║██╔══██║██║╚████║██║░░╚██╗██╔══╝░░░░░██║░░░██╔══╝░░██╔══██║██║╚██╔╝██║");
            Console.WriteLine("╚█████╔╝██║░░██║██║░░██║██║░╚███║╚██████╔╝███████╗░░░██║░░░███████╗██║░░██║██║░╚═╝░██║");
            Console.WriteLine("╚════╝░╚═╝░░╚═╝╚═╝░░╚═╝╚═╝░░╚══╝░╚═════╝░╚══════╝░░░╚═╝░░░╚══════╝╚═╝░░╚═╝╚═╝░░░░░╚═╝ ");
            Instance = this;
            Command.Load();
        }
        public static class Command
        {
            public static void Load()
            {
                PluginConfig Config = Instance.Config;
                Dictionary<IEnumerable<string>, (string description, CommandInfo.CommandCallback handler)> commands = new()
                {
                    { Config.CommandsPlayer.Terrorist, ("Moves you to terrorist", Command_Terrorist) },
                    { Config.CommandsPlayer.Spectate, ("Moves you to spectate", Command_Afk) },
                    { Config.CommandsAdmin.Terrorist, ("Moves you to terrorist", Command_MoveTerrorist) },
                    { Config.CommandsAdmin.CounterTerrorist, ("Moves you to counterterrorist", Command_MoveCounterTerrorist) },
                    { Config.CommandsAdmin.Spectate, ("Moves you to spectate", Command_MoveSpectate) },
                    { Config.CommandsAdmin.Swapper, ("Swaps the player's team", Command_Swap) },
                };

                foreach (KeyValuePair<IEnumerable<string>, (string description, CommandInfo.CommandCallback handler)> commandPair in commands)
                {
                    foreach (string command in commandPair.Key)
                    {
                        Instance.AddCommand($"css_{command}", commandPair.Value.description, commandPair.Value.handler);
                    }
                }
            }

            public static void Command_Terrorist(CCSPlayerController? player, CommandInfo command)
            {
                var instance = ChangeTeams.Instance;
                var Config = instance.Config;
                if (player != null)
                {
                    player.ChangeTeam(CsTeam.Terrorist);
                    player.PrintToChat(Config.Tag + instance.Localizer["player.terrorist"]);
                }
            }

            public static void Command_Afk(CCSPlayerController? player, CommandInfo command)
            {
                var instance = ChangeTeams.Instance;
                var Config = instance.Config;
                if (player != null)
                {
                    player.ChangeTeam(CsTeam.Spectator);
                    player.PrintToChat(Config.Tag + instance.Localizer["player.afk"]);
                }
            }

            public static void Command_MoveTerrorist(CCSPlayerController? player, CommandInfo command)
            {
                var instance = ChangeTeams.Instance;
                var Config = instance.Config;
                if (player != null && command.ArgCount >= 1 && Config.Permissions.Any(permission => AdminManager.PlayerHasPermissions(player, permission)))
                {
                    foreach (var find_player in Utilities.GetPlayers())
                    {
                        if (find_player.PlayerName == command.ArgByIndex(1) || find_player.SteamID.ToString() == command.ArgByIndex(1))
                        {
                            find_player.ChangeTeam(CsTeam.Terrorist);
                            find_player.PrintToChat(string.Format(Config.Tag + instance.Localizer["admin.terrorist"], player.PlayerName));
                            Server.PrintToChatAll(string.Format(Config.Tag + instance.Localizer["player.tmove.announcement"], find_player.PlayerName, player.PlayerName));
                        }
                    }
                }
            }

            public static void Command_MoveCounterTerrorist(CCSPlayerController? player, CommandInfo command)
            {
                var instance = ChangeTeams.Instance;
                var Config = instance.Config;
                if (player != null && command.ArgCount >= 1 && Config.Permissions.Any(permission => AdminManager.PlayerHasPermissions(player, permission)))
                {
                    foreach (var find_player in Utilities.GetPlayers())
                    {
                        if (find_player.PlayerName == command.ArgByIndex(1) || find_player.SteamID.ToString() == command.ArgByIndex(1))
                        {
                            find_player.ChangeTeam(CsTeam.CounterTerrorist);
                            find_player.PrintToChat(string.Format(Config.Tag + instance.Localizer["admin.counter_terrorist"], player.PlayerName));
                            Server.PrintToChatAll(string.Format(Config.Tag + instance.Localizer["player.ctmove.announcement"], find_player.PlayerName, player.PlayerName));
                        }
                    }
                }
            }

            public static void Command_MoveSpectate(CCSPlayerController? player, CommandInfo command)
            {
                var instance = ChangeTeams.Instance;
                var Config = instance.Config;
                if (player != null && command.ArgCount >= 1 && Config.Permissions.Any(permission => AdminManager.PlayerHasPermissions(player, permission)))
                {
                    foreach (var find_player in Utilities.GetPlayers())
                    {
                        if (find_player.PlayerName == command.ArgByIndex(1) || find_player.SteamID.ToString() == command.ArgByIndex(1))
                        {
                            find_player.ChangeTeam(CsTeam.Spectator);
                            find_player.PrintToChat(string.Format(Config.Tag + instance.Localizer["admin.spectator"], player.PlayerName));
                            Server.PrintToChatAll(string.Format(Config.Tag + instance.Localizer["player.spec_move.announcement"], find_player.PlayerName, player.PlayerName));
                        }
                    }
                }
            }

            public static void Command_Swap(CCSPlayerController? player, CommandInfo command)
            {
                var instance = ChangeTeams.Instance;
                var Config = instance.Config;
                if (player != null && command.ArgCount >= 1 && Config.Permissions.Any(permission => AdminManager.PlayerHasPermissions(player, permission)))
                {
                    foreach (var find_player in Utilities.GetPlayers())
                    {
                        if (find_player.PlayerName == command.ArgByIndex(1) || find_player.SteamID.ToString() == command.ArgByIndex(1))
                        {
                            string playerName = player.PlayerName;
                            string find_playerName = find_player.PlayerName;

                            if (find_player.Team == CsTeam.Terrorist)
                            {
                                find_player.ChangeTeam(CsTeam.CounterTerrorist);
                                find_player.PrintToChat(string.Format(Config.Tag + instance.Localizer["admin.counter_terrorist"], playerName));
                                Server.PrintToChatAll(string.Format(Config.Tag + instance.Localizer["player.ctmove.announcement"], find_playerName, playerName));
                            }
                            else if (find_player.Team == CsTeam.CounterTerrorist)
                            {
                                find_player.ChangeTeam(CsTeam.Terrorist);
                                find_player.PrintToChat(string.Format(Config.Tag + instance.Localizer["admin.terrorist"], playerName));
                                Server.PrintToChatAll(string.Format(Config.Tag + instance.Localizer["player.tmove.announcement"], find_playerName, playerName));
                            }
                        }
                    }
                }
            }
        }
    }
}
