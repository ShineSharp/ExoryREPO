namespace ExorAIO.Champions.Tristana
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using ExorAIO.Utilities;

    using Orbwalking = SFXTargetSelector.Orbwalking;

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
            var RangeIncreaser = 7 * ObjectManager.Player.Level;

            /// <summary>
            /// Sets the target.
            /// </summary>
            if (Targets.ETarget != null &&
                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo && Targets.ETarget.IsValid<Obj_AI_Hero>()) ||
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear && Targets.ETarget.IsValid<Obj_AI_Base>())
            {
                Variables.Orbwalker.ForceTarget(Targets.ETarget);
            }

            /// <summary>
            /// The R KillSteal Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Targets.Target != null &&
                !ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(Variables.R.Range + RangeIncreaser) &&
                KillSteal.Damage(Targets.Target) > Targets.Target.Health &&
            
                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userks").GetValue<bool>()))
            {
                Orbwalking.ResetAutoAttackTimer();
                Variables.R.CastOnUnit(Targets.Target);
            }

            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Targets.Target.IsValidTarget(Variables.E.Range + RangeIncreaser) &&
                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeauto").GetValue<bool>() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.ewhitelist.{Targets.Target.ChampionName.ToLower()}").GetValue<bool>())))
            {
                Variables.E.CastOnUnit(Targets.Target);
            }
        }

        /// <summary>
        /// Called before the next aa is fired.
        /// </summary>
        /// <param name="args">The <see cref="Orbwalking.BeforeAttackEventArgs"/> instance containing the beforeattack data.</param>
        public static void ExecuteBeforeAttack(Orbwalking.BeforeAttackEventArgs args)
        {
            var RangeIncreaser = 7 * ObjectManager.Player.Level;

            /// <summary>
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                (Bools.IsCharged((Obj_AI_Base)args.Target) || !Variables.E.IsReady()) &&

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqauto").GetValue<bool>()) ||

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear && 
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>())))
            {
                Variables.Q.Cast();
            }

            /// <summary>
            /// The E Farm Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededEMana &&
                    ((Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useebuildings").GetValue<bool>() && args.Target.IsValid<Obj_AI_Turret>()) ||
                    (Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useefarm").GetValue<bool>() &&
                        GameObjects.Minions.Count(
                            units =>
                                units.IsValidTarget(Variables.E.Range + RangeIncreaser) &&
                                units.Distance(Targets.Minions.FirstOrDefault(), false) < 150f) > 2))))
            {
                Variables.E.CastOnUnit((Obj_AI_Base)args.Target);
            }
        }
    }
}
