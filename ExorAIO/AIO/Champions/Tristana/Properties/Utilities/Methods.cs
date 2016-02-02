using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Tristana
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
            Game.OnUpdate += Tristana.OnUpdate;
        }
    }
}
