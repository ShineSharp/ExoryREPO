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
        /// Loads the damage drawings.
        /// </summary>
        public static void LoadDamage()
        {
            Drawing.OnDraw += delegate
            {
                if (Variables.Menu.Item($"{Variables.MainMenuName}.drawings.edmg").GetValue<bool>())
                {
                    foreach (var unit in GameObjects.Enemy
                        .Where(
                            h => h.IsValid &&
                            h.IsHPBarRendered &&
                            h.HasBuff("kalistaexpungemarker") &&
                            GameObjects.Jungle.Contains(h)))
                    {
                        if (Variables.JungleHpBarOffsetList.FirstOrDefault(x => x.BaseSkinName.Equals(unit.CharData.BaseSkinName)) != null)
                        {
                            Variables.Width = Variables.JungleHpBarOffsetList.FirstOrDefault(x => x.BaseSkinName.Equals(unit.CharData.BaseSkinName)).Width;
                            Variables.Height = Variables.JungleHpBarOffsetList.FirstOrDefault(x => x.BaseSkinName.Equals(unit.CharData.BaseSkinName)).Height;
                            Variables.XOffset = Variables.JungleHpBarOffsetList.FirstOrDefault(x => x.BaseSkinName.Equals(unit.CharData.BaseSkinName)).XOffset;
                            Variables.YOffset = Variables.JungleHpBarOffsetList.FirstOrDefault(x => x.BaseSkinName.Equals(unit.CharData.BaseSkinName)).YOffset;
                        }
                        
                        var HPBar = unit.HPBarPosition;
                        {
                            HPBar.X += Variables.XOffset;
                            HPBar.Y += Variables.YOffset;
                        }

                        var drawStartXPos = unit.HPBarPosition.X + Variables.Width * ((unit.Health - DamageManager.GetPerfectRendDamage(unit)) / unit.MaxHealth * 100 / 100);
                        var drawEndXPos = unit.HPBarPosition.X + Variables.Width * (unit.HealthPercent / 100);

                        Drawing.DrawLine(drawStartXPos, unit.HPBarPosition.Y, drawEndXPos, unit.HPBarPosition.Y, Variables.Height, !Bools.IsKillableByRend(unit) ? Color.Orange : Color.Blue);
                        Drawing.DrawLine(drawStartXPos, unit.HPBarPosition.Y, drawStartXPos, unit.HPBarPosition.Y + Variables.Height + 1, 1, Color.Lime);
                    }
                }
            };
        }
    }
}
