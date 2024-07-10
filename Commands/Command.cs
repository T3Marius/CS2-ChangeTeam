using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
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
            command.ReplyToCommand(Instance.Config.Tag + Instance.Localizer["You have been moved to terrorist!"]);   
        }

        if (command.ArgCount >= 1 && AdminManager.PlayerHasPermissions(player, "@css/generic"))
        {
            foreach (var find_player in Utilities.GetPlayers())
            {
                if (find_player.PlayerName == command.ArgByIndex(1) || find_player.SteamID.ToString() == command.ArgByIndex(1))
                {
                    find_player.ChangeTeam(CsTeam.Terrorist);
                    break;
                }
            }
        }
    }
    public static void Command_CounterTerrorist(CCSPlayerController? player, CommandInfo command)
    {
        if (player != null)
        {
            player.ChangeTeam(CsTeam.CounterTerrorist);
            command.ReplyToCommand(Instance.Config.Tag + Instance.Localizer["You have been moved to counter-terrorist!"]);
        }
        if (command.ArgCount >= 1 && AdminManager.PlayerHasPermissions(player, "@css/generic"))
        {
            foreach (var find_player in Utilities.GetPlayers())
            {
                if (find_player.PlayerName == command.ArgByIndex(1) || find_player.SteamID.ToString() == command.ArgByIndex(1))
                {
                    find_player.ChangeTeam(CsTeam.CounterTerrorist);
                    break;
                }
            }
        }
    
    }  
    public static void Command_Spectate(CCSPlayerController? player, CommandInfo command)
    {
        if (player != null)
        {
            player.ChangeTeam(CsTeam.Spectator);
            command.ReplyToCommand(Instance.Config.Tag + Instance.Localizer["You have been moved to spectate!"]);
        }
        if (command.ArgCount >= 1 && AdminManager.PlayerHasPermissions(player, "@css/generic"))
        {
            foreach (var find_player in Utilities.GetPlayers())
            {
                if (find_player.PlayerName == command.ArgByIndex(1) || find_player.SteamID.ToString() == command.ArgByIndex(1))
                {
                    find_player.ChangeTeam(CsTeam.Spectator);
                    break;
                }
            }
        }
    }
}