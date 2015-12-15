namespace ExorLucian
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using Color = System.Drawing.Color;

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
            Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.qmana").GetValue<Slider>().Value;
    }

    /// <summary>
    /// The drawings class.
    /// </summary>
    public class Drawings
    {
        /// <summary>
        /// Loads the range drawings.
        /// </summary>
        public static void LoadRanges()
        {
            Drawing.OnDraw += delegate
            {
                /// <summary>
                /// Loads the Q drawing.
                /// </summary>
                if (Variables.Q.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.q").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.Q.Range, System.Drawing.Color.Green);
                }

                /// <summary>
                /// Loads the E drawing.
                /// </summary>
                if (Variables.E.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.w").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.W.Range, System.Drawing.Color.Cyan);
                }

                /// <summary>
                /// Loads the R drawing.
                /// </summary>
                if (Variables.R.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.e").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.E.Range, System.Drawing.Color.Red);
                }
            };
        }
    }
}
