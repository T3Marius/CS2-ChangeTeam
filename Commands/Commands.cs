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
            // Load the configuration
            Config_Config.Load();

            // Get configurations for admin and player commands
            var adminConfig = Config_Config.Config.CommandsAdmin;
            var playerConfig = Config_Config.Config.CommandsPlayer;

            // Register commands based on configuration
            var commands = new Dictionary<IEnumerable<string>, (string description, CommandInfo.CommandCallback handler)>
            {
                { adminConfig.MoveTerrorist, ("AdminTcommand", Command_MoveTerrorist) },
                { adminConfig.MoveCounterTerrorist, ("AdminCTCommand", Command_MoveCounterTerrorist) },
                { adminConfig.MoveSpectate, ("AdminSpecCommand", Command_MoveSpectate) },
                { adminConfig.Swapper, ("AdminSwapCommand", Command_Swap) },
                { playerConfig.Terrorist, ("PlayerTcommand", Command_Terrorist) },
                { playerConfig.CounterTerrorist, ("PlayerCTcommand", Command_CounterTerrorist) },
                { playerConfig.Spectate, ("PlayerSpecCommand", Command_Afk) }
            };

            // Add commands to the instance
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
        public static void Command_CounterTerrorist(CCSPlayerController? player, CommandInfo command)
        {
            if (player == null) return;

            player.ChangeTeam(CsTeam.CounterTerrorist);
            player.PrintToChat(Config.Tag + Instance.Localizer["player.counter_terrorist"]);
        }

        [CommandHelper(minArgs: 0, whoCanExecute: CommandUsage.CLIENT_ONLY)]
        public static void Command_Afk(CCSPlayerController? player, CommandInfo command)
        {
            if (player == null) return;

            player.ChangeTeam(CsTeam.Spectator);
            player.PrintToChat(Config.Tag + Instance.Localizer["player.afk"]);
        }

        [CommandHelper(minArgs: 1, "<name or #userid>", whoCanExecute: CommandUsage.CLIENT_AND_SERVER)]
        public static void Command_MoveTerrorist(CCSPlayerController? player, CommandInfo command)
        {
            if (player == null || !HasPermission(player)) return;

            var target = FindPlayer(command.ArgByIndex(1));
            if (target != null)
            {
                target.ChangeTeam(CsTeam.Terrorist);
                target.PrintToChat(string.Format(Config.Tag + Instance.Localizer["admin.terrorist"], player.PlayerName));
                Server.PrintToChatAll(string.Format(Config.Tag + Instance.Localizer["player.tmove.announcement"], target.PlayerName, player.PlayerName));
            }
        }

        [CommandHelper(minArgs: 1, "<name or #userid>", whoCanExecute: CommandUsage.CLIENT_AND_SERVER)]
        public static void Command_MoveCounterTerrorist(CCSPlayerController? player, CommandInfo command)
        {
            if (player == null || !HasPermission(player)) return;

            var target = FindPlayer(command.ArgByIndex(1));
            if (target != null)
            {
                target.ChangeTeam(CsTeam.CounterTerrorist);
                target.PrintToChat(string.Format(Config.Tag + Instance.Localizer["admin.counter_terrorist"], player.PlayerName));
                Server.PrintToChatAll(string.Format(Config.Tag + Instance.Localizer["player.ctmove.announcement"], target.PlayerName, player.PlayerName));
            }
        }

        [CommandHelper(minArgs: 1, "<name or #userid>", whoCanExecute: CommandUsage.CLIENT_AND_SERVER)]
        public static void Command_MoveSpectate(CCSPlayerController? player, CommandInfo command)
        {
            if (player == null || !HasPermission(player)) return;

            var target = FindPlayer(command.ArgByIndex(1));
            if (target != null)
            {
                target.ChangeTeam(CsTeam.Spectator);
                target.PrintToChat(string.Format(Config.Tag + Instance.Localizer["admin.spectator"], player.PlayerName));
                Server.PrintToChatAll(string.Format(Config.Tag + Instance.Localizer["player.spec_move.announcement"], target.PlayerName, player.PlayerName));
            }
        }

        [CommandHelper(minArgs: 1, "<name or #userid>", whoCanExecute: CommandUsage.CLIENT_AND_SERVER)]
        public static void Command_Swap(CCSPlayerController? player, CommandInfo command)
        {
            if (player == null || !HasPermission(player)) return;

            var target = FindPlayer(command.ArgByIndex(1));
            if (target != null)
            {
                Swap(player, target);
            }
        }

        // Helper Methods
        private static bool HasPermission(CCSPlayerController player)
        {
            return Config.Permissions.Count == 0 || Config.Permissions.Any(permission => AdminManager.PlayerHasPermissions(player, permission));
        }

        private static CCSPlayerController? FindPlayer(string identifier)
        {
            return Utilities.GetPlayers().FirstOrDefault(p => p.PlayerName == identifier || p.SteamID.ToString() == identifier);
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
