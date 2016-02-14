using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.DrMundo
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
        public static void ExecuteAuto(EventArgs args)
        {
            /// <summary>
            /// The Q Killsteal Logic,
            /// The Q Combo Logic,
            /// The Q AutoHarass Logic,
            /// </summary>
            if (Variables.Q.IsReady() &&
				ObjectManager.Player.HealthPercent > 10 &&
			    Targets.Target.IsValidTarget(Variables.Q.Range) &&

                ((Variables.Q.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").IsActive()) ||

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").IsActive()) ||

                (!ObjectManager.Player.UnderTurret() &&
                    ObjectManager.Player.HealthPercent > ManaManager.NeededQMana &&
					Variables.Menu.Item($"{Variables.MainMenuName}.qspell.harass").IsActive() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.whitelist.{Targets.Target.ChampionName.ToLower()}").IsActive())))
            {
                Variables.Q.SPredictionCast(Targets.Target, HitChance.High);
            }

            /// <summary>
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.combo").IsActive())
            {
                if ((!ObjectManager.Player.HasBuff("BurningAgony") &&
					Targets.Target.IsValidTarget(Variables.W.Range) &&
					ObjectManager.Player.HealthPercent >= ManaManager.NeededQMana) ||

                    ((ObjectManager.Player.HasBuff("BurningAgony") &&
					!Targets.Target.IsValidTarget(Variables.W.Range) ||
					ObjectManager.Player.HealthPercent < ManaManager.NeededQMana)))
                {
                    Variables.W.Cast();
                }
            }

            /// <summary>
            /// The R Lifesaver Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                ObjectManager.Player.CountEnemiesInRange(700) > 0 &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.lifesaver").IsActive() &&
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
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.E.Range) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").IsActive())
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
            /// The W LaneClear Logic,
            /// The W JungleClear Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.farm").IsActive())
            {
                if ((!ObjectManager.Player.HasBuff("BurningAgony") &&
                    (Targets.Minions?.Count() >= 2 || Targets.JungleMinions.Any()) &&
                    ObjectManager.Player.HealthPercent >= ManaManager.NeededWMana) ||
                    
                    (ObjectManager.Player.HasBuff("BurningAgony") &&
                    (!Targets.Minions.Any() && !Targets.JungleMinions.Any()) ||
                    ObjectManager.Player.HealthPercent < ManaManager.NeededWMana))
                {
                    Variables.W.Cast();
                }
            }
        }
    }
}
