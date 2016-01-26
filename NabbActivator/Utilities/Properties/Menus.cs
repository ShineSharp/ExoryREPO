using LeagueSharp;
using LeagueSharp.Common;

namespace NabbActivator
{
    /// <summary>
    /// The menu class.
    /// </summary>
    class Menus
    {
        /// <summary>
        /// Sets the menus.
        /// </summary>
        public static void Initialize()
        {
            /// <summary>
            /// Sets the main menu.
            /// </summary>
            Variables.Menu = new Menu("NabbActivator", $"{Variables.MainMenuName}", true);
            {
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.offensives",       "Offensives")).SetValue(true);
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.defensives",       "Defensives")).SetValue(true);
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.spells",           "Spells")).SetValue(true);
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.cleansers",        "Cleansers")).SetValue(true);
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.consumables",      "Potions")).SetValue(true);
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.resetters",        "Tiamat/Hydra/Titanic")).SetValue(true);
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.randomizer",       "Enable NabbHumanizer")).SetValue(true);

                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.separator", ""));

                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.warn1", "!(Must be the same as your Combo key)!"));
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.combo_button",     "Combo"))
                    .SetValue(new KeyBind(32, KeyBindType.Press));
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.warn2", "!(Must be the same as your Combo key)!"));

                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.separator2", ""));

                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.warn3", "!(Must be the same as your LaneClear key)!"));
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.laneclear_button", "LaneClear"))
                    .SetValue(new KeyBind(86,KeyBindType.Press));
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.warn4", "!(Must be the same as your LaneClear key)!"));
                
                /// <summary>
                /// Sets consumable sliders menu.
                /// </summary>
                Variables.SliderMenu = new Menu("Consumables Options", $"{Variables.MainMenuName}.consumables.options");
                {
                    Variables.SliderMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.consumables.health", "Consumables: Health < x%")
                        .SetValue(new Slider(50, 0, 100)));
                    Variables.SliderMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.consumables.mana",   "Consumables: Mana < x%")
                        .SetValue(new Slider(50, 0, 100)));
                }
                Variables.Menu.AddSubMenu(Variables.SliderMenu);
            }
            Variables.Menu.AddToMainMenu();
        }
    }
}
