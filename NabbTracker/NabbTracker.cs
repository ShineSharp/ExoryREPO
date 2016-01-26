using LeagueSharp;
using LeagueSharp.Common;

namespace NabbTracker
{
    /// <summary>
    /// The main class.
    /// </summary>
    class Tracker
    {
        /// <summary>
        /// Called when the game loads itself.
        /// </summary>
        public static void OnLoad()
        {
            Menus.Initialize();
            Drawings.Initialize();
        }
    }
}
