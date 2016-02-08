using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Akali
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
            /// The Q Combo Logic,
            /// The Q KillSteal Logic,
            /// The Q AutoHarass Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&

                ((Targets.Target.Health < Variables.Q.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").IsActive()) ||

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").IsActive()) ||

                (!Targets.Target.UnderTurret() &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.harass").IsActive())))
            {
                Variables.Q.CastOnUnit(Targets.Target);
            }

            /// <summary>
            /// The R Combo Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.combo").IsActive() &&
                    (!Targets.Target.UnderTurret() || !Variables.Menu.Item($"{Variables.MainMenuName}.misc.safe").IsActive()) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.whitelist.{Targets.Target.ChampionName.ToLower()}").IsActive()) ||

                (Targets.Target.Health < Variables.R.GetDamage(Targets.Target) * 2 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.ks").IsActive())))
            {
                Variables.R.CastOnUnit(Targets.Target);
            }

            /// <summary>
            /// The E KillSteal Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Targets.Target.Health < Variables.E.GetDamage(Targets.Target) &&
                Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.ks").IsActive())
            {
                Variables.E.Cast();
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
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").IsActive())
            {
                Variables.E.Cast();
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
            /// The E LaneClear Logic,
            /// The E JungleClear Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededEMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                (Targets.Minions.Count() >= 3 || Targets.JungleMinions.Any()) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.farm").IsActive())
            {
                Variables.E.Cast();
            }
        }
    }
}
