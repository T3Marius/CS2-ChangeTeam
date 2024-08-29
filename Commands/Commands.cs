using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;
using CounterStrikeSharp.API;
using static ChangeTeams.Config_Config;
using static ChangeTeams.ChangeTeams;

namespace ChangeTeams
{
    public static class Commands
    {
        public static void Load()
        {
            Config_Config.Load();

            var adminConfig = Config_Config.Config.CommandsAdmin;
            var playerConfig = Config_Config.Config.CommandsPlayer;

            var commands = new Dictionary<IEnumerable<string>, (string description, CommandInfo.CommandCallback handler)>
            {
                { adminConfig.MoveTerrorist, ("AdminTcommand", Command_MoveTerrorist) },
                { adminConfig.MoveCounterTerrorist, ("AdminCTCommand", Command_MoveCounterTerrorist) },
                { adminConfig.MoveSpectate, ("AdminSpecCommand", Command_MoveSpectate) },
                { adminConfig.Swapper, ("AdminSwapCommand", Command_Swap) },
                { playerConfig.Terrorist, ("PlayerTcommand", Command_Terrorist) },
                { playerConfig.Spectate, ("PlayerSpecCommand", Command_Afk) }
            };

            foreach (var commandPair in commands)
            {
                foreach (var command in commandPair.Key)
                {
                    Instance.AddCommand($"css_{command}", commandPair.Value.description, commandPair.Value.handler);
                }
            }
        }

        [CommandHelper(minArgs: 0, whoCanExecute: CommandUsage.CLIENT_ONLY)]
        public static void Command_Terrorist(CCSPlayerController? player, CommandInfo command)
        {
            if (player == null) return;

            player.ChangeTeam(CsTeam.Terrorist);
            player.PrintToChat(Config.Tag + Instance.Localizer["player.terrorist"]);
        }

        [CommandHelper(minArgs: 0, whoCanExecute: CommandUsage.CLIENT_ONLY)]
        public static void Command_Afk(CCSPlayerController? player, CommandInfo command)
        {
            if (player == null) return;

            player.ChangeTeam(CsTeam.Spectator);
            player.PrintToChat(Config.Tag + Instance.Localizer["player.afk"]);
        }

        [CommandHelper(minArgs: 1, "<name or #userid>", whoCanExecute: CommandUsage.CLIENT_AND_SERVER)]
        public static void Command_MoveTerrorist(CCSPlayerController? caller, CommandInfo command)
        {
            if (caller == null || !HasPermission(caller)) return;

            var target = FindPlayer(command.ArgByIndex(1), caller);
            if (target == null || !target.IsValid || target.Connected != PlayerConnectedState.PlayerConnected)
            {
                return;
            }

            target.ChangeTeam(CsTeam.Terrorist);
            target.PrintToChat(string.Format(Config.Tag + Instance.Localizer["admin.terrorist"], caller.PlayerName));
            Server.PrintToChatAll(string.Format(Config.Tag + Instance.Localizer["player.tmove.announcement"], target.PlayerName, caller.PlayerName));
        }

        [CommandHelper(minArgs: 1, "<name or #userid>", whoCanExecute: CommandUsage.CLIENT_AND_SERVER)]
        public static void Command_MoveCounterTerrorist(CCSPlayerController? caller, CommandInfo command)
        {
            if (caller == null || !HasPermission(caller)) return;

            var target = FindPlayer(command.ArgByIndex(1), caller);
            if (target == null || !target.IsValid || target.Connected != PlayerConnectedState.PlayerConnected)
            {
                return;
            }

            target.ChangeTeam(CsTeam.CounterTerrorist);
            target.PrintToChat(string.Format(Config.Tag + Instance.Localizer["admin.counter_terrorist"], caller.PlayerName));
            Server.PrintToChatAll(string.Format(Config.Tag + Instance.Localizer["player.ctmove.announcement"], target.PlayerName, caller.PlayerName));
        }

        [CommandHelper(minArgs: 1, "<name or #userid>", whoCanExecute: CommandUsage.CLIENT_AND_SERVER)]
        public static void Command_MoveSpectate(CCSPlayerController? caller, CommandInfo command)
        {
            if (caller == null || !HasPermission(caller)) return;

            var target = FindPlayer(command.ArgByIndex(1), caller);
            if (target == null || !target.IsValid || target.Connected != PlayerConnectedState.PlayerConnected)
            {
                return;
            }

            target.ChangeTeam(CsTeam.Spectator);
            target.PrintToChat(string.Format(Config.Tag + Instance.Localizer["admin.spectator"], caller.PlayerName));
            Server.PrintToChatAll(string.Format(Config.Tag + Instance.Localizer["player.spec_move.announcement"], target.PlayerName, caller.PlayerName));
        }

        [CommandHelper(minArgs: 1, "<name or #userid>", whoCanExecute: CommandUsage.CLIENT_AND_SERVER)]
        public static void Command_Swap(CCSPlayerController? caller, CommandInfo command)
        {
            if (caller == null || !HasPermission(caller)) return;

            var target = FindPlayer(command.ArgByIndex(1), caller);
            if (target == null || !target.IsValid || target.Connected != PlayerConnectedState.PlayerConnected)
            {
                return;
            }

            Swap(caller, target);
        }

        private static bool HasPermission(CCSPlayerController player)
        {
            return Config.Permissions.Count == 0 || Config.Permissions.Any(permission => AdminManager.PlayerHasPermissions(player, permission));
        }

        private static CCSPlayerController? FindPlayer(string identifier, CCSPlayerController? caller)
        {
            if (identifier == "@me" && caller != null)
            {
                return caller;
            }
            var matchingPlayers = Utilities.GetPlayers()
                .Where(p => p.PlayerName.Contains(identifier, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (matchingPlayers.Count == 1)
            {
                return matchingPlayers.First();
            }
            if (matchingPlayers.Count > 1)
            {
                caller?.PrintToChat("Multiple players found with that name. Please refine your search.");
            }
            else
            {
                caller?.PrintToChat("No player found with that name.");
            }

            return null;
        }

        private static void Swap(CCSPlayerController player, CCSPlayerController target)
        {
            if (target.Team == CsTeam.Terrorist)
            {
                target.ChangeTeam(CsTeam.CounterTerrorist);
                target.PrintToChat(string.Format(Config.Tag + Instance.Localizer["admin.counter_terrorist"], player.PlayerName));
                Server.PrintToChatAll(string.Format(Config.Tag + Instance.Localizer["player.ctmove.announcement"], target.PlayerName, player.PlayerName));
            }
            else if (target.Team == CsTeam.CounterTerrorist)
            {
                target.ChangeTeam(CsTeam.Terrorist);
                target.PrintToChat(string.Format(Config.Tag + Instance.Localizer["admin.terrorist"], player.PlayerName));
                Server.PrintToChatAll(string.Format(Config.Tag + Instance.Localizer["player.tmove.announcement"], target.PlayerName, player.PlayerName));
            }
        }
    }
}
