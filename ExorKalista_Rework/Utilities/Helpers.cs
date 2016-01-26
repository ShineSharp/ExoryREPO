namespace ExorKalista
{
    using System;
    using System.Linq;
    using System.Drawing;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.SDK;
    using LeagueSharp.Common;

    using SharpDX;
    using SharpDX.Direct3D9;

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
                if (Variables.Menu.Item($"{Variables.MainMenuName}.drawings.q").GetValue<bool>())
                {
                    Drawing.DrawCircle(ObjectManager.Player.Position, Variables.Q.Range, System.Drawing.Color.Green);
                }

                /// <summary>
                /// Loads the E drawing.
                /// </summary>
                if (Variables.Menu.Item($"{Variables.MainMenuName}.drawings.e").GetValue<bool>())
                {
                    Drawing.DrawCircle(ObjectManager.Player.Position, Variables.E.Range, System.Drawing.Color.Cyan);
                }

                /// <summary>
                /// Loads the R drawing.
                /// </summary>
                if (Variables.Menu.Item($"{Variables.MainMenuName}.drawings.r").GetValue<bool>())
                {
                    Drawing.DrawCircle(ObjectManager.Player.Position, Variables.R.Range, System.Drawing.Color.Red);
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
                ObjectManager.Get<Obj_AI_Base>().Where(
                    h =>
                        !h.IsMe &&
                        h.IsValid() &&
                        h.IsHPBarRendered &&
                        !h.CharData.BaseSkinName.Contains("Minion") &&
                        Bools.IsPerfectRendTarget(h))
                .ForEach(
                    unit =>
                    {
                        /// <summary>
                        /// The default enemy HP bar offset.
                        /// </summary>
                        int XOffset = 10;
                        int YOffset = 20;
                        int Width = 103;
                        int Height = 8;

                        /// <summary>
                        /// Defines what HPBar Offsets it should display.
                        /// </summary>
                        var mobOffset = Variables.JungleHpBarOffsetList.FirstOrDefault(x => x.BaseSkinName == unit.CharData.BaseSkinName);

                        var width = (int)(unit.Type == GameObjectType.obj_AI_Minion ? mobOffset.Width : Width);
                        var height = (int)(unit.Type == GameObjectType.obj_AI_Minion ? mobOffset.Height : Height);
                        var xOffset = (int)(unit.Type == GameObjectType.obj_AI_Minion ? mobOffset.XOffset : XOffset);
                        var yOffset = (int)(unit.Type == GameObjectType.obj_AI_Minion ? mobOffset.YOffset : YOffset);

                        var barPos = unit.HPBarPosition;
                        barPos.X += xOffset;
                        barPos.Y += yOffset;

                        var drawEndXPos = barPos.X + width * (unit.HealthPercent / 100);
                        var drawStartXPos = barPos.X + (unit.Health > DamageManager.GetPerfectRendDamage(unit) ?
                            width * (((unit.Health - DamageManager.GetPerfectRendDamage(unit)) / unit.MaxHealth * 100) / 100) : 0);

                        Drawing.DrawLine(drawStartXPos, barPos.Y, drawEndXPos, barPos.Y, height, Bools.IsKillableByRend(unit) ? Color.Blue : Color.Orange);
                        Drawing.DrawLine(drawStartXPos, barPos.Y, drawStartXPos, barPos.Y + height + 1, 1, Color.Lime);
                    }
                );
            };
        }
    }
}
