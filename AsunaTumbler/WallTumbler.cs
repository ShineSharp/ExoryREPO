namespace AsunaTumbler
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    /// <summary>
    /// The main class.
    /// </summary>
    public class WallTumbler
    {
        /// <summary>
        /// Triggers when the champion is loaded.
        /// </summary>
        public static void OnLoad()
        {
            Settings.SetSpells();
            Settings.SetMenu();
            Settings.SetMethods();
            Drawings.Load();
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
