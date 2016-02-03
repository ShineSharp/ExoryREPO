using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Quinn
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
            Game.OnUpdate += Quinn.OnUpdate;
            Obj_AI_Base.OnDoCast += Quinn.OnDoCast;
            AntiGapcloser.OnEnemyGapcloser += Quinn.OnEnemyGapcloser;
            Interrupter2.OnInterruptableTarget += Quinn.OnInterruptableTarget;
        }
    }
}
