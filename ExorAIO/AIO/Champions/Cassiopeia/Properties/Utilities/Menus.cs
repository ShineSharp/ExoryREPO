using LeagueSharp.Common;

namespace ExorAIO.Champions.Cassiopeia
{
    using System.Drawing;
    using ExorAIO.Utilities;
    using Color = SharpDX.Color;
    using SPrediction;

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
            SPrediction.Prediction.Initialize(Variables.Menu);

            /// <summary>
            /// Sets the spells menu.
            /// </summary>
            Variables.SettingsMenu = new Menu("Spells", $"{Variables.MainMenuName}.settingsmenu");
            {
                Variables.QMenu = new Menu("Use Q to:", $"{Variables.MainMenuName}.qmenu")
                    .SetFontStyle(FontStyle.Regular, Color.Green);
                {
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.combo",     "Combo")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.farm",      "LaneClear")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.mana",      "LaneClear: Mana >= x%"))
                        .SetValue(new Slider(50, 0, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.QMenu);

                Variables.WMenu = new Menu("Use W to:", $"{Variables.MainMenuName}.wmenu")
                    .SetFontStyle(FontStyle.Regular, Color.Purple);
                {
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspell.combo",     "Combo")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspell.farm",      "LaneClear")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspell.mana",      "LaneClear: Mana >= x%"))
                        .SetValue(new Slider(50, 0, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.WMenu);

                Variables.EMenu = new Menu("Use E to:", $"{Variables.MainMenuName}.emenu")
                    .SetFontStyle(FontStyle.Regular, Color.Cyan);
                {
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.combo",     "Combo")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.farm",      "LastHit")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.delay",     "E Delay (ms)"))
                        .SetValue(new Slider(0, 0, 250));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.EMenu);

                Variables.RMenu = new Menu("Use R to:", $"{Variables.MainMenuName}.rmenu")
                    .SetFontStyle(FontStyle.Regular, Color.Red);
                {
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rspell.combo",     "Combo")).SetValue(true);
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rspell.enemies",   "Combo: if facing Enemies >="))
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
                Variables.MiscMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.misc.poison",       "E non Poisoned Minions")).SetValue(false);
                Variables.MiscMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.misc.aa",           "AA in Combo")).SetValue(true);
                Variables.MiscMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.misc.tear",         "Stack Tear")).SetValue(true);
                Variables.MiscMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.misc.tearmana",     "Stack Tear: Mana >= x%"))
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
                    .SetFontStyle(FontStyle.Regular, Color.Green);

                Variables.DrawingsMenu
                    .AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.w", "W Range"))
                    .SetValue(false)
                    .SetFontStyle(FontStyle.Regular, Color.Purple);

                Variables.DrawingsMenu
                    .AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.e", "E Range"))
                    .SetValue(false)
                    .SetFontStyle(FontStyle.Regular, Color.Cyan);

                Variables.DrawingsMenu
                    .AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.r", "R Range"))
                    .SetValue(false)
                    .SetFontStyle(FontStyle.Regular, Color.Red);
            }
            Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
        }
    }
}
