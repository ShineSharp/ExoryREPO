using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Caitlyn
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using SharpDX;
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
        public static void ExecuteR(EventArgs args)
        {
            /// <summary>
            /// The R KillSteal Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                !ObjectManager.Player.UnderTurret() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.ks").IsActive())
            {
                foreach (var target in HeroManager.Enemies
                    .Where(t =>
                        t.IsValidTarget(Variables.R.Range) &&
                        t.Health < Variables.R.GetDamage(t) &&
                        (!t.IsValidTarget(Variables.Q.Range) || !Variables.Q.IsReady())))
                {
                    Variables.R.CastOnUnit(target);
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
            /// The W Immobile Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Bools.IsImmobile(Targets.Target) &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.immobile").IsActive())
            {
                Variables.W.Cast(Targets.Target.Position);
            }

            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(Variables.E.Range) &&
                !ObjectManager.Player.HasBuff("caitlynheadshotrangecheck") &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").IsActive())
            {
                Variables.E.SPredictionCast(Targets.Target, HitChance.High);
            }

            /// <summary>
            /// The Q KillSteal Logic,
            /// The Q Combo Logic,
            /// The Q AutoHarass Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&
                
                ((Variables.Q.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").IsActive()) ||

                (ObjectManager.Player.HasBuff("caitlynheadshotrangecheck") &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").IsActive()) ||

                (Targets.Target.HasBuff("caitlynyordletrapdebuff") &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.harass").IsActive())))
            {
                Variables.Q.SPredictionCast(Targets.Target, HitChance.High);
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
                if (Targets.Minions?.Count() >= 3)
                {
                    Variables.Q.CastOnUnit(Targets.Minions.FirstOrDefault());
                }
                else if (Targets.JungleMinions.Any())
                {
                    Variables.Q.CastOnUnit(Targets.JungleMinions.FirstOrDefault());
                }
            }
        }
    }
}
