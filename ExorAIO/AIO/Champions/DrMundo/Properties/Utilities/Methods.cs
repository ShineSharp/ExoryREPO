using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.DrMundo
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
            Game.OnUpdate += DrMundo.OnUpdate;
            Obj_AI_Base.OnDoCast += DrMundo.OnDoCast;
        }
    }
}
