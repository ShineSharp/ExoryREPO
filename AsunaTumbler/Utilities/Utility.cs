namespace AsunaTumbler
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    /// <summary>
    /// The settings class.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Sets the spells.
        /// </summary>
        public static void SetSpells()
        {
            Variables.Q = new Spell(SpellSlot.Q);
        }

        /// <summary>
        /// Sets the menu.
        /// </summary>
        public static void SetMenu()
        {
            Variables.Menu = new Menu("AsunaTumbler", $"{Variables.MainMenuName}", true);
            {
                Variables.SettingsMenu = new Menu("WallTumbler Menu", $"{Variables.MainMenuName}.walltumblermenu");
                {
                    Variables.SettingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.walltumbler.execute", "Execute WallTumble").SetValue(new KeyBind("Y".ToCharArray()[0], KeyBindType.Press)));
                    Variables.SettingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.walltumbler.onclick", "Enable On-Click WallTumble")).SetValue(true);
                }
                Variables.Menu.AddSubMenu(Variables.SettingsMenu);

                Variables.DrawingsMenu = new Menu("Drawings Menu", $"{Variables.MainMenuName}.drawingsmenu");
                {
                    Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.show",       "Show Spots")).SetValue(true);
                }
                Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
            }
            Variables.Menu.AddToMainMenu();
        }

        /// <summary>
        /// Sets the methods.
        /// </summary>
        public static void SetMethods()
        {
            Game.OnUpdate += WallTumbler.OnUpdate;
        }
    }
}
