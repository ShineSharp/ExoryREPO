using LeagueSharp;
using LeagueSharp.Common;

namespace AsunaCondemn
{
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
            Variables.Menu = new Menu("AsunaCondem", $"{Variables.MainMenuName}", true);
            {
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.e", "Draw E Prediction")).SetValue(true);
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.execute", "Execute Condemn -> Flash Mechanic"))
                    .SetValue(
                        new KeyBind(
                            "C".ToCharArray()[0],
                            KeyBindType.Press));
            }
            Variables.Menu.AddToMainMenu();
        }
    }
}
