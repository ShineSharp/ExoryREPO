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

    /// <summary>
    /// The logics class.
    /// </summary>
    public class Logics
    {
        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteAuto(EventArgs args)
        {
            /// <summary>
            /// The Q Combo Logic,
            /// The Q AutoHarass Logic,
            /// The Q Killsteal Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ObjectManager.Player.HealthPercent > 10 &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&
                Variables.R.GetPrediction(Targets.Target).Hitchance >= HitChance.VeryHigh &&

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>()) ||

                (ObjectManager.Player.HealthPercent >= 50 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqautoharass").GetValue<bool>()) ||

                (Variables.Q.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqks").GetValue<bool>())))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
            }

            /// <summary>
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewcombo").GetValue<bool>()))
            {
                if (((Targets.Target.IsValidTarget(Variables.W.Range) && ObjectManager.Player.HealthPercent >= 35) &&
                        !ObjectManager.Player.HasBuff("BurningAgony")) ||

                    ((!Targets.Target.IsValidTarget(Variables.W.Range) || ObjectManager.Player.HealthPercent < 35) &&
                        ObjectManager.Player.HasBuff("BurningAgony")))
                {
                    Variables.W.Cast();
                    return;
                }
                Variables.W.Cast();
            }
            
            /// <summary>
            /// The W Farm Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewfarm").GetValue<bool>()))
            {
                if ((!ObjectManager.Player.HasBuff("BurningAgony") && (Targets.Minions.Count() >= 2 &&
                        ObjectManager.Player.Health >= Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewfarmhp").GetValue<Slider>().Value)) ||

                    (ObjectManager.Player.HasBuff("BurningAgony") && (Targets.Minions.Count() < 2 ||
                        ObjectManager.Player.Health < Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewfarmhp").GetValue<Slider>().Value)))
                {
                    Variables.W.Cast();
                    return;
                }
                Variables.W.Cast();
            }

            /// <summary>
            /// The R Lifesaver Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                ObjectManager.Player.CountEnemiesInRange(700) > 0 &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userlifesaver").GetValue<bool>() &&
                    HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/3))
            {
                Variables.R.Cast();
            }
        }

        /// <summary>
        /// Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void ExecuteModes(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.E.Range) &&
                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useecombo").GetValue<bool>()))
            {
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
