using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Core.Capabilities;
using CounterStrikeSharp.API.Core.Translations;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Modules.Entities;
using CounterStrikeSharp.API.Modules.Commands.Targeting;

namespace ChangeTeams;

public class ChangeTeams : BasePlugin, IPluginConfig<TeamsConfig>
{
    public override string ModuleAuthor => "Marius";
    public override string ModuleName => "ChangeTeams";
    public override string ModuleVersion => "0.0.1";
    public TeamsConfig Config { get; set; } = new();
    public static ChangeTeams Instance { get; set; } = new();
    public override void Load(bool hotReload)
    {
        Instance = this;
        Command.Load();
    }
    public void OnConfigParsed(TeamsConfig config)
    {
       Config = config;
    }
    public static TargetResult GetTarget(CommandInfo command)
    {
        var identifier = command.GetArg(1); 
        var matchingPlayers = new List<CCSPlayerController>();  

        if (matchingPlayers.Count != 0)
        {
        }

        return new TargetResult { Players = matchingPlayers };
    }
}