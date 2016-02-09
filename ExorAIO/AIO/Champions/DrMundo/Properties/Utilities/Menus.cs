using LeagueSharp.Common;

namespace ExorAIO.Champions.DrMundo
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
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.combo",      "Combo")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.ks",         "KillSteal")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.harass",     "AutoHarass")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.mana",       "AutoHarass: Energy >= x%"))
                        .SetValue(new Slider(50, 0, 99));
                    {
                        Variables.WhiteListMenu = new Menu("AutoHarass: Whitelist Menu", $"{Variables.MainMenuName}.qmenu.whitelistmenu");
                        {
                            foreach (var champ in HeroManager.Enemies)
                            {
                                Variables.WhiteListMenu
                                    .AddItem(
                                        new MenuItem(
                                            $"{Variables.MainMenuName}.qspell.whitelist.{champ.ChampionName.ToLower()}",
                                            $"AutoHarass: {champ.ChampionName}")
                                    .SetValue(true)
                                );
                            }
                        }
                        Variables.QMenu.AddSubMenu(Variables.WhiteListMenu);
                    }
                }
                Variables.SettingsMenu.AddSubMenu(Variables.QMenu);

                Variables.EMenu = new Menu("Use E to:", $"{Variables.MainMenuName}.emenu")
                    .SetFontStyle(FontStyle.Regular, Color.Cyan);
                {
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.combo",      "Combo")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.ks",         "KillSteal")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.farm",       "LaneClear")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.mana",       "LaneClear: Energy >= x%"))
                        .SetValue(new Slider(50, 0, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.EMenu);

                Variables.RMenu = new Menu("Use R to:", $"{Variables.MainMenuName}.rmenu")
                    .SetFontStyle(FontStyle.Regular, Color.Red);
                {
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rspell.lifesaver",  "Lifesaver")).SetValue(true);
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
                    .AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.w", "W Range"))
                    .SetValue(false)
                    .SetFontStyle(FontStyle.Regular, Color.Purple);

                Variables.DrawingsMenu
                    .AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.e", "E Range"))
                    .SetValue(false)
                    .SetFontStyle(FontStyle.Regular, Color.Cyan);
            }
            Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
        }
    }
}
