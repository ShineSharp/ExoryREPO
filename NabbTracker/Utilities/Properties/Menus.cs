using LeagueSharp;
using LeagueSharp.Common;

namespace NabbTracker
{
    using System.Drawing;
    using Color = SharpDX.Color;

    /// <summary>
    /// The menu class.
    /// </summary>
    class Menus
    {
        /// <summary>
        /// Builds the general Menu.
        /// </summary>
        public static void Initialize()
        {
            /// <summary>
            /// The general Menu.
            /// </summary>
            Variables.Menu = new Menu($"{Variables.MainMenuCodeName}", $"{Variables.MainMenuName}", true);
            {
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.allies", "Enable Allies"))
                    .SetFontStyle(FontStyle.Regular, Color.Green)
                    .SetValue(true);
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.enemies", "Enable Enemies"))
                    .SetFontStyle(FontStyle.Regular, Color.Red)
                    .SetValue(true);
            }
            Variables.Menu.AddToMainMenu();
        }
    }
}
