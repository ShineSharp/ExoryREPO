using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Evelynn
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
            Game.OnUpdate += Evelynn.OnUpdate;
            Obj_AI_Base.OnDoCast += Evelynn.OnDoCast;
        }
    }
}
