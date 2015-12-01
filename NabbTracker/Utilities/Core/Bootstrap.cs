namespace NabbTracker
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;
    
    /// <summary>
    /// The main class.
    /// </summary>
    public class Tracker
    {
        /// <summary>
        /// Called when the game loads itself.
        /// </summary>
        public static void OnLoad()
        {
            BuildMenu();
            Drawings.Load();
        }

        /// <summary>
        /// Builds the general Menu.
        /// </summary>
        public static void BuildMenu()
        {
            Variables.Menu = new Menu($"{Variables.MainMenuCodeName}", $"{Variables.MainMenuName}", true);
            {
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.allies", "Enable Allies")).SetValue(true);
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.enemies", "Enable Enemies")).SetValue(true);
            }
            Variables.Menu.AddToMainMenu();
        }
    }
}
