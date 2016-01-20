namespace ExorKalista
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using SharpDX;

    using Orbwalking = SFXTargetSelector.Orbwalking;

    /// <summary>
    /// The Sentinel manager class.
    /// </summary>
    class SentinelManager
    {
        /// <summary>
        /// Gets all the sentinel locations.
        /// </summary>
        public static readonly List<Vector3> AllLocations = new List<Vector3>
        {
            new Vector3(9827.56f, 4426.136f, -71.2406f),
            new Vector3(4951.126f, 10394.05f, -71.2406f),
            new Vector3(10998.14f, 6954.169f, 51.72351f),
            new Vector3(7082.083f, 10838.25f, 56.2041f),
            new Vector3(3804.958f, 7875.456f, 52.11121f),
            new Vector3(7811.249f, 4034.486f, 53.81299f)
        };

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteSentinels(EventArgs args)
        {
            /// <summary>
            /// The W Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
               !ObjectManager.Player.IsWindingUp &&
               !ObjectManager.Player.IsDashing() &&
               !ObjectManager.Player.IsRecalling() &&
                ObjectManager.Player.CountEnemiesInRange(1500) == 0 &&

                (ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.None &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewauto").GetValue<bool>()))
            {
                Variables.W.Cast(SentinelManager.AllLocations
                    .Where(
                        h =>
                            ObjectManager.Player.Distance(h) < Variables.W.Range)
                    .OrderBy(
                        d =>
                            ObjectManager.Player.Distance(d))
                    .FirstOrDefault()
                );
            }
        }
    }
}
