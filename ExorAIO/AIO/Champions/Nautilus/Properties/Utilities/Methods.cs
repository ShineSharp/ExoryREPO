using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Nautilus
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
            Game.OnUpdate += Nautilus.OnUpdate;
            Obj_AI_Base.OnDoCast += Nautilus.OnDoCast;
        }
    }
}
