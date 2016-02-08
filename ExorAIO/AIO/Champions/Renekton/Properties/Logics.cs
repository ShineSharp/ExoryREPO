using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Renekton
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
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                !ObjectManager.Player.HasBuff("renektonsliceanddicedelay") &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                (!Targets.Target.UnderTurret() || Targets.Target.HealthPercent < 10) &&
                Targets.Target.IsValidTarget(Variables.E.Range + Orbwalking.GetRealAutoAttackRange(Targets.Target)) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").IsActive())
            {
                Variables.E.Cast(Targets.Target.Position);
            }

            /// <summary>
            /// The Q AutoHarass Logic,
            /// The Q KillSteal Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&

                ((Variables.Q.GetDamage(Targets.Target) > Targets.Target.Health) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").IsActive()) ||

                (!ObjectManager.Player.UnderTurret() &&
                    ObjectManager.Player.ManaPercent >= 50 &&
                    Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.harass").IsActive()))
            {
                Variables.Q.Cast();
            }

            /// <summary>
            /// The R Lifesaver Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                ObjectManager.Player.CountEnemiesInRange(700) > 0 &&
                HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/4 &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.lifesaver").IsActive())
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
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.W.Range) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.combo").IsActive())
            {
                Variables.W.Cast();
            }

            /// <summary>
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&
                !ObjectManager.Player.HasBuff("RenektonPreExecute") &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").IsActive())
            {
                Variables.Q.Cast();
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
                (Targets.Minions?.Count() >= 3 || Targets.JungleMinions.Any()) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.farm").IsActive())
            {
                Variables.Q.Cast();
            }
        }
    }
}
