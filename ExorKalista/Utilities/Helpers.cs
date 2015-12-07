namespace ExorKalista
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
        public static int NeededQMana => Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.qmana").GetValue<Slider>().Value;
        public static int NeededWMana => Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.wmana").GetValue<Slider>().Value;
        public static int NeededEMana => Variables.Menu.Item($"{Variables.MainMenuName}.esettings.emana").GetValue<Slider>().Value;
        public static int NeededRMana => Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.rmana").GetValue<Slider>().Value;
    }

    /// <summary>
    /// The drawings class.
    /// </summary>
    public class Drawings
    {
        public static void LoadRange()
        {
            Drawing.OnDraw += delegate
            {
                if (Variables.Q.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.q").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.Q.Range, System.Drawing.Color.Green);
                }

                if (Variables.E.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.e").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.E.Range, System.Drawing.Color.Cyan);
                }

                if (Variables.R.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.r").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.R.Range, System.Drawing.Color.Red);
                }
            };
        }
    
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
                                    Color.Red
                            );
                        }
                    }
                }
            };
        }
    }
}
