using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Jax
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
            Game.OnUpdate += Jax.OnUpdate;
            Obj_AI_Base.OnDoCast += Jax.OnDoCast;
        }
    }
}
