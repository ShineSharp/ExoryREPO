using LeagueSharp;
using LeagueSharp.Common;

namespace ExorRyze
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
            Game.OnUpdate += Ryze.OnUpdate;
            AntiGapcloser.OnEnemyGapcloser += Ryze.OnEnemyGapcloser;
        }
    }
}
