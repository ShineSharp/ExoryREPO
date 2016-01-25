namespace ExorAIO.Champions.Akali
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
            Game.OnUpdate += Akali.OnUpdate;
            Obj_AI_Base.OnDoCast += Akali.OnDoCast;
        }
    }
}
