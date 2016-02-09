using LeagueSharp;
using LeagueSharp.Common;

namespace AsunaCondemn
{
    using System.Drawing;
    using Color = SharpDX.Color;

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
            Variables.Menu = new Menu("AsunaCondem", $"{Variables.MainMenuName}", true);
            {
                /// <summary>
                /// Sets the spells menu.
                /// </summary>
                Variables.EMenu = new Menu("Use E to:", $"{Variables.MainMenuName}.emenu")
                    .SetFontStyle(FontStyle.Regular, Color.Cyan);
                {
                    Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.espell.execute", "Execute Condemn -> Flash Mechanic"))
                        .SetValue(
                            new KeyBind(
                                "C".ToCharArray()[0],
                                KeyBindType.Press));
                }
                Variables.Menu.AddSubMenu(Variables.EMenu);
    
                /// <summary>
                /// Sets the drawings menu.
                /// </summary>
                Variables.DrawingsMenu = new Menu("Drawings", $"{Variables.MainMenuName}.drawingsmenu");
                {
                    Variables.DrawingsMenu
                        .AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.e", "E Range"))
                        .SetValue(false)
                        .SetFontStyle(FontStyle.Regular, Color.Cyan);
                }
                Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
            }
            Variables.Menu.AddToMainMenu();
        }
    }
}
