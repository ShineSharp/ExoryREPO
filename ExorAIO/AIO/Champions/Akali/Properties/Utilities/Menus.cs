namespace ExorAIO.Champions.Akali
{
    using System.Drawing;
    using LeagueSharp.Common;
    using ExorAIO.Utilities;

    /// <summary>
    /// The menu class.
    /// </summary>
    class Menus
    {
        /// <summary>
        /// Sets the menu.
        /// </summary>
        public static void Initialize()
        {
            /// <summary>
            /// Sets the spells menu.
            /// </summary>
            Variables.SettingsMenu = new Menu("Spells Menu", $"{Variables.MainMenuName}.settingsmenu");
            {
                Variables.QMenu = new Menu("Use Q to:", $"{Variables.MainMenuName}.qmenu")
                    .SetFontStyle(FontStyle.Regular, SharpDX.Color.Green);
                {
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.combo",  "Combo")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.ks",     "KillSteal")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.harass", "AutoHarass")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.mana",   "AutoHarass: Energy >= x%"))
                        .SetValue(new Slider(50, 0, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.QMenu);

                Variables.EMenu = new Menu("Use E to:", $"{Variables.MainMenuName}.emenu")
                    .SetFontStyle(FontStyle.Regular, SharpDX.Color.Cyan);
                {
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.combo",  "Combo")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.ks",     "KillSteal")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.farm",   "LaneClear")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.mana",   "LaneClear: Energy >= x%"))
                        .SetValue(new Slider(50, 0, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.EMenu);

                Variables.RMenu = new Menu("Use R to:", $"{Variables.MainMenuName}.rmenu")
                    .SetFontStyle(FontStyle.Regular, SharpDX.Color.Red);
                {
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rspell.combo",  "Combo")).SetValue(true);
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rspell.ks",     "KillSteal")).SetValue(true);
                }
                Variables.SettingsMenu.AddSubMenu(Variables.RMenu);
            }
            Variables.Menu.AddSubMenu(Variables.SettingsMenu);

            /// <summary>
            /// Sets the miscellaneous menu.
            /// </summary>
            Variables.MiscMenu = new Menu("Miscellaneous Menu", $"{Variables.MainMenuName}.miscmenu");
            {
                Variables.MiscMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.misc.safecheck", "Enable Safety Checks?")).SetValue(true);
            }
            Variables.Menu.AddSubMenu(Variables.MiscMenu);

            /// <summary>
            /// Sets the drawings menu.
            /// </summary>
            Variables.DrawingsMenu = new Menu("Drawings Menu", $"{Variables.MainMenuName}.drawingsmenu");
            {
                Variables.DrawingsMenu
                    .AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.q", "Show Q Range"))
                    .SetValue(false)
                    .SetFontStyle(FontStyle.Regular, SharpDX.Color.Green);

                Variables.DrawingsMenu
                    .AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.e", "Show E Range"))
                    .SetValue(false)
                    .SetFontStyle(FontStyle.Regular, SharpDX.Color.Cyan);

                Variables.DrawingsMenu
                    .AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.r", "Show R Range"))
                    .SetValue(false)
                    .SetFontStyle(FontStyle.Regular, SharpDX.Color.Red);
            }
            Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
        }
    }
}
