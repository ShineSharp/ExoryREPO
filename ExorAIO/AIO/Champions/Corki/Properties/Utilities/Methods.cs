namespace ExorAIO.Champions.Corki
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
            Game.OnUpdate += Corki.OnUpdate;
            Obj_AI_Base.OnDoCast += Corki.OnDoCast;
        }
    }
}
