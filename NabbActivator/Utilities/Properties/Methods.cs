namespace NabbActivator
{
    using LeagueSharp;
    using LeagueSharp.Common;

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
            Game.OnUpdate += Activator.Game_OnGameUpdate;
            Obj_AI_Base.OnDoCast += Activator.Obj_AI_Base_OnDoCast;
        }
    }
}
