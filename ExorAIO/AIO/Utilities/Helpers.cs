namespace ExorAIO.Utilities
{
    using System;
    using System.Linq;
    using LeagueSharp;
    using LeagueSharp.Common;

    /// <summary>
    /// The Mana manager class.
    /// </summary>
    class ManaManager
    {
        /// <summary>
        /// Sets the minimum necessary mana to use the Q spell.
        /// </summary>
        public static int NeededQMana
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.qspell.mana") != null ?
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.mana").GetValue<Slider>().Value : 0;

        /// <summary>
        /// Sets the minimum necessary mana to use the W spell.
        /// </summary>
        public static int NeededWMana 
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.wspell.mana") != null ?
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.mana").GetValue<Slider>().Value : 0;

        /// <summary>
        /// Sets the minimum necessary mana to use the E spell.
        /// </summary>
        public static int NeededEMana
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.espell.mana") != null ?
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.mana").GetValue<Slider>().Value : 0;

        /// <summary>
        /// Sets the minimum necessary mana to use the R spell.
        /// </summary>
        public static int NeededRMana
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.rspell.mana") != null ?
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.mana").GetValue<Slider>().Value : 0;

        /// <summary>
        /// Sets the minimum necessary mana to stack the tear item.
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
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.Q.Range, System.Drawing.Color.Green, 1);
                }

                /// <summary>
                /// Loads the W drawing.
                /// </summary>
                if (Variables.W != null &&
                    Variables.W.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.w") != null &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.w").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.W.Range, System.Drawing.Color.Purple, 1);
                }

                /// <summary>
                /// Loads the E drawing.
                /// </summary>
                if (Variables.E != null &&
                    Variables.E.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.e") != null &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.e").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.E.Range, System.Drawing.Color.Cyan, 1);
                }

                /// <summary>
                /// Loads the R drawing.
                /// </summary>
                if (Variables.R != null &&
                    Variables.R.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.r") != null &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.r").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.R.Range, System.Drawing.Color.Red, 1);
                }
            };
        }
    }
}
