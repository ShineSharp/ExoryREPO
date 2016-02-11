using LeagueSharp;
using LeagueSharp.Common;

namespace ExorJhin
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using SharpDX;
    using Orbwalking = SFXTargetSelector.Orbwalking;
    using SPrediction;

    /// <summary>
    /// The logics class.
    /// </summary>
    class Logics
    {
        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteR(EventArgs args)
        {
            /// <summary>
            /// The R Manager.
            /// </summary>
            Variables.Orbwalker.SetAttack(
                !Variables.R.Instance.Name.Equals("JhinRShot") &&
                !ObjectManager.Player.HasBuff("JhinPassiveReload"));
 
            Variables.Orbwalker.SetMovement(!Variables.R.Instance.Name.Equals("JhinRShot"));
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteAuto(EventArgs args)
        {
            /// <summary>
            /// The Q Logics.
            /// </summary>
            if (Variables.Q.IsReady())
            {
                /// <summary>
                /// The Q AutoHarass Logic.
                /// </summary>
                if (!Targets.Target.IsValidTarget(Variables.Q.Range) &&
                    Targets.Target.IsValidTarget(Variables.Q.Range + 400f) &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Mixed &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.harass").IsActive())
                {
                    foreach (var tgminion in from target in ObjectManager.Player.GetEnemiesInRange(Variables.Q.Range + 600f)
                        select Targets.Minions?
                            .FirstOrDefault(minion =>
                                minion.Distance(target) <= 200f &&
                                minion.IsValidTarget(Variables.Q.Range) &&
                                minion.Health < KillSteal.GetQDamage(minion))

                        into tgminion
                        where tgminion != null
                        select tgminion)
                    {
                        Variables.Q.CastOnUnit(tgminion);
                        return;
                    }
                }

                /// <summary>
                /// The Q KillSteal Logic,
                /// The Q Reload Logic.
                /// </summary>
                if (!ObjectManager.Player.IsWindingUp &&
                    Targets.Target.IsValidTarget(Variables.Q.Range) &&

                    ((KillSteal.GetQDamage(Targets.Target) > Targets.Target.Health &&
                        Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").IsActive()) ||

                    (ObjectManager.Player.HasBuff("JhinPassiveReload") &&
                        Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").IsActive())))
                {
                    Orbwalking.ResetAutoAttackTimer();
                    Variables.Q.CastOnUnit(Targets.Target);
                    return;
                }
            }

            /// <summary>
            /// The W KillSteal Logic.
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&

                ((KillSteal.GetWDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wspell.ks").IsActive()) ||

                (!ObjectManager.Player.IsWindingUp &&
                    Targets.Target.HasBuff("jhinespotteddebuff") &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wspell.combo").IsActive())))
            {
                Variables.W.SPredictionCast(Targets.Target, HitChance.High);
            }
            
            /// <summary>
            /// The E Immobile Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Bools.IsImmobile(Targets.Target) &&
                Targets.Target.IsValidTarget(Variables.E.Range) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.immobile").IsActive())
            {
                Variables.E.Cast(Targets.Target.Position);
            }

            /// <summary>
            /// The R Pre-Shot Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                !ObjectManager.Player.UnderTurret() &&
                !Targets.Target.IsValidTarget(1500f) &&
                !Variables.R.Instance.Name.Equals("JhinRShot") &&
                Targets.Target.IsValidTarget(Variables.R.Range) &&
                ObjectManager.Player.CountEnemiesInRange(1000f) == 0 &&
                KillSteal.GetRDamage(Targets.Target)*4 > Targets.Target.Health &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.auto").IsActive())
            {
                Variables.R.SPredictionCast(Targets.Target, HitChance.High);
            }
            
            /// <summary>
            /// The R Shot Logic.
            /// </summary> 
            if (Variables.R.IsReady() &&
                Bools.IsInsideRCone(Targets.Target) &&
                Variables.R.Instance.Name.Equals("JhinRShot"))
            {
                Variables.R.SPredictionCast(Targets.Target, HitChance.High);
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
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.Q.Range) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").IsActive())
            {
                Orbwalking.ResetAutoAttackTimer();
                Variables.Q.CastOnUnit((Obj_AI_Hero)args.Target);
                return;
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
            /// The Q LaneClear Logic,
            /// The Q JungleClear Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.farm").IsActive())
            {
                if (Targets.Minions?.Count() >= 3)
                {
                    Variables.Q.CastOnUnit(Targets.Minions.FirstOrDefault());
                }
                else if (Targets.JungleMinions.Any())
                {
                    Variables.Q.CastOnUnit(Targets.JungleMinions.FirstOrDefault());
                }
            }

            /// <summary>
            /// The W LaneClear Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.W.GetLineFarmLocation(Targets.Minions, Variables.W.Width).MinionsHit >= 4 &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.farm").IsActive())
            {
                Variables.W.Cast(Variables.W.GetLineFarmLocation(Targets.Minions, Variables.W.Width).Position);
            }
        }
    }
}
