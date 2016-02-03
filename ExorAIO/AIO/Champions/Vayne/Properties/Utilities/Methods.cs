using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Vayne
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
            Game.OnUpdate += Vayne.OnUpdate;
            Orbwalking.OnAttack += Vayne.OnAttack;
            Obj_AI_Base.OnDoCast += Vayne.OnDoCast;
        }
    }
}
