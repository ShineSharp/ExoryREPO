using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Draven
{
    /// <summary>
    /// The methods class.
    /// </summary>
    class Methods
    {
        /// <summary>
        /// Sets the methods.
        /// </summary>
        public static void Initialize()
        {
            Game.OnUpdate += Draven.OnUpdate;
            AntiGapcloser.OnEnemyGapcloser += Draven.OnEnemyGapcloser;
            Interrupter2.OnInterruptableTarget += Draven.OnInterruptableTarget;
        }
    }
}
