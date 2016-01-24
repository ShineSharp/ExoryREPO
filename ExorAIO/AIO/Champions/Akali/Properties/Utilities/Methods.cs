namespace ExorAIO.Champions.Akali
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
            Game.OnUpdate += Akali.Game_OnGameUpdate;
            Obj_AI_Base.OnDoCast += Akali.Obj_AI_Base_OnDoCast;
        }
    }
}
