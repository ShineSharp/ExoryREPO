using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Nasus
{
    using System;
    using System.Linq;
    using ExorAIO.Utilities;
    using Orbwalking = SFXTargetSelector.Orbwalking;

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
            /// The Q KillSteal Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&
                KillSteal.GetQDamage(Targets.Target) > Targets.Target.Health &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").GetValue<bool>())
            {
                Variables.Q.Cast();
            }

            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Targets.Target.IsValidTarget(Variables.E.Range) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").GetValue<bool>())
            {
                Variables.E.Cast(Variables.E.GetPrediction(Targets.Target).CastPosition);
            }

            /// <summary>
            /// The W Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                !Bools.IsImmobile(Targets.Target) &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.combo").GetValue<bool>())
            {
                Variables.W.CastOnUnit(Targets.Target);
            }

            /// <summary>
            /// The R Lifesaver Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                ObjectManager.Player.CountEnemiesInRange(700f) > 0 &&
                HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/4 &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.lifesaver").GetValue<bool>())
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
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").GetValue<bool>())
            {
                Variables.Q.Cast();
                ObjectManager.Player.IssueOrder(GameObjectOrder.AttackUnit, (Obj_AI_Hero)args.Target);
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
                Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.Combo &&
                (Targets.QMinion != null ||
                    Variables.Orbwalker.GetTarget().IsValid<Obj_AI_Turret>() ||
					GameObjects.Jungle.Contains((Obj_AI_Minion)Variables.Orbwalker.GetTarget())) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.farm").GetValue<bool>())
            {
                Variables.Q.Cast();
                ObjectManager.Player.IssueOrder(GameObjectOrder.AttackUnit, (Targets.QMinion != null ? Targets.QMinion : (Obj_AI_Base)Variables.Orbwalker.GetTarget()));
			}
        }
    }
}
