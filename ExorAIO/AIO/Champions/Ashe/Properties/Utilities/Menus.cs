namespace ExorAIO.Champions.Ashe
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
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.combo",    "Combo")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.farm",     "LaneClear")).SetValue(false);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qspell.mana",     "LaneClear: Mana >= x%"))
                        .SetValue(new Slider(50, 0, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.QMenu);

                Variables.WMenu = new Menu("Use W to:", $"{Variables.MainMenuName}.wmenu")
                    .SetFontStyle(FontStyle.Regular, SharpDX.Color.Purple);
                {
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspell.combo",    "Combo")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspell.ks",       "KillSteal")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspell.harass",   "Harass Impaired Enemies")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspell.farm",     "LaneClear")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wspell.mana",     "LaneClear: Mana >= x%"))
                        .SetValue(new Slider(50, 0, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.WMenu);

                Variables.RMenu = new Menu("Use R to:", $"{Variables.MainMenuName}.rmenu")
                    .SetFontStyle(FontStyle.Regular, SharpDX.Color.Red);
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
                                            $"Ultimate: {champ.ChampionName}")
                                    .SetValue(true));
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
                Variables.MiscMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.misc.ermechanic",    "E->R Doublelift Mechanic")).SetValue(true);
            }
            Variables.Menu.AddSubMenu(Variables.MiscMenu);

            /// <summary>
            /// Sets the drawings menu.
            /// </summary>
            Variables.DrawingsMenu = new Menu("Drawings", $"{Variables.MainMenuName}.drawingsmenu");
            {
                Variables.DrawingsMenu
                    .AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.w", "W Range"))
                    .SetValue(false)
                    .SetFontStyle(FontStyle.Regular, SharpDX.Color.Purple);
            }
            Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
        }
    }
}