using LeagueSharp;
using LeagueSharp.Common;

namespace NabbActivator
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
            Game.OnUpdate += Activator.OnUpdate;
            Obj_AI_Base.OnDoCast += Activator.OnDoCast;
        }
    }
}
