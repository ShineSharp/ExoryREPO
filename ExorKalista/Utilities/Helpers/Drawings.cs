using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.Common;

namespace ExorKalista
{
    using System;
    using System.Linq;
    using System.Drawing;
    using System.Collections.Generic;
    using Color = System.Drawing.Color;

    /// <summary>
    /// The drawings class.
    /// </summary>
    public class Drawings
    {
        /// <summary>
        /// Loads the range drawings.
        /// </summary>
        public static void InitializeDrawings()
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
        public static void InitializeDamage()
        {
            Drawing.OnDraw += delegate
            {
                ObjectManager.Get<Obj_AI_Base>()
                    .Where(h =>
                        !h.IsMe &&
                        h.IsValid() &&
                        h.IsHPBarRendered &&
                        Bools.IsPerfectRendTarget(h) &&
                        !h.CharData.BaseSkinName.Contains("Mini") &&
                        !h.CharData.BaseSkinName.Contains("Minion"))
                    .ForEach(unit =>
                    {
                        /// <summary>
                        /// Defines what HPBar Offsets it should display.
                        /// </summary>
                        var mobOffset = 
                            Variables.JungleHpBarOffsetList
                                .FirstOrDefault(x => x.BaseSkinName.Equals(unit.CharData.BaseSkinName));

                        var width = (int)(unit.Type.Equals(GameObjectType.obj_AI_Minion) ? mobOffset.Width : Variables.Width);
                        var height = (int)(unit.Type.Equals(GameObjectType.obj_AI_Minion) ? mobOffset.Height : Variables.Height);
                        var xOffset = (int)(unit.Type.Equals(GameObjectType.obj_AI_Minion) ? mobOffset.XOffset : Variables.XOffset);
                        var yOffset = (int)(unit.Type.Equals(GameObjectType.obj_AI_Minion) ? mobOffset.YOffset : Variables.YOffset);

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
