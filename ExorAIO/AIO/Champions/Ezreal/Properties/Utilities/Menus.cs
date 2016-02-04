using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Ezreal
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
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.combo",    "Combo")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.ks",       "KillSteal")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.farm",     "FarmHelper")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.harass",   "AutoHarass")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.mana",     "FarmHelper/AutoHarass: Mana >= x"))
                        .SetValue(new Slider(50, 10, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.QMenu);

                Variables.WMenu = new Menu("Use W to:", $"{Variables.MainMenuName}.wmenu")
                    .SetFontStyle(FontStyle.Regular, Color.Purple);
                {
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspell.combo",    "Combo")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspell.ks",       "KillSteal")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspell.harass",   "AutoHarass")).SetValue(ObjectManager.Player.TotalMagicalDamage > ObjectManager.Player.FlatPhysicalDamageMod);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspell.mana",     "AutoHarass: Mana >= x"))
                        .SetValue(new Slider(50, 10, 99));
                    
                }
                Variables.SettingsMenu.AddSubMenu(Variables.WMenu);

                Variables.RMenu = new Menu("Use R to:", $"{Variables.MainMenuName}.rmenu")
                    .SetFontStyle(FontStyle.Regular, Color.Red);
                {
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rspell.combo",    "Combo")).SetValue(true);
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rspell.ks",       "KillSteal")).SetValue(true);
                    {
                        Variables.WhiteListMenu = new Menu("Ultimate: Whitelist Menu", $"{Variables.MainMenuName}.rmenu.whitelistmenu");
                        {
                            foreach (var champ in HeroManager.Enemies)
                            {
                                Variables.WhiteListMenu
                                    .AddItem(
                                        new MenuItem(
                                            $"{Variables.MainMenuName}.rspell.whitelist.{champ.ChampionName.ToLower()}",
                                            $"Use against: {champ.ChampionName}")
                                    .SetValue(true)
                                );
                            }
                        }
                        Variables.RMenu.AddSubMenu(Variables.WhiteListMenu);
                    }
                }
                Variables.SettingsMenu.AddSubMenu(Variables.RMenu);
            }
            Variables.Menu.AddSubMenu(Variables.SettingsMenu);

            /// <summary>
            /// Sets the miscellaneous menu.
            /// </summary>
            Variables.MiscMenu = new Menu("Miscellaneous", $"{Variables.MainMenuName}.miscmenu");
            {
                Variables.MiscMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.misc.tear",        "Stack Tear")).SetValue(true);
                Variables.MiscMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.misc.tearmana",    "Stack Tear: Mana > x%"))
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
            }
            Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
        }
    }
}
