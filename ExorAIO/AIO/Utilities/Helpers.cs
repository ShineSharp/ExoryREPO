using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Utilities
{
    using Color = System.Drawing.Color;

    /// <summary>
    /// The Mana manager class.
    /// </summary>
    class ManaManager
    {
        /// <summary>
        /// The minimum mana needed to cast the Q Spell.
        /// </summary>
        public static int NeededQMana
        => 
            Variables.Menu.Item($"{Variables.MainMenuName}.qspell.mana") != null ?
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.mana").GetValue<Slider>().Value : 0;

        /// <summary>
        /// The minimum mana needed to cast the W Spell.
        /// </summary>
        public static int NeededWMana 
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.wspell.mana") != null ?
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.mana").GetValue<Slider>().Value : 0;

        /// <summary>
        /// The minimum mana needed to cast the E Spell.
        /// </summary>
        public static int NeededEMana
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.espell.mana") != null ?
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.mana").GetValue<Slider>().Value : 0;

        /// <summary>
        /// The minimum mana needed to cast the R Spell.
        /// </summary>
        public static int NeededRMana
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.rspell.mana") != null ?
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.mana").GetValue<Slider>().Value : 0;

        /// <summary>
        /// The minimum mana needed to stack the Tear Item.
        /// </summary>
        public static int NeededTearMana
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.misc.tearmana") != null ?
                Variables.Menu.Item($"{Variables.MainMenuName}.misc.tearmana").GetValue<Slider>().Value : 0;
    }

    /// <summary>
    /// The drawings class.
    /// </summary>
    class Drawings
    {
        /// <summary>
        /// Loads the drawings.
        /// </summary>
        public static void Initialize()
        {
            Drawing.OnDraw += delegate
            {
                /// <summary>
                /// Loads the Q drawing.
                /// </summary>
                if (Variables.Q != null &&
                    Variables.Q.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.q") != null &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.q").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.Q.Range, Color.Green, 1);
                }

                /// <summary>
                /// Loads the W drawing.
                /// </summary>
                if (Variables.W != null &&
                    Variables.W.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.w") != null &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.w").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.W.Range, Color.Purple, 1);
                }

                /// <summary>
                /// Loads the E drawing.
                /// </summary>
                if (Variables.E != null &&
                    Variables.E.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.e") != null &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.e").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.E.Range, Color.Cyan, 1);
                }

                /// <summary>
                /// Loads the R drawing.
                /// </summary>
                if (Variables.R != null &&
                    Variables.R.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.r") != null &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.r").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.R.Range, Color.Red, 1);
                }
            };
        }
    }
}
