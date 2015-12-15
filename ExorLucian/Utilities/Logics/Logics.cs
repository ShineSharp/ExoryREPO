namespace ExorLucian
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

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
            /// The Q Logics.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range + 600f))
            {
                /// <summary>
                /// The Q AutoHarass Logic.
                /// </summary>
                if (ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqautoharass").GetValue<bool>())
                {
                    foreach (var obj in Targets.Minions
                        .Where(
                            obj =>
                                !(obj is Obj_AI_Hero) &&
                                !(new Geometry.Polygon.Rectangle(ObjectManager.Player.Position, ObjectManager.Player.Position.Extend(obj.Position, Variables.Q.Range + 600f), Variables.Q.Width)
                                .IsOutside(Variables.Q.GetPrediction(Targets.Target).UnitPosition.To2D()))))
                    {
                        Variables.Q.CastOnUnit(obj);
                        break;
                    }
                }

                /// <summary>
                /// The Q KillSteal Logic.
                /// </summary>
                if (Targets.Target.IsValidTarget(Variables.Q.Range) &&

                    (Variables.Q.GetDamage(Targets.Target) > Targets.Target.Health &&
                        Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqks").GetValue<bool>()))
                {
                    Variables.Q.CastOnUnit(Targets.Target);
                }
            }

            /// <summary>
            /// The W KillSteal Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&
                Variables.W.GetPrediction(Targets.Target).Hitchance >= HitChance.High &&
                                
                (Variables.W.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewks").GetValue<bool>()))
            {
                Variables.W.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
            }
            
            /// <summary>
            /// The E Gap Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                (Targets.Target.IsValidTarget(Variables.E.Range) && !Targets.Target.IsValidTarget(Variables.Q.Range)) &&
                ObjectManager.Player.CountEnemiesInRange(1500) < 2 &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeauto").GetValue<bool>()))
            {
                Variables.E.Cast(Game.CursorPos);
                return;
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
                ObjectManager.Player.CountEnemiesInRange(1500) < 2 &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeauto").GetValue<bool>()))
            {
                Variables.E.Cast(Game.CursorPos);
                return;
            }

            /// <summary>
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.Q.Range) &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>()))
            {
                Variables.Q.CastOnUnit(Targets.Target);
                return;
            }

            /// <summary>
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.W.Range) &&
                Variables.W.GetPrediction(Targets.Target).Hitchance >= HitChance.High &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewcombo").GetValue<bool>()))
            {
                Variables.W.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
            }
        }

        /// <summary>
        /// Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void ExecuteFarm(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// The Q Farm Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&

                (ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                    Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).MinionsHit >= 3 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>()))
            {
                Variables.Q.Cast(Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).Position);
            }
        }
    }
}
