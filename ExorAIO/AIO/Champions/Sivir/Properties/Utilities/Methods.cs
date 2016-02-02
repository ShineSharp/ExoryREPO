using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Sivir
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
            Game.OnUpdate += Sivir.OnUpdate;
            Obj_AI_Base.OnDoCast += Sivir.OnDoCast;
            Obj_AI_Hero.OnProcessSpellCast += Sivir.OnProcessSpellCast;
        }
    }
}
