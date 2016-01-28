using LeagueSharp;
using LeagueSharp.Common;

namespace AsunaCondemn
{
    using System;

    /// <summary>
    /// The main class.
    /// </summary>
    class Condem
    {
        /// <summary>
        /// Triggers when the champion is loaded.
        /// </summary>
        public static void OnLoad()
        {
            Menus.Initialize();
            Spells.Initialize();
            Methods.Initialize();
            Drawings.Initialize();
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void OnUpdate(EventArgs args)
        {
            if (!ObjectManager.Player.IsDead)
            {
                Logics.ExecuteAuto(args);
            }
        }
    }
}
