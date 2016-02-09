using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Draven
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
        public static void ExecuteQ(EventArgs args)
        {
            /// <summary>
            /// The Q Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ObjectManager.Player.IsWindingUp &&
                ObjectManager.Player.GetBuffCount("dravenspinningattack") < 2 &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.auto").IsActive())
            {
                Variables.Q.Cast();
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecutePathing(EventArgs args)
        {
            if (Variables.Menu.Item($"{Variables.MainMenuName}.misc.path").IsActive())
            {
                if (GameObjects.AllGameObjects
                        .Any(x => x.Name.Equals("Draven_Base_Q_reticle_self.troy")) &&

                    ObjectManager.Player.Distance(GameObjects.AllGameObjects
                        .Find(x => x.Name.Equals("Draven_Base_Q_reticle_self.troy")).Position) <= 110f)
                {
                    ObjectManager.Player.IssueOrder(GameObjectOrder.MoveTo, ObjectManager.Player.Position);
                    Variables.Orbwalker.SetMovement(false);
                }
                else
                {
                    Variables.Orbwalker.SetMovement(Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.None);
                }
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteAuto(EventArgs args)
        {
            /// <summary>
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Targets.Target.IsValidTarget(800f) &&
                !ObjectManager.Player.HasBuff("dravenfurybuff") &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.combo").IsActive())
            {
                Variables.W.Cast();
            }

            /// <summary>
            /// The E KillSteal Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Targets.Target.IsValidTarget(Variables.E.Range) &&
                Targets.Target.Health < Variables.E.GetDamage(Targets.Target) &&
                !Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.ks").IsActive())
            {
                Variables.E.SPredictionCast(Targets.Target, HitChance.VeryHigh);
            }

            /// <summary>
            /// The R KillSteal Logic,
            /// The R Combo Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(Variables.R.Range) &&
                
                ((Targets.Target.Health < Variables.R.GetDamage(Targets.Target)/2 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.ks").IsActive()) ||
                
                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    ObjectManager.Player.CountEnemiesInRange(Orbwalking.GetRealAutoAttackRange(null)) >= 2 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.combo").IsActive() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.whitelist.{Targets.Target.ChampionName.ToLower()}").IsActive())))
            {
                Variables.R.SPredictionCast(Targets.Target, HitChance.VeryHigh);
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
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").IsActive())
            {
                Variables.E.SPredictionCast((Obj_AI_Hero)args.Target, HitChance.VeryHigh);
            }
        }
    }
}
