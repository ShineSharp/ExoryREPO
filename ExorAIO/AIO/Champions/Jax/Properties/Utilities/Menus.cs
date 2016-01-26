using LeagueSharp.Common;

namespace ExorAIO.Champions.Jax
{
    using System.Drawing;
    using ExorAIO.Utilities;
    using Color = SharpDX.Color;

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
                    .SetFontStyle(FontStyle.Regular, Color.Green);
                {
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.combo",     "Combo")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.ks",        "KillSteal")).SetValue(true);
                }
                Variables.SettingsMenu.AddSubMenu(Variables.QMenu);

                Variables.WMenu = new Menu("Use W to:", $"{Variables.MainMenuName}.wmenu")
                    .SetFontStyle(FontStyle.Regular, Color.Purple);
                {
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsspell.combo",    "Combo")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspell.jc",        "JungleClear")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspell.mana",      "JungleClear: Mana >= x"))
                        .SetValue(new Slider(50, 10, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.WMenu);

                Variables.EMenu = new Menu("Use E to:", $"{Variables.MainMenuName}.emenu")
                    .SetFontStyle(FontStyle.Regular, Color.Cyan);
                {
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.combo",     "Combo")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.farm",      "LaneClear")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.mana",      "LaneClear: Mana >= x"))
                        .SetValue(new Slider(50, 10, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.EMenu);

                Variables.RMenu = new Menu("R Settings", $"{Variables.MainMenuName}.rsettingsmenu")
                    .SetFontStyle(FontStyle.Regular, Color.Red);
                {
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rspell.lifesaver", "LifeSaver")).SetValue(true);
                }
                Variables.SettingsMenu.AddSubMenu(Variables.RMenu);
            }
            Variables.Menu.AddSubMenu(Variables.SettingsMenu);

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
                    .AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.e", "E Range"))
                    .SetValue(false)
                    .SetFontStyle(FontStyle.Regular, Color.Cyan);
            }
            Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
        }
    }
}