namespace ExorAIO.Champions.Nasus
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
            /// <summary>
            /// Will not AA if can kill with Q.
            /// </summary>
            Variables.Orbwalker.SetAttack(Bools.ShouldAttack(Targets.Unit));
            
            /// <summary>
            /// The Smart W Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ((Obj_AI_Hero)Targets.Unit).IsValidTarget(Variables.W.Range) &&
                !Bools.IsImmobile((Obj_AI_Hero)Targets.Unit) &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewcombo").GetValue<bool>() && Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo))
            {
                Variables.W.CastOnUnit(Targets.Unit);
                return;
            }

            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ((Obj_AI_Hero)Targets.Unit).IsValidTarget(Variables.E.Range) &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useecombo").GetValue<bool>() && Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo))
            {
                Variables.E.Cast(Variables.E.GetPrediction(Targets.Unit).UnitPosition);
                
                if (((Obj_AI_Hero)Targets.Unit).IsValidTarget(Variables.Q.Range) && 
                    Variables.Menu.Item($"{Variables.MainMenuName}.miscsettings.useresetters").GetValue<bool>())
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

            /// <summary>
            /// The Q KillSteal Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ((Obj_AI_Hero)Targets.Unit).IsValidTarget(Variables.Q.Range) &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqks").GetValue<bool>() && Variables.Q.GetDamage(Targets.Unit) > Targets.Unit.Health))
            {
                Orbwalking.ResetAutoAttackTimer();
                Variables.Q.Cast();
                return;
            }

            /// <summary>
            /// The Q Farming Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>() && Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.Combo))
            {
                foreach (var unit in Targets.Units
                    .Where(
                        m => 
                            m.Health < Variables.Q.GetDamage(m)))
                {
                    //Orbwalking.ResetAutoAttackTimer();
                    Variables.Q.Cast();
                    return;
                }
            }
            
            /// <summary>
            /// The R Lifesaver Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                ObjectManager.Player.CountEnemiesInRange(700) > 0 &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userlifesaver").GetValue<bool>() &&
                    HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/4))
            {
                Variables.R.Cast();
            }
        }

        public static void ExecuteModes(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.Q.Range) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>())
            {
                Orbwalking.ResetAutoAttackTimer();
                Variables.Q.Cast();
            }
        }
    }
}
