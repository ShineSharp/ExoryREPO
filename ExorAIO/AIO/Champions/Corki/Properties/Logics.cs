using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Corki
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
            /// The Q KillSteal Logic,
            /// The Q Immobile Harass Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&

                ((Variables.Q.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").IsActive()) ||

                (Bools.IsImmobile(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.immobile").IsActive())))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).CastPosition);
            }

            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Targets.Target.IsValidTarget(Variables.E.Range) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").IsActive())
            {
                Variables.E.Cast();
            }

            /// <summary>
            /// The R KillSteal Logic,
            /// The R AutoHarass Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Targets.Target.IsValidTarget(Variables.R.Range) &&
                Variables.R.GetPrediction(Targets.Target).Hitchance >= HitChance.High &&

                ((Variables.R.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.ks").IsActive()) ||

                (!Targets.Target.UnderTurret() &&
					ObjectManager.Player.ManaPercent > ManaManager.NeededRMana &&
                    Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.harass").IsActive() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.whitelist.{Targets.Target.ChampionName.ToLower()}").GetValue<bool>())))
            {
                Variables.R.Cast(Variables.R.GetPrediction(Targets.Target).UnitPosition);
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
                Variables.Q.Cast(Variables.Q.GetPrediction((Obj_AI_Hero)args.Target).CastPosition);
            }

            /// <summary>
            /// The R Combo Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.R.Range) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.R.GetPrediction((Obj_AI_Hero)args.Target).Hitchance >= HitChance.VeryHigh &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.combo").IsActive())
            {
                Variables.R.Cast(Variables.R.GetPrediction(Targets.Target).UnitPosition);
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

            /// <summary>
            /// The E Farm Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededEMana &&
                (Targets.Minions.Count() >= 3 || Targets.JungleMinions.Any()) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.farm").IsActive())
            {
                Variables.E.Cast();
            }

            /// <summary>
            /// The R Farm Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededRMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.farm").IsActive())
            {
                if (Variables.R.GetLineFarmLocation(Targets.Minions, Variables.R.Width).MinionsHit >= 2)
                {
                    Variables.R.Cast(Variables.R.GetLineFarmLocation(Targets.Minions, Variables.R.Width).Position);
                }
                else if (Targets.JungleMinions.Any())
                {
                    Variables.R.Cast((Targets.JungleMinions.FirstOrDefault()).Position);
                }
            }
        }
    }
}
