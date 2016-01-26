namespace ExorAIO.Champions.Darius
{
    using System;
    using System.Linq;
    using LeagueSharp;
    using LeagueSharp.Common;
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
            /// The Q AutoHarass Logic,
            /// The Q KillSteal Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                !Targets.Target.IsValidTarget(Variables.Q.Range - 220f) &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&

                ((!Targets.Target.UnderTurret() &&
					ObjectManager.Player.ManaPercent >= ManaManager.NeededQMana &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.harass").GetValue<bool>()) ||

                (Variables.Q.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").GetValue<bool>())))
            {
                Variables.Q.Cast();
            }

            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Targets.Target.IsValidTarget(Variables.E.Range) &&
                ObjectManager.Player.Distance(Variables.E.GetPrediction(Targets.Target).UnitPosition) < Variables.E.Range &&
				Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").GetValue<bool>())
            {
                Variables.E.Cast(Variables.E.GetPrediction(Targets.Target).UnitPosition);
            }

            /// <summary>
            /// The R KillSteal Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Targets.Target.IsValidTarget(Variables.R.Range) &&
                KillSteal.GetRDamage(Targets.Target) < Targets.Target.Health &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.ks").GetValue<bool>())
            {
                Variables.R.CastOnUnit(Targets.Target);
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
                ((Obj_AI_Hero)args.Target).IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.combo").GetValue<bool>())
            {
                Variables.W.Cast();
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
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                ObjectManager.Player.ManaPercent >= ManaManager.NeededQMana &&
                ((Targets.Minions?.Count() >= 3) ||
                    GameObjects.Jungle.Contains((Obj_AI_Minion)args.Target)) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.farm").GetValue<bool>())
            {
                Variables.Q.Cast();
            }
        }
    }
}