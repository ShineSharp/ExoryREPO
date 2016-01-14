namespace AsunaCondemn
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
            Variables.E = new Spell(SpellSlot.E, ObjectManager.Player.BoundingRadius*2 + 550f);
            Variables.E.SetTargetted(0.25f, 1250f);

            Variables.Flash = ObjectManager.Player.GetSpellSlot("summonerflash");
        }

        /// <summary>
        /// Sets the menu.
        /// </summary>
        public static void SetMenu()
        {
            Variables.Menu = new Menu("AsunaCondem", $"{Variables.MainMenuName}", true);
            {
                Variables.SettingsMenu = new Menu("Condem Menu", $"{Variables.MainMenuName}.condemmenu");
                {
                    Variables.SettingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.condem.executecondem", "Execute Condemn -> Flash Mechanic").SetValue(new KeyBind("C".ToCharArray()[0], KeyBindType.Press)));
                }
                Variables.Menu.AddSubMenu(Variables.SettingsMenu);

                Variables.DrawingsMenu = new Menu("Drawings Menu", $"{Variables.MainMenuName}.drawingsmenu");
                {
                    Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.e", "Show E Prediction")).SetValue(true);
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
            Game.OnUpdate += Condem.Game_OnGameUpdate;
            Drawing.OnDraw += Condem.Drawing_OnDraw;
        }
    }
}
