namespace ExorAIO.Champions.Ashe
{
    using LeagueSharp;
    using LeagueSharp.Common;
    using ExorAIO.Utilities;

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
            Game.OnUpdate += Ashe.OnUpdate;
            Obj_AI_Base.OnDoCast += Ashe.OnDoCast;
        }
    }
}
