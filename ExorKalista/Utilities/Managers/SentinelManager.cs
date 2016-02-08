using LeagueSharp;
using LeagueSharp.Common;

namespace ExorKalista
{
    using System;
    using System.Linq;
    using SharpDX;
    using Orbwalking = SFXTargetSelector.Orbwalking;

    /// <summary>
    /// The Sentinel manager class.
    /// </summary>
    class SentinelManager
    {
        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void Initialize(EventArgs args)
        {
            /// <summary>
            /// The W Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
               !ObjectManager.Player.IsWindingUp &&
               !ObjectManager.Player.IsRecalling() &&
                ObjectManager.Player.CountEnemiesInRange(1500) == 0 &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.None &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.auto").IsActive())
            {
                if (Variables.SentinelLocations
                    .Any(h => ObjectManager.Player.Distance(h) < Variables.W.Range))
                {
                    Variables.W.Cast(
                        Variables.SentinelLocations
                            .OrderBy(d => ObjectManager.Player.Distance(d))
                            .FirstOrDefault()
                    );
                }
            }
        }
    }
}
