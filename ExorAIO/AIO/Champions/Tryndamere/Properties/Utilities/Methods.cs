using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Tryndamere
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
            Game.OnUpdate += Tryndamere.OnUpdate;
            Obj_AI_Base.OnDoCast += Tryndamere.OnDoCast;
        }
    }
}
