using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Graves
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
            Game.OnUpdate += Graves.OnUpdate;
            Obj_AI_Base.OnDoCast += Graves.OnDoCast;
        }
    }
}
