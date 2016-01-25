namespace ExorAIO.Champions.Cassiopeia
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
            Variables.SettingsMenu = new Menu("Spells", $"{Variables.MainMenuName}.settingsmenu");
            {
                Variables.QMenu = new Menu("Use Q to:", $"{Variables.MainMenuName}.qmenu")
                    .SetFontStyle(FontStyle.Regular, SharpDX.Color.Green);
                {
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspells.combo",     "Combo")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspells.farm",      "LaneClear")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspells.mana",      "LaneClear: Mana >= x%"))
                        .SetValue(new Slider(50, 0, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.QMenu);

                Variables.WMenu = new Menu("Use W to:", $"{Variables.MainMenuName}.wmenu")
                    .SetFontStyle(FontStyle.Regular, SharpDX.Color.Purple);
                {
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspells.combo",     "Combo")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspells.farm",      "LaneClear")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspells.mana",      "LaneClear: Mana >= x%"))
                        .SetValue(new Slider(50, 0, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.WMenu);

                Variables.EMenu = new Menu("Use E to:", $"{Variables.MainMenuName}.emenu")
                    .SetFontStyle(FontStyle.Regular, SharpDX.Color.Cyan);
                {
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espells.combo",     "Combo")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espells.farm",      "LastHit")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espells.delay",     "E Delay (ms)"))
                        .SetValue(new Slider(0, 0, 250));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.EMenu);

                Variables.RMenu = new Menu("Use R to:", $"{Variables.MainMenuName}.rmenu")
                    .SetFontStyle(FontStyle.Regular, SharpDX.Color.Red);
                {
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rspells.combo",     "Combo")).SetValue(true);
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rspells.enemies",   "Combo: if facing Enemies >="))
                        .SetValue(new Slider(1, 1, 5));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.RMenu);
            }
            Variables.Menu.AddSubMenu(Variables.SettingsMenu);

            /// <summary>
            /// Sets the miscellaneous menu.
            /// </summary>
            Variables.MiscMenu = new Menu("Miscellaneous", $"{Variables.MainMenuName}.miscmenu");
            {
                Variables.MiscMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.misc.enopoison",     "E non Poisoned Minions?")).SetValue(true);
                Variables.MiscMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.misc.enableaa",      "AA in Combo?")).SetValue(true);
                Variables.MiscMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.misc.stacktear",     "Stack Tear?")).SetValue(true);
                Variables.MiscMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.misc.stacktearmana", "Stack Tear: Mana >= x%"))
                    .SetValue(new Slider(80, 1, 95));
            }
            Variables.Menu.AddSubMenu(Variables.MiscMenu);

            /// <summary>
            /// Sets the drawings menu.
            /// </summary>
            Variables.DrawingsMenu = new Menu("Drawings", $"{Variables.MainMenuName}.drawingsmenu");
            {
                Variables.DrawingsMenu
                    .AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.q", "Q Range"))
                    .SetValue(false)
                    .SetFontStyle(FontStyle.Regular, SharpDX.Color.Green);

                Variables.DrawingsMenu
                    .AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.w", "W Range"))
                    .SetValue(false)
                    .SetFontStyle(FontStyle.Regular, SharpDX.Color.Purple);

                Variables.DrawingsMenu
                    .AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.e", "E Range"))
                    .SetValue(false)
                    .SetFontStyle(FontStyle.Regular, SharpDX.Color.Cyan);

                Variables.DrawingsMenu
                    .AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.r", "R Range"))
                    .SetValue(false)
                    .SetFontStyle(FontStyle.Regular, SharpDX.Color.Red);
            }
            Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
        }
    }
}
