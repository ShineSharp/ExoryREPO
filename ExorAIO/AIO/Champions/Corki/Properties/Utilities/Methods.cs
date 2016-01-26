using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Corki
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
            Game.OnUpdate += Corki.OnUpdate;
            Obj_AI_Base.OnDoCast += Corki.OnDoCast;
        }
    }
}
