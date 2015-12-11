using System;
using System.Collections.Generic;

using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Utilities
{
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
            Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.qmana") != null ?
                Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.qmana").GetValue<Slider>().Value :
                0;

        /// <summary>
        /// Sets the minimum necessary mana to use the W spell.
        /// </summary>
        public static int NeededWMana 
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.wmana") != null ?
                Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.wmana").GetValue<Slider>().Value :
                0;

        /// <summary>
        /// Sets the minimum necessary mana to use the E spell.
        /// </summary>
        public static int NeededEMana
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.esettings.emana") != null ?
                Variables.Menu.Item($"{Variables.MainMenuName}.esettings.emana").GetValue<Slider>().Value :
                0;

        /// <summary>
        /// Sets the minimum necessary mana to use the R spell.
        /// </summary>
        public static int NeededRMana
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.rmana") != null ?
                Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.rmana").GetValue<Slider>().Value :
                0;
    }

    /// <summary>
    /// The drawings class.
    /// </summary>
    public class Drawings
    {
        /// <summary>
        /// Loads the drawings.
        /// </summary>
        public static void Load()
        {
            Drawing.OnDraw += delegate
            {
                /// <summary>
                /// Loads the Q drawing.
                /// </summary>
                if (Variables.Q != null &&
                    Variables.Q.IsReady() &&
                    Variables.Q.Range != 0 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.q") != null &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.q").GetValue<bool>())
                {
                    if (ObjectManager.Player.ChampionName.Equals("Jinx") &&
                        Bools.IsUsingFishBones())
                    {
                        Render.Circle.DrawCircle(ObjectManager.Player.Position, ObjectManager.Player.BoundingRadius*2 + 525f, System.Drawing.Color.Green, 1);
                        return;
                    }

                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.Q.Range, System.Drawing.Color.Green, 1);
                }

                /// <summary>
                /// Loads the W drawing.
                /// </summary>
                if (Variables.W != null &&
                    Variables.W.IsReady() &&
                    Variables.W.Range != 0 &&
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
                    Variables.E.Range != 0 &&
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
                    Variables.R.Range != 0 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.r") != null &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.r").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.R.Range, System.Drawing.Color.Red, 1);
                }
            };
        }
    }
}
