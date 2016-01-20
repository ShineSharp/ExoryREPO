namespace ExorKalista
{
    using System;
    using System.Linq;
    using System.Drawing;
    using System.Collections.Generic;

    using LeagueSharp;
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
                if (Variables.Q.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.q").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.Q.Range, System.Drawing.Color.Green, 1);
                }

                /// <summary>
                /// Loads the E drawing.
                /// </summary>
                if (Variables.E.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.e").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.E.Range, System.Drawing.Color.Cyan, 1);
                }

                /// <summary>
                /// Loads the R drawing.
                /// </summary>
                if (Variables.R.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.r").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.R.Range, System.Drawing.Color.Red, 1);
                }
            };
        }

        /// <summary>
        /// The enemy HP bar offset.
        /// </summary>
        private static readonly int XOffset = 10;
        private static readonly int YOffset = 20;
        private static readonly int Width = 103;
        private static readonly int Height = 8;

        /// <summary>
        /// The jungle HP bar offset.
        /// </summary>      
        internal class JungleHpBarOffset
        {
            internal string BaseSkinName;
            internal int Height;
            internal int Width;
            internal int XOffset;
            internal int YOffset;
        }

        /// <summary>
        /// The jungle HP bar offset list.
        /// </summary>
        internal static readonly List<JungleHpBarOffset> JungleHpBarOffsetList = new List<JungleHpBarOffset>
        {
            new JungleHpBarOffset{BaseSkinName = "SRU_Dragon", Width = 140, Height = 4, XOffset = 12, YOffset = 24},
            new JungleHpBarOffset{BaseSkinName = "SRU_Baron", Width = 190, Height = 10, XOffset = 16, YOffset = 24},
            new JungleHpBarOffset{BaseSkinName = "SRU_RiftHerald", Width = 139, Height = 6, XOffset = 12, YOffset = 22},
            new JungleHpBarOffset{BaseSkinName = "SRU_Red", Width = 139, Height = 4, XOffset = 12, YOffset = 24},
            new JungleHpBarOffset{BaseSkinName = "SRU_RedMini", Width = 49, Height = 2, XOffset = 1, YOffset = 5},
            new JungleHpBarOffset{BaseSkinName = "SRU_Blue", Width = 139, Height = 4, XOffset = 12, YOffset = 24},
            new JungleHpBarOffset{BaseSkinName = "SRU_BlueMini", Width = 49, Height = 2, XOffset = 1, YOffset = 5},
            new JungleHpBarOffset{BaseSkinName = "SRU_BlueMini2", Width = 49, Height = 2, XOffset = 1, YOffset = 5},
            new JungleHpBarOffset{BaseSkinName = "SRU_Gromp", Width = 86, Height = 2, XOffset = 1, YOffset = 7},
            new JungleHpBarOffset{BaseSkinName = "Sru_Crab", Width = 61, Height = 2, XOffset = 1, YOffset = 5},
            new JungleHpBarOffset{BaseSkinName = "SRU_Krug", Width = 79, Height = 2, XOffset = 1, YOffset = 7},
            new JungleHpBarOffset{BaseSkinName = "SRU_KrugMini", Width = 55, Height = 2, XOffset = 1, YOffset = 5},
            new JungleHpBarOffset{BaseSkinName = "SRU_Razorbeak", Width = 74, Height = 2, XOffset = 1, YOffset = 7},
            new JungleHpBarOffset{BaseSkinName = "SRU_RazorbeakMini", Width = 49, Height = 2, XOffset = 1, YOffset = 5},
            new JungleHpBarOffset{BaseSkinName = "SRU_Murkwolf", Width = 74, Height = 2, XOffset = 1, YOffset = 7},
            new JungleHpBarOffset{BaseSkinName = "SRU_MurkwolfMini", Width = 55, Height = 2, XOffset = 1, YOffset = 5}
        };

        /// <summary>
        /// Loads the damage drawings.
        /// </summary>
        public static void LoadDamage()
        {
            Drawing.OnDraw += delegate
            {
                foreach (var unit in ObjectManager.Get<Obj_AI_Base>()
                    .Where(
                        h =>
                            h.IsValid &&
                            h.IsHPBarRendered))
                {
                    int width, height, xOffset, yOffset;

                    if (unit is Obj_AI_Hero)
                    {
                        width = Width;
                        height = Height;
                        xOffset = XOffset;
                        yOffset = YOffset;
                    }
                    else
                    {
                        var mobOffset = JungleHpBarOffsetList.FirstOrDefault(x => x.BaseSkinName == unit.CharData.BaseSkinName);
                        if (mobOffset != null)
                        {
                            width = mobOffset.Width;
                            height = mobOffset.Height;
                            xOffset = mobOffset.XOffset;
                            yOffset = mobOffset.YOffset;
                        }
                        else
                        {
                            return;
                        }
                    }

                    if (DamageManager.GetPerfectRendDamage(unit) < 0)
                    {
                        return;
                    }

                    var barPos = unit.HPBarPosition;
                    barPos.X += xOffset;
                    barPos.Y += yOffset;

                    var hpPercent = unit.HealthPercent;
                    var hpPercentAfterDamage = (unit.Health - DamageManager.GetPerfectRendDamage(unit)) / unit.MaxHealth * 100;
                    var drawStartXPos = barPos.X + width * (hpPercentAfterDamage / 100);
                    var drawEndXPos = barPos.X + width * (hpPercent / 100);

                    if (unit.Health < DamageManager.GetPerfectRendDamage(unit))
                    {
                        drawStartXPos = barPos.X;
                    }

                    Drawing.DrawLine(drawStartXPos, barPos.Y, drawEndXPos, barPos.Y, height, Color.FromArgb(170, Bools.IsKillableByRend(unit) ? Color.Blue : Color.Orange));
                    Drawing.DrawLine(drawStartXPos, barPos.Y, drawStartXPos, barPos.Y + height + 1, 1, Color.Lime);
                }
            };
        }
    }
}
