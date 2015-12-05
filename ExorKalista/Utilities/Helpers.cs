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
        public static int NeededQMana => Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.qmana") != null ? Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.qmana").GetValue<Slider>().Value : 0;
        public static int NeededWMana => Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.wmana") != null ? Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.wmana").GetValue<Slider>().Value : 0;
        public static int NeededEMana => Variables.Menu.Item($"{Variables.MainMenuName}.esettings.emana") != null ? Variables.Menu.Item($"{Variables.MainMenuName}.esettings.emana").GetValue<Slider>().Value : 0;
        public static int NeededRMana => Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.rmana") != null ? Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.rmana").GetValue<Slider>().Value : 0;
    }

    /// <summary>
    /// The drawings class.
    /// </summary>
    public class Drawings
    {
        public static void Load()
        {
            Drawing.OnDraw += delegate
            {
                if (Variables.Q != null &&
                    Variables.Q.IsReady() &&
                    Variables.Q.Range != 0 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.q") != null &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.q").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.Q.Range, System.Drawing.Color.Green);
                }

                if (Variables.W != null &&
                    Variables.W.IsReady() &&
                    Variables.W.Range != 0 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.w") != null &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.w").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.W.Range, System.Drawing.Color.Purple);
                }

                if (Variables.E != null &&
                    Variables.E.IsReady() &&
                    Variables.E.Range != 0)
                {
                    if (Variables.Menu.Item($"{Variables.MainMenuName}.drawings.e") != null &&
                        Variables.Menu.Item($"{Variables.MainMenuName}.drawings.e").GetValue<bool>())
                    {
                        Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.E.Range, System.Drawing.Color.Cyan);
                    }

                    if (Variables.Menu.Item($"{Variables.MainMenuName}.drawings.edmg").GetValue<bool>())
                    {
                        foreach (var target in HeroManager.Enemies
                            .Where(
                                x => 
                                    !x.IsDead &&
                                    x.IsVisible &&
                                    x.HasBuff("kalistaexpungemarker")))
                        {
                            Drawing.DrawText(
                                Drawing.WorldToScreen(target.Position).X,
                                Drawing.WorldToScreen(target.Position).Y - 100,
                                
                                (Variables.E.GetDamage(target)/target.Health)*100 >= 100 ?
                                    Color.Red :
                                    Color.GreenYellow,

                                ((Variables.E.GetDamage(target)/target.Health)*100).ToString("0.0")
                            );
                        }
                        
                        foreach (var miniontarget in MinionManager.GetMinions(Variables.Q.Range, MinionTypes.All, MinionTeam.Neutral)
                            .Where(
                                y => 
                                    !y.IsDead &&
                                    y.IsVisible &&
                                    y.HasBuff("kalistaexpungemarker")))
                        {
                            Drawing.DrawText(
                                Drawing.WorldToScreen(miniontarget.Position).X,
                                Drawing.WorldToScreen(miniontarget.Position).Y - 100,

                                (Targets.Dragon != null ?
                                    Variables.GetDragonReduction(miniontarget) :
                                    Variables.GetBaronReduction(miniontarget) /miniontarget.Health)*100 >= 100 ?
                                    Color.Red :
                                    Color.GreenYellow,

                                ((Variables.E.GetDamage(miniontarget)/miniontarget.Health)*100).ToString("0.0")
                            );
                        }
                    }
                }

                if (Variables.R != null &&
                    Variables.R.IsReady() &&
                    Variables.R.Range != 0 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.r") != null &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.r").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.R.Range, System.Drawing.Color.Red);
                }
            };
        }
    }
}
