using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Quinn
{
    using System;
    using System.Linq;
    using ExorAIO.Utilities;
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
        public static void ExecuteRAuto(EventArgs args)
        {
            /// <summary>
            /// The R Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                ObjectManager.Player.InFountain() &&
                Variables.R.Instance.Name.Equals("QuinnR"))
            {
                Variables.R.Cast();
            }

            Variables.Orbwalker.SetAttack(!Variables.R.Instance.Name.Equals("quinnrfinale")); 
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteRTarget(EventArgs args)
        {
            if (Targets.Target.CountEnemiesInRange(1000f) > 1 &&
                Variables.R.Instance.Name.Equals("quinnrfinale"))
            {
                Variables.R.Cast();
            }
        }
        
        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteAuto(EventArgs args)
        {
            /// <summary>
            /// The Q Combo Logic,
            /// The Q KillSteal Logic,
            /// The Q Against Impaired Targets Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&

                ((Targets.Target.Health < Variables.Q.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").IsActive()) ||

                (Bools.IsImmobile(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.immobile").IsActive())))
            {
                Variables.Q.SPredictionCast(Targets.Target, HitChance.Low);
            }

            /// <summary>
            /// The W Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
               !ObjectManager.Player.IsRecalling() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.auto").IsActive())
            {
                foreach (Obj_AI_Hero enemy in GameObjects.EnemyHeroes
                    .Where(x =>
                        x.IsValid &&
                        !x.IsVisible &&
                        !x.IsDead &&
                        x.Distance(ObjectManager.Player.ServerPosition) < Variables.W.Range))
                {
                    Variables.W.Cast();
                }
            }

            /// <summary>
            /// The E KillSteal Logic.
            /// The E Valor Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(Variables.E.Range) &&

                ((Targets.Target.Health < Variables.E.GetDamage(Targets.Target) + ObjectManager.Player.GetAutoAttackDamage(Targets.Target)*2 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.espell.ks").IsActive()) ||

                (Variables.R.Instance.Name.Equals("quinnrfinale") &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").IsActive())))
            {
                Variables.E.CastOnUnit(Targets.Target);
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
                !((Obj_AI_Hero)args.Target).HasBuff("quinnw") &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").IsActive())
            {
                Variables.Q.SPredictionCast((Obj_AI_Hero)args.Target, HitChance.Low);
                return;
            }

            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                !((Obj_AI_Hero)args.Target).HasBuff("quinnw") &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").IsActive())
            {
                Variables.E.CastOnUnit((Obj_AI_Hero)args.Target);
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
                if (Variables.Q.GetCircularFarmLocation(Targets.Minions, Variables.Q.Width).MinionsHit >= 3)
                {
                    Variables.Q.Cast(Variables.Q.GetCircularFarmLocation(Targets.Minions, Variables.Q.Width).Position);
                }
                else if (Targets.JungleMinions.Any())
                {
                    Variables.Q.Cast((Targets.JungleMinions.FirstOrDefault()).Position);
                }
            }
        }
    }
}
