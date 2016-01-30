using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.KogMaw
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
            Game.OnUpdate += KogMaw.OnUpdate;
            Obj_AI_Base.OnDoCast += KogMaw.OnDoCast;
        }
    }
}
