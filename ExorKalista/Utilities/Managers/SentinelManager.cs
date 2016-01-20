namespace ExorKalista
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using SharpDX;

    /// <summary>
    /// The Sentinel manager class.
    /// </summary>
    class SentinelManager
    {
        /// <summary>
        /// Gets all the sentinel locations.
        /// </summary>
        public static Vector2[] AllLocations =
        {
            SummonersRift.River.Baron,
            SummonersRift.River.Dragon,
            SummonersRift.Jungle.Red_RedBuff,
            SummonersRift.Jungle.Red_BlueBuff,
            SummonersRift.Jungle.Blue_BlueBuff,
            SummonersRift.Jungle.Blue_BlueBuff
        };

        /// <summary>
        /// Gets the possible sentinel locations.
        /// </summary>
        public static Vector2 GetPerfectSpot
        => 
            AllLocations.Where(
                loc =>
                    loc != null &&
                    loc.Distance(ObjectManager.Player) < Variables.W.Range)
            .OrderBy(
                h =>
                    h.Distance(ObjectManager.Player))
            .FirstOrDefault();
    }
}
