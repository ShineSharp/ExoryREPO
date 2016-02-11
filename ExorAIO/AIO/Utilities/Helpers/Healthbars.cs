using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.Common;

namespace ExorAIO.Utilities
{
    using System.Linq;
    using Color = System.Drawing.Color;
    using KillSteal = ExorAIO.Champions.Kalista.KillSteal;

    /// <summary>
    /// The drawings class.
    /// </summary>
    class Healthbars
    {
        /// <summary>
        /// Loads the drawings.
        /// </summary>
        public static void Initialize()
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
                        var drawStartXPos = barPos.X + (unit.Health > KillSteal.GetPerfectRendDamage(unit) ?
                            width * (((unit.Health - KillSteal.GetPerfectRendDamage(unit)) / unit.MaxHealth * 100) / 100) : 0);

                        Drawing.DrawLine(drawStartXPos, barPos.Y, drawEndXPos, barPos.Y, height, unit.Health < KillSteal.GetPerfectRendDamage(unit) ? Color.Blue : Color.Orange);
                        Drawing.DrawLine(drawStartXPos, barPos.Y, drawStartXPos, barPos.Y + height + 1, 1, Color.Lime);
                    }
                );
            };
        }
    }
}
