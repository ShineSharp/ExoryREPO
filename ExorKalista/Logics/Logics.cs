using LeagueSharp;
using LeagueSharp.Common;

namespace ExorKalista
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
        public static void ExecuteAuto(EventArgs args)
        {
            /// <summary>
            /// The Q KillSteal Logic,
            /// The Q Immobile Harass Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&
                Variables.Q.GetSPrediction(Targets.Target).HitChance >= HitChance.High &&

                ((Targets.Target.Health < Variables.Q.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").IsActive()) ||

                (Bools.IsImmobile(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.immobile").IsActive())))
            {
                Variables.Q.Cast(Variables.Q.GetSPrediction(Targets.Target).UnitPosition.To3D());
            }

            /// <summary>
            /// The E before Dying Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= 0 &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.death").IsActive())
            {
                Variables.E.Cast();
            }
            
            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").IsActive() &&
                HeroManager.Enemies.Any(h => Bools.IsKillableByRend(h) && Bools.IsPerfectRendTarget(h)))
            {
                Variables.E.Cast();
            }

            /// <summary>
            /// The R Lifesaver Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Variables.SoulBound != null &&
                Variables.SoulBound.CountEnemiesInRange(800) > 0 &&
                Variables.SoulBound.IsValidTarget(Variables.R.Range, false) &&
                HealthPrediction.GetHealthPrediction(Variables.SoulBound, (int)(1500 + Game.Ping / 2f)) <= 0 &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userlifesaver").IsActive())
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
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.Q.Range) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Q.GetSPrediction((Obj_AI_Hero)args.Target).HitChance >= HitChance.High &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").IsActive())
            {
                Variables.Q.Cast(Variables.Q.GetSPrediction((Obj_AI_Hero)args.Target).UnitPosition.To3D());
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
                ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                Targets.Minions.Count(m => m.Health < Variables.Q.GetDamage(m)) > 2 &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).MinionsHit > 2 &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.farm").IsActive())
            {
                Variables.Q.Cast(Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).Position);
            }

            /// <summary>
            /// The E Farm Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededEMana &&
                Targets.Minions.Count(x => Bools.IsPerfectRendTarget(x) && Bools.IsKillableByRend(x)) >= 2 &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.farm").IsActive())
            {
                Variables.E.Cast();
            }

            /// <summary>
            /// The E Harass Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededEMana &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.harass").IsActive())
            {
                if (Targets.Minions
                    .Any(h =>
                        Bools.IsKillableByRend(h) &&
                        Bools.IsPerfectRendTarget(h)) &&
                    (Targets.HarassableTargets.Count() > 1 ||
                    (Targets.HarassableTargets.Count() == 1 &&
                        Variables.Menu.Item($"{Variables.MainMenuName}.espell.whitelist.{(Targets.HarassableTargets.FirstOrDefault()).ChampionName.ToLower()}").IsActive())))
                {
                    Variables.E.Cast();
                }
            }

            /// <summary>
            /// The E against Monsters Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.jgc").IsActive())
            {
                if (GameObjects.Jungle
                    .Any(m =>
                        Bools.IsKillableByRend(m) &&
                        Bools.IsPerfectRendTarget(m) &&
                        (!m.CharData.BaseSkinName.Contains("Mini") ||
                            m.CharData.BaseSkinName.Contains("Crab"))))
                {
                    Variables.E.Cast();
                }
            }
        }
    }
}
