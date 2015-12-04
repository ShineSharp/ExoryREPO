namespace ExorAIO.Champions.DrMundo
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using ItemData = LeagueSharp.Common.Data.ItemData;
    using Orbwalking = SFXTargetSelector.Orbwalking;

    using ExorAIO.Utilities;

    public class Logics
    {
        public static void ExecuteAuto(EventArgs args)
        {
            /*
                Q Harass/Farm Logic;
                Q KillSteal Logic;
            */
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&
                ObjectManager.Player.HealthPercent > 10 &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>() &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo) ||

                (Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqautoharass").GetValue<bool>() && 
                    ObjectManager.Player.HealthPercent >= 50) ||

                (Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqks").GetValue<bool>() &&
                    Variables.Q.GetDamage(Targets.Target) > Targets.Target.Health))
            {
                Variables.Q.CastIfHitchanceEquals(Targets.Target, HitChance.VeryHigh, false);
            }

            /*
                W Combo Logic;
            */
            if (Variables.W.IsReady() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewcombo").GetValue<bool>() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo)
            {
                if ((!ObjectManager.Player.HasBuff("BurningAgony") &&
                        (Targets.Target.IsValidTarget(Variables.W.Range) &&
                        ObjectManager.Player.HealthPercent >= 35)) ||

                    (ObjectManager.Player.HasBuff("BurningAgony") && 
                        (!Targets.Target.IsValidTarget(Variables.W.Range) ||
                        ObjectManager.Player.HealthPercent < 35 ||
                        !Targets.Target.IsValid)))
                {
                    Variables.W.Cast();
                    return;
                }
                Variables.W.Cast();
            }
            
            /*
                W Farm Logic;
            */
            if (Variables.W.IsReady() &&
                ObjectManager.Player.HealthPercent >= 35 &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewfarm").GetValue<bool>() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear)
            {
                if ((!ObjectManager.Player.HasBuff("BurningAgony") &&
                        Targets.Minions.Count() >= 2 &&
                        ObjectManager.Player.Health >= Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewfarmhp").GetValue<Slider>().Value) ||

                    (ObjectManager.Player.HasBuff("BurningAgony") &&
                        (Targets.Minions.Count() < 2 ||
                        ObjectManager.Player.Health < Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewfarmhp").GetValue<Slider>().Value)))
                {
                    Variables.W.Cast();
                    return;
                }
                Variables.W.Cast();
            }

            /*
                R LifeSaver Logic;
            */
            if (Variables.R.IsReady() &&
                ObjectManager.Player.CountEnemiesInRange(700) > 0 &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userlifesaver").GetValue<bool>() &&
                    HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/3))
            {
                Variables.R.Cast();
            }
        }

        public static void ExecuteModes(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /*
                E Combo Logic;
            */
            if (Variables.E.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.E.Range) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useecombo").GetValue<bool>())
            {
                Orbwalking.ResetAutoAttackTimer();
                Variables.E.Cast();

                if (Variables.Menu.Item($"{Variables.MainMenuName}.miscsettings.useresetters").GetValue<bool>())
                {
                    if (ItemData.Tiamat_Melee_Only.GetItem().IsReady())
                    {
                        ItemData.Tiamat_Melee_Only.GetItem().Cast();
                    }

                    if (ItemData.Ravenous_Hydra_Melee_Only.GetItem().IsReady())
                    {
                        ItemData.Ravenous_Hydra_Melee_Only.GetItem().Cast();
                    }

                    if (ItemData.Titanic_Hydra_Melee_Only.GetItem().IsReady())
                    {
                        ItemData.Titanic_Hydra_Melee_Only.GetItem().Cast();
                    }
                }
            }
        }
    }
}
