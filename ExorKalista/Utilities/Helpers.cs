namespace ExorKalista
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using SharpDX;
    using SharpDX.Direct3D9;

    using Color = System.Drawing.Color;

    /// <summary>
    /// The Sentinel manager class.
    /// </summary>
    class SentinelManager
    {
        /// <summary>
        /// Gets all the sentinel locations.
        /// </summary>
        public static Vector2[] AllLocations =
        {
            SummonersRift.River.Baron,
            SummonersRift.River.Dragon,
            SummonersRift.Jungle.Red_RedBuff,
            SummonersRift.Jungle.Red_BlueBuff,
            SummonersRift.Jungle.Blue_BlueBuff,
            SummonersRift.Jungle.Blue_BlueBuff
        };

        /// <summary>
        /// Gets the possible sentinel locations.
        /// </summary>
        public static Vector2 GetPerfectSpot
        => 
            AllLocations.Where(
                loc =>
                    loc != null &&
                    loc.Distance(ObjectManager.Player) < Variables.W.Range)
            .OrderBy(
                h =>
                    h.Distance(ObjectManager.Player))
            .FirstOrDefault();
    }

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

        /// <summary>
        /// Sets the minimum necessary mana to use the W spell.
        /// </summary>
        public static int NeededWMana 
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.wmana").GetValue<Slider>().Value;

        /// <summary>
        /// Sets the minimum necessary mana to use the E spell.
        /// </summary>
        public static int NeededEMana
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.esettings.emana").GetValue<Slider>().Value;

        /// <summary>
        /// Sets the minimum necessary mana to use the R spell.
        /// </summary>
        public static int NeededRMana
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.rmana").GetValue<Slider>().Value;
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
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.e").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.E.Range, System.Drawing.Color.Cyan);
                }

                /// <summary>
                /// Loads the R drawing.
                /// </summary>
                if (Variables.R.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.r").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.R.Range, System.Drawing.Color.Red);
                }
            };
        }

        /// <summary>
        /// Loads the damage drawings.
        /// </summary>
        public static void LoadDamage()
        {
            Drawing.OnDraw += delegate
            {
                if (Variables.Menu.Item($"{Variables.MainMenuName}.drawings.edmg").GetValue<bool>())
                {
                    const int XOffset = 10;
                    const int YOffset = 20;
                    const int Width = 103;
                    const int Height = 8;

                    foreach (var unit in HeroManager.Enemies
                        .Where(
                            h => h.IsValid &&
                            h.IsHPBarRendered))
                    {
                        var barPos = unit.HPBarPosition;

                        var percentHealthAfterDamage = Math.Max(0, unit.Health - Variables.GetPerfectRendDamage(unit))/unit.MaxHealth;
                        var yPos = barPos.Y + YOffset;
                        var xPosDamage = barPos.X + XOffset + Width*percentHealthAfterDamage;
                        var xPosCurrentHp = barPos.X + XOffset + Width*unit.Health/unit.MaxHealth;

                        var differenceInHp = xPosCurrentHp - xPosDamage;
                        var pos1 = barPos.X + 9 + 107*percentHealthAfterDamage;

                        for (var i = 0; i < differenceInHp; i++)
                        {
                            Drawing.DrawLine(
                                pos1 + i,
                                yPos,
                                pos1 + i,
                                yPos + Height,
                                1,
                                Variables.GetPerfectRendDamage(unit) > unit.Health ?
                                    Color.Blue :
                                    Color.Orange
                            );
                        }
                    }
                }
            };
        }
    }
}
