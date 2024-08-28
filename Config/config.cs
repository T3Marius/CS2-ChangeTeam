using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;
using System.Text.Json.Serialization;
public class PluginConfig : BasePluginConfig
{
    [JsonPropertyName("Tag")] public string Tag { get; set; } = "{red}[SWAP]{default} ";

    [JsonPropertyName("Permissions")] public List<string> Permissions { get; set; } = new List<string>();
    public Config_Commands_Player CommandsPlayer { get; set; } = new Config_Commands_Player();
    public Config_Commands_Admin CommandsAdmin { get; set; } = new Config_Commands_Admin();

    public class Config_Commands_Player
    {
        [JsonPropertyName("Terrorist")]
        public string[] Terrorist { get; set; } = { "t", "terrorist" };

        [JsonPropertyName("Spectator")]
        public string[] Spectate { get; set; } = { "afk", "spectate" };
    }
    public class Config_Commands_Admin
    {
        [JsonPropertyName("Terrorist")]
        public string[] Terrorist { get; set; } = { "movet", "moveterrorist" };

        [JsonPropertyName("CounterTerrorist")]
        public string[] CounterTerrorist { get; set; } = { "movect", "movecounterterrorist" };

        [JsonPropertyName("Spectator")]
        public string[] Spectate { get; set; } = { "spec", "movespectate" };

        [JsonPropertyName("Swapper")]
        public string[] Swapper { get; set; } = { "swap" };
    }
}