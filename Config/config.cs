using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ChangeTeams;

public class TeamsConfig : BasePluginConfig
{
    [JsonPropertyName("ChatTag")] public string Tag { get; set; } = $"[{ChatColors.Green}SWAP{ChatColors.Default}]";
    [JsonPropertyName("Commands")] public Config_Command Commands { get; set; } = new Config_Command();

    public class Config_Command
    {
        //players commands
        public string[] Terrorist { get; set; } = ["terroist", "t"];
        public string[] CounterTerrorist { get; set; } = ["ct", "counterterrorist"];
        public string[] Spectate { get; set; } = ["s", "spectate"];

        //admins commands// don t change nothing here.
        // public string[] Spec { get; set; } = ["movespec"];
        //public string[] T { get; set; } = ["movet"];
        //public string[] CT { get; set; } = ["movect"];
    }
}
