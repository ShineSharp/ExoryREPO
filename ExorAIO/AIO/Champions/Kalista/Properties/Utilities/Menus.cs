using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Kalista
{
    using System.Drawing;
    using Color = SharpDX.Color;
    using ExorAIO.Utilities;
    using Orbwalking = SFXTargetSelector.Orbwalking;
    using TargetSelector = SFXTargetSelector.TargetSelector;
    using SPrediction;

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
            /// Sets the spells menu.
            /// </summary>
            Variables.SettingsMenu = new Menu("Spells", $"{Variables.MainMenuName}.settingsmenu");
            {
                Variables.QMenu = new Menu("Use Q to:", $"{Variables.MainMenuName}.qmenu")
                    .SetFontStyle(FontStyle.Regular, Color.Green);
                {
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.combo",     "Combo")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.immobile",  "Harass Impaired Enemies")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.ks",        "KillSteal")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.farm",      "LaneClear")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.mana",      "LaneClear: Mana >= %"))
                        .SetValue(new Slider(50, 0, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.QMenu);

                Variables.WMenu = new Menu("Use W to:", $"{Variables.MainMenuName}.wmenu")
                    .SetFontStyle(FontStyle.Regular, Color.Purple);
                {
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspell.auto",      "Logical")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspell.mana",      "Logical: Mana >= %"))
                        .SetValue(new Slider(50, 0, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.WMenu);

                Variables.EMenu = new Menu("Use E to:", $"{Variables.MainMenuName}.emenu")
                    .SetFontStyle(FontStyle.Regular, Color.Cyan);
                {
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.combo",     "Combo")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.jgc",       "JungleClear")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.death",     "Before Death")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.harass",    "Minion->Harass")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.farm",      "FarmHelper/LaneClear")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.mana",      "FarmHelper/LaneClear: Mana >= %"))
                        .SetValue(new Slider(50, 0, 99));
                    {
                        Variables.WhiteListMenu = new Menu("Minion->Harass: Whitelist", $"{Variables.MainMenuName}.espell.whitelist");
                        {
                            foreach (var champ in HeroManager.Enemies)
                            {
                                Variables.WhiteListMenu
                                    .AddItem(
                                        new MenuItem(
                                            $"{Variables.MainMenuName}.espell.whitelist.{champ.ChampionName.ToLower()}",
                                            $"Harass: {champ.ChampionName}")
                                    .SetValue(true));
                            }
                        }
                        Variables.EMenu.AddSubMenu(Variables.WhiteListMenu);
                    }
                }
                Variables.SettingsMenu.AddSubMenu(Variables.EMenu);

                Variables.RMenu = new Menu("Use R to:", $"{Variables.MainMenuName}.rmenu")
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
                    .AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.q",    "Q Range"))
                    .SetValue(false)
                    .SetFontStyle(FontStyle.Regular, Color.Green);

                Variables.DrawingsMenu
                    .AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.e",    "E Range"))
                    .SetValue(false)
                    .SetFontStyle(FontStyle.Regular, Color.Cyan);

                Variables.DrawingsMenu
                    .AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.edmg", "E Damage"))
                    .SetValue(true)
                    .SetFontStyle(FontStyle.Regular, Color.Orange);

                Variables.DrawingsMenu
                    .AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.r",    "R Range"))
                    .SetValue(false)
                    .SetFontStyle(FontStyle.Regular, Color.Red);
            }
            Variables.Menu.AddSubMenu(Variables.DrawingsMenu);

            /// <summary>
            /// Sets the spells menu.
            /// </summary>
            SPrediction.Prediction.Initialize(Variables.Menu);
        }
    }
}
