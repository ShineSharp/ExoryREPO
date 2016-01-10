namespace ExorAIO.Champions.Varus
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using SharpDX;

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
            /// <summary>
            /// The AA Check.
            /// </summary>
            Variables.Orbwalker.SetAttack(!Variables.Q.IsCharging);

            /// <summary>
            /// The Long Q Logic
            /// The Q KillSteal Logic.
            /// </summary>
            if (Targets.Target.IsValidTarget(Variables.Q.Range) &&
                Targets.Target.Health < Variables.Q.GetDamage(Targets.Target) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqks").GetValue<bool>())
            {
                if (!Variables.Q.IsCharging)
                {
                    Variables.Q.StartCharging();
                }
                
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
            }

            /// <summary>
            /// The E KillSteal Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Targets.Target.IsValidTarget(Variables.E.Range) &&
            
                (Targets.Target.Health < Variables.E.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeks").GetValue<bool>()))
            {
                Variables.E.Cast(Variables.E.GetPrediction(Targets.Target).CastPosition);
                return;
            }
            
            /// <summary>
            /// The R KillSteal Logic,
            /// The R Combo Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Targets.Target.IsValidTarget(Variables.R.Range) &&

                ((Targets.Target.Health < Variables.R.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userks").GetValue<bool>()) ||

                (Targets.Target.CountEnemiesInRange(500) > 1 &&
                    !ObjectManager.Player.IsWindingUp &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.usercombo").GetValue<bool>())))
            {
                Variables.R.Cast(Variables.R.GetPrediction(Targets.Target).UnitPosition);
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
                !ObjectManager.Player.IsWindingUp &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>())
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
                return;
            }

            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                !Variables.Q.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(Variables.E.Range) &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useecombo").GetValue<bool>()))
            {
                Variables.E.Cast(Variables.E.GetPrediction(Targets.Target).CastPosition);
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteFarm(EventArgs args)
        {
            /// <summary>
            /// The Q Farm Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&

                (ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                    (Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).MinionsHit >= 3 ||
                        GameObjects.Jungle.Contains((Obj_AI_Minion)Variables.Orbwalker.GetTarget())) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>()))
            {
                if (!Variables.Q.IsCharging)
                {
                    Variables.Q.StartCharging();
                }

                Variables.Q.Cast(Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).Position);
            }
        }
    }
}
