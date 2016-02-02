using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Olaf
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
            Game.OnUpdate += Olaf.OnUpdate;
            Obj_AI_Base.OnDoCast += Olaf.OnDoCast;
        }
    }
}
