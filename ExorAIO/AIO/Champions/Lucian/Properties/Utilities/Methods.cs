using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Lucian
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
            Game.OnUpdate += Lucian.OnUpdate;
            Obj_AI_Base.OnDoCast += Lucian.OnDoCast;
        }
    }
}
