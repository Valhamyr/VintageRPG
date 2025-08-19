using Vintagestory.API.Common;

// Placeholder namespaces for the xskills and xlib APIs
using XSkills.API; // Provided by the XSkills mod
using XLib.API;    // Provided by the XLib library

namespace VintageRPG
{
    /// <summary>
    /// Ability class registered with XSkills that toggles creative flight for the player
    /// when acquired. The exact base class and API calls are supplied by the XSkills and
    /// XLib mods; this file shows how to hook into those systems without modifying them.
    /// </summary>
    public class FlightAbility : XSkillAbility
    {
        public override string Code => "vintagerpg:flight";

        public override void OnActivated(IPlayer player)
        {
            base.OnActivated(player);
            player.Entity.WorldData.SetFreeMoveAllSides(true); // enables creative-style flight
        }

        public override void OnDeactivated(IPlayer player)
        {
            base.OnDeactivated(player);
            player.Entity.WorldData.SetFreeMoveAllSides(false);
        }
    }

    /// <summary>
    /// Registers the FlightAbility with XSkills during game start.
    /// </summary>
    public class VintageRPGSystem : ModSystem
    {
        public override void Start(ICoreAPI api)
        {
            base.Start(api);
            var xskillsApi = api.ModLoader.GetModSystem("xskills") as IXSkillsAPI;
            xskillsApi?.RegisterAbility(new FlightAbility());
        }
    }
}
