using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Entities;
using CounterStrikeSharp.API.Modules.Memory;
using System.Text;
using Vector = CounterStrikeSharp.API.Modules.Utils.Vector;

namespace ChangeTeams;

public static class PlayerExtensions
{
    public static bool CanTarget(this CCSPlayerController? controller, CCSPlayerController? target)
    {
        if (controller is null || target is null) return true;
        if (target.IsBot) return true;

        return AdminManager.CanPlayerTarget(controller, target) ||
                                  AdminManager.CanPlayerTarget(new SteamID(controller.SteamID),
                                      new SteamID(target.SteamID));
    }
}