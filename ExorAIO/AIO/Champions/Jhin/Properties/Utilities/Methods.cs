using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Jhin
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
            Game.OnUpdate += Jhin.OnUpdate;
            Obj_AI_Base.OnDoCast += Jhin.OnDoCast;
            AntiGapcloser.OnEnemyGapcloser += Jhin.OnEnemyGapcloser;
        }
    }
}
