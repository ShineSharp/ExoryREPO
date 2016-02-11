using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Kalista
{
    /// <summary>
    /// The methods class.
    /// </summary>
    class Methods
    {
        /// <summary>
        /// The methods.
        /// </summary>
        public static void Initialize()
        {
            Game.OnUpdate += Kalista.OnUpdate;
            Orbwalking.OnNonKillableMinion += Kalista.OnNonKillableMinion;
        }
    }
}
