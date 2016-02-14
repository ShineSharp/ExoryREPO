using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Caitlyn
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
            Game.OnUpdate += Caitlyn.OnUpdate;
            Obj_AI_Base.OnDoCast += Caitlyn.OnDoCast;
            AntiGapcloser.OnEnemyGapcloser += Caitlyn.OnEnemyGapcloser;
        }
    }
}
