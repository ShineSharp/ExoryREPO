using LeagueSharp;
using LeagueSharp.Common;

namespace ExorLucian
{
    using System.Drawing;

    using Orbwalking = SFXTargetSelector.Orbwalking;
    using TargetSelector = SFXTargetSelector.TargetSelector;

    /// <summary>
    /// The settings class.
    /// </summary>
    class Menus
    {
        /// <summary>
        /// Sets the menu.
        /// </summary>
        public static void Initialize()
        {
            /// <summary>
            /// Sets the main menu.
            /// </summary>
            Variables.Menu = new Menu("ExorLucian", $"{Variables.MainMenuName}", true);
            {
                /// <summary>
                /// Sets the orbwalker menu.
                /// </summary>
                Variables.OrbwalkerMenu = new Menu("Orbwalker", $"{Variables.MainMenuName}.orbwalkermenu");
                {
                    Variables.Orbwalker = new Orbwalking.Orbwalker(Variables.OrbwalkerMenu);
                }
                Variables.Menu.AddSubMenu(Variables.OrbwalkerMenu);

                /// <summary>
                /// Sets the target-selector menu.
                /// </summary>
                Variables.TargetSelectorMenu = new Menu("[SFX]Target Selector", $"{Variables.MainMenuName}.targetselector");
                {
                    TargetSelector.AddToMenu(Variables.TargetSelectorMenu);
                }
                Variables.Menu.AddSubMenu(Variables.TargetSelectorMenu);

                /// <summary>
                /// Sets the spells menu.
                /// </summary>
                Variables.SettingsMenu = new Menu("Spells", $"{Variables.MainMenuName}.settingsmenu");
                {
                    Variables.QMenu = new Menu("Use Q to:", $"{Variables.MainMenuName}.qmenu")
                        .SetFontStyle(FontStyle.Regular, SharpDX.Color.Green);
                    {
                        Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.combo",  "Combo")).SetValue(true);
                        Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.harass", "Minion -> Harass")).SetValue(true);
                        Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.ks",     "KillSteal")).SetValue(true);
                        Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.farm",   "LaneClear")).SetValue(true);
                        Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.mana",   "Harass/LaneClear: Mana >= x%"))
                            .SetValue(new Slider(50, 0, 99));
                        {
                            Variables.WhiteListMenu = new Menu("Minion -> Harass: Whitelist", $"{Variables.MainMenuName}.qspell.whitelist");
                            {
                                foreach (var champ in HeroManager.Enemies)
                                {
                                    Variables.WhiteListMenu
                                        .AddItem(
                                            new MenuItem(
                                                $"{Variables.MainMenuName}.qspell.whitelist.{champ.ChampionName.ToLower()}",
                                                $"Harass: {champ.ChampionName}")
                                        .SetValue(true));
                                }
                            }
                            Variables.QMenu.AddSubMenu(Variables.WhiteListMenu);
                        }

                    }
                    Variables.SettingsMenu.AddSubMenu(Variables.QMenu);

                    Variables.WMenu = new Menu("Use W to:", $"{Variables.MainMenuName}.wmenu")
                        .SetFontStyle(FontStyle.Regular, SharpDX.Color.Purple);
                    {
                        Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspell.combo",  "Combo")).SetValue(true);
                        Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspell.ks",     "KillSteal")).SetValue(true);
                        Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspell.farm",   "LaneClear")).SetValue(true);
                        Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspell.mana",   "LaneClear: Mana >= x%"))
                            .SetValue(new Slider(50, 0, 99));
                    }
                    Variables.SettingsMenu.AddSubMenu(Variables.WMenu);

                    Variables.EMenu = new Menu("Use E to:", $"{Variables.MainMenuName}.emenu")
                        .SetFontStyle(FontStyle.Regular, SharpDX.Color.Cyan);
                    {
                        Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.auto",   "Smart Logic")).SetValue(true);
                        Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.jcfarm", "JungleClear")).SetValue(true);
                        Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.mana",   "JungleClear: Mana >= x%"))
                            .SetValue(new Slider(50, 0, 99));
                    }
                    Variables.SettingsMenu.AddSubMenu(Variables.EMenu);

                    Variables.RMenu = new Menu("Use R to:", $"{Variables.MainMenuName}.rmenu")
                        .SetFontStyle(FontStyle.Regular, SharpDX.Color.Red);
                    {
                        Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rspell.boolrsa", "Semi-Automatic R")).SetValue(false);
                        Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rspell.keyrsa", "Key:"))
                            .SetValue(
                                new KeyBind(
                                    "T".ToCharArray()[0],
                                    KeyBindType.Press));
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
                        .SetFontStyle(FontStyle.Regular, SharpDX.Color.Green);

                    Variables.DrawingsMenu
                        .AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.w", "W Range"))
                        .SetValue(false)
                        .SetFontStyle(FontStyle.Regular, SharpDX.Color.Purple);

                    Variables.DrawingsMenu
                        .AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.e", "E Range"))
                        .SetValue(false)
                        .SetFontStyle(FontStyle.Regular, SharpDX.Color.Cyan);
                }
                Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
            }
            Variables.Menu.AddToMainMenu();
        }
    }
}
