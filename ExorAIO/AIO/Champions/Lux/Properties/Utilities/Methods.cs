using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Lux
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
            Game.OnUpdate += Lux.OnUpdate;
            GameObject.OnCreate += Lux.OnCreate;
            GameObject.OnDelete += Lux.OnDelete;
        }
    }
}
