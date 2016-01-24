namespace ExorAIO.Champions.Ashe
{
    using LeagueSharp;
    using LeagueSharp.Common;
    using ExorAIO.Utilities;

    /// <summary>
    /// The methods class.
    /// </summary>
    public class Methods
    {  
        /// <summary>
        /// Sets the methods.
        /// </summary>
        public static void Initialize()
        {
            Game.OnUpdate += Ashe.Game_OnGameUpdate;
            Obj_AI_Base.OnDoCast += Ashe.Obj_AI_Base_OnDoCast;
        }
    }
}
