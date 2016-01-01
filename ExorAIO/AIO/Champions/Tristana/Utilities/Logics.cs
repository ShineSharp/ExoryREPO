namespace ExorAIO.Champions.Tristana
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using ExorAIO.Utilities;

    using Orbwalking = SFXTargetSelector.Orbwalking;
    using TargetSelector = SFXTargetSelector.TargetSelector;

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
            /// The target priority.
            /// </summary>
            TargetSelector.Weights.GetItem("low-health").ValueFunction = hero => hero.Health - KillSteal.Damage(hero);
            TargetSelector.Weights.GetItem("low-health").Tooltip = "Low Health (Health - Combo Damage) = Higher Weight";
            TargetSelector.Weights.Register(
                new TargetSelector.Weights.Item(
                    "e-charge", "E Charge", 10, false, hero => hero.HasBuff("TristanaECharge") ? 1 : 0,
                    "Has E Charge = Higher Weight"));

            /// <summary>
            /// The R KillSteal Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                Bools.HasNoProtection(Targets.Target) &&
                Targets.Target.IsValidTarget(Variables.R.Range) &&
                KillSteal.Damage(Targets.Target) > Targets.Target.Health &&
            
                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userks").GetValue<bool>()))
            {
                Variables.R.CastOnUnit(Targets.Target);
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteModes(EventArgs args)
        {
            /// <summary>
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                (Bools.IsCharged(Targets.Target) || !Variables.E.IsReady()) &&

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqauto").GetValue<bool>()) ||

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>())))
            {
                Variables.Q.Cast();
            }

            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                Bools.HasNoProtection(Targets.Target) &&
                Targets.Target.IsValidTarget(Variables.E.Range) &&
                
                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeauto").GetValue<bool>() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.ewhitelist.{Targets.Target.ChampionName.ToLower()}").GetValue<bool>()))
            {
                Variables.E.CastOnUnit(Targets.Target);
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteFarm(EventArgs args)
        {
            /// <summary>
            /// The E Farm Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededEMana &&

                    ((Variables.Orbwalker.GetTarget().IsValid<Obj_AI_Turret>() &&
                        Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useebuildings").GetValue<bool>()) ||

                    ((GameObjects.Minions.Count(
                        units =>
                            units.IsValidTarget(Variables.E.Range) &&
                            units.Distance(Targets.Minions.FirstOrDefault(), false) < 150f) > 2) ||
                        GameObjects.JungleLarge.Contains((Obj_AI_Minion)Variables.Orbwalker.GetTarget()) ||
                        GameObjects.JungleLegendary.Contains((Obj_AI_Minion)Variables.Orbwalker.GetTarget())) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useefarm").GetValue<bool>())))
            {
                Variables.E.CastOnUnit((Obj_AI_Base)Variables.Orbwalker.GetTarget());
            }
        }
    }
}
