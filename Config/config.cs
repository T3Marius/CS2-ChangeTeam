using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ChangeTeams;

public class TeamsConfig : BasePluginConfig
{
    [JsonPropertyName("ChatTag")] public string Tag { get; set; } = "[{green}SWAP{default}]";
    [JsonPropertyName("Commands")] public Config_Command Commands { get; set; } = new Config_Command();
    [JsonPropertyName("ForceTeamEnabled")] public bool ForceTeamEnable { get; set; } = false;
}

    public class Config_Command
    {
        //players commands
        public string[] Terrorist { get; set; } = ["terroist", "t"];
        public string[] CounterTerrorist { get; set; } = ["ct", "counterterrorist"];
        public string[] Spectate { get; set; } = ["spec", "spectate", "s"];

    [JsonPropertyName("Admins Commands")]
        public string[] T { get; set; } = ["!t {Name}"];
        public string[] CT { get; set; } = ["!ct {Name}"];
        public string[] Spec { get; set; } = ["!spec {Name}"];
    }
