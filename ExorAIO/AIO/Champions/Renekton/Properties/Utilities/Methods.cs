using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Renekton
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
            Game.OnUpdate += Renekton.OnUpdate;
            Obj_AI_Base.OnDoCast += Renekton.OnDoCast;
        }
    }
}
