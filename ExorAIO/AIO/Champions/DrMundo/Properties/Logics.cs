namespace ExorAIO.Champions.DrMundo
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
            /// The Q Combo Logic,
            /// The Q AutoHarass Logic,
            /// The Q Killsteal Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
				ObjectManager.Player.HealthPercent > 10 &&
			    Targets.Target.IsValidTarget(Variables.Q.Range) &&
                Variables.R.GetPrediction(Targets.Target).Hitchance >= HitChance.High &&

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").GetValue<bool>()) ||

                (ObjectManager.Player.HealthPercent > ManaManager.NeededQMana &&
					Variables.Menu.Item($"{Variables.MainMenuName}.qspell.harass").GetValue<bool>()) ||

                (Variables.Q.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").GetValue<bool>())))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
            }

            /// <summary>
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.combo").GetValue<bool>())
            {
                if ((!ObjectManager.Player.HasBuff("BurningAgony") &&
					Targets.Target.IsValidTarget(Variables.W.Range) &&
					ObjectManager.Player.HealthPercent > ManaManager.NeededQMana) ||

                    ((ObjectManager.Player.HasBuff("BurningAgony") &&
					(!Targets.Target.IsValidTarget(Variables.W.Range) ||
					ObjectManager.Player.HealthPercent < 35))))
                {
                    Variables.W.Cast();
                }
            }

            /// <summary>
            /// The R Lifesaver Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                ObjectManager.Player.CountEnemiesInRange(700) > 0 &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.lifesaver").GetValue<bool>() &&
                HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/3)
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
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").GetValue<bool>())
            {
                Variables.E.Cast();
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteFarm(EventArgs args)
        {
            /// <summary>
            /// The W Farm Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.farm").GetValue<bool>())
            {
                if ((!ObjectManager.Player.HasBuff("BurningAgony") && 
                    ((Targets.Minions?.Count() >= 2 ||
						GameObjects.Jungle.Contains((Obj_AI_Minion)Variables.Orbwalker.GetTarget())) &&
						ObjectManager.Player.Health >= ManaManager.NeededWMana)) ||

                    (ObjectManager.Player.HasBuff("BurningAgony") && 
                    ((Targets.Minions?.Count() < 2 ||
						!GameObjects.Jungle.Contains((Obj_AI_Minion)Variables.Orbwalker.GetTarget())) ||
						ObjectManager.Player.Health < ManaManager.NeededWMana)))
                {
                    Variables.W.Cast();
                }
            }
        }
    }
}
