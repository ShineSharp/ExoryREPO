using LeagueSharp;
using LeagueSharp.Common;

namespace ExorKalista
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
