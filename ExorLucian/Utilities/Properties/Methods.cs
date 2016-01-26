using LeagueSharp;
using LeagueSharp.Common;

namespace ExorLucian
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
