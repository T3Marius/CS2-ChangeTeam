using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Commands.Targeting;
using CounterStrikeSharp.API.Modules.Entities;
using CounterStrikeSharp.API.Modules.Utils;
using static ChangeTeams.ChangeTeams;
using static ChangeTeams.PlayerExtensions;

namespace ChangeTeams;
public static class Command
{
    public static void Load()
    {
        TeamsConfig config = Instance.Config;

        Dictionary<IEnumerable<string>, (string description, CommandInfo.CommandCallback handler)> commands = new()
        {
            {config.Commands.Terrorist, ("Moves you to terrorist", Command_Terrorist)},
            {config.Commands.CounterTerrorist, ("Moves you to counterterrorist", Command_CounterTerrorist)},
            {config.Commands.Spectate, ("Moves you to spectate", Command_Spectate)},
            //{config.Commands.T, ("Move the player to terrorist", Command_T) },
            //{config.Commands.CT, ("Move the player to counterterroist", Command_CT)},
            //{config.Commands.Spec, ("Move the player to spectate", Command_Spec) }
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
        if (player != null)
        {
            player.ChangeTeam(CsTeam.Terrorist);
            player.PrintToChat($"{Instance.Config.Tag} {ChatColors.Orange}You have been moved to {ChatColors.Gold}Terrorist!");
        }
    }
    public static void Command_CounterTerrorist(CCSPlayerController? player, CommandInfo command)
    {
        if (player != null)
        {
            player.ChangeTeam(CsTeam.CounterTerrorist);
            player.PrintToChat($"{Instance.Config.Tag} {ChatColors.Orange}You have been moved to {ChatColors.Blue}Counter-Terrorist!");
        }
    }
    public static void Command_Spectate(CCSPlayerController? player, CommandInfo command)
    {
        if (player != null)
        {
            player.ChangeTeam(CsTeam.Spectator);
            player.PrintToChat($"{Instance.Config.Tag} {ChatColors.Orange}You have been moved to {ChatColors.Purple}Spectate!");
        }
    }
    /*
    [RequiresPermissions("@css/kick")]
    [CommandHelper(minArgs: 2, usage: "<#userid or name> [<movet>]", whoCanExecute: CommandUsage.CLIENT_AND_SERVER)]
    public static void Command_T(CCSPlayerController? player, CommandInfo command)
    {
        if (player != null)
        {
            var playerName = player == null ? "Console" : player.PlayerName;
            var teamName = command.GetArg(2).ToLower();
            var _teamName = "T";
            var teamNum = CsTeam.Terrorist;
            var targets = GetTarget(command);
            if (targets == null) return;

            var playersToTarget = targets.Players.Where(player => player is { IsValid: true, IsHLTV: false }).ToList();

            switch (teamName)
            {
                case "t":
                case "terrorist":
                    teamNum = CsTeam.Terrorist;
                    _teamName = "T";
                    break;
            }
        }
    }
    [RequiresPermissions("@css/kick")]
    [CommandHelper(minArgs: 2, usage: "<#userid or name> [<movect>]", whoCanExecute: CommandUsage.CLIENT_AND_SERVER)]
    public static void Command_CT(CCSPlayerController? player, CommandInfo command)
    {
        if (player != null)
        {
            var playerName = player == null ? "Console" : player.PlayerName;
            var teamName = command.GetArg(2).ToLower();
            var _teamName = "CT";
            var teamNum = CsTeam.CounterTerrorist;

            var targets = GetTarget(command);
            if (targets == null) return;

            var playersToTarget = targets.Players.Where(player => player is { IsValid: true, IsHLTV: false }).ToList();

            switch (teamName)
            {
                case "ct":
                case "counterterrorist":
                    teamNum = CsTeam.CounterTerrorist;
                    _teamName = "CT";
                    break;
            }
        }
    }
    [RequiresPermissions("@css/kick")]
    [CommandHelper(minArgs: 2, usage: "<#userid or name> [<movespec>]", whoCanExecute: CommandUsage.CLIENT_AND_SERVER)]
    public static void Command_Spec(CCSPlayerController? player, CommandInfo command)
    {
        if (player != null)
        {
            var playerName = player == null ? "Console" : player.PlayerName;
            var teamName = command.GetArg(2).ToLower();
            var _teamName = "spec";
            var teamNum = CsTeam.Spectator;

            var targets = GetTarget(command);
            if (targets == null) return;

            var playersToTarget = targets.Players.Where(player => player is { IsValid: true, IsHLTV: false }).ToList();

            switch (teamName)
            {
                case "spec":
                case "spectate":
                    teamNum = CsTeam.Spectator;
                    _teamName = "Spec";
                    break;
            }
        }
    }
    */
}