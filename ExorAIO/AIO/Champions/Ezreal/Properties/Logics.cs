using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Ezreal
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
        public static void ExecuteR(EventArgs args)
        {
            /// <summary>
            /// The R KillSteal Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.ks").IsActive())
            {
                foreach (var target in HeroManager.Enemies
                    .Where(t =>
                        t.IsValidTarget(Variables.R.Range) &&
                        t.Health < Variables.R.GetDamage(t)/2 &&
                        (!t.IsValidTarget(Variables.Q.Range) || !Variables.Q.IsReady())))
                {
                    Variables.R.SPredictionCast(target, HitChance.High);
                }
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteTearStacking(EventArgs args)
        {
            /// <summary>
            /// The Tear Stacking Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Bools.HasTear(ObjectManager.Player) &&
                !ObjectManager.Player.IsRecalling() &&
                ObjectManager.Player.CountEnemiesInRange(1500) == 0 &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededTearMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.None &&
                Variables.Menu.Item($"{Variables.MainMenuName}.misc.tear").IsActive())
            {
                Variables.Q.Cast(Game.CursorPos);
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteAuto(EventArgs args)
        {
            /// <summary>
            /// The Q KillSteal Logic,
            /// The Q Immobile Harass Logic,
            /// The Q AutoHarass Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    !Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").IsActive()) ||
                
                (Targets.Target.Health < Variables.Q.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").IsActive()) ||

                (ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    !Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.harass").IsActive())))
            {
                Variables.Q.SPredictionCast(Targets.Target, HitChance.High);
            }

            /// <summary>
            /// The W KillSteal Logic,
            /// The W Immobile Harass Logic,
            /// The W AutoHarass Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                !Variables.Q.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&

                ((Targets.Target.Health < Variables.W.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wspell.ks").IsActive()) ||

                (ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                    ObjectManager.Player.TotalMagicalDamage > ObjectManager.Player.FlatPhysicalDamageMod &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wspell.harass").IsActive())))
            {
                Variables.W.SPredictionCast(Targets.Target, HitChance.High);
            }

            /// <summary>
            /// The R Combo Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                !Variables.Q.IsReady() &&
                !Variables.W.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(Variables.R.Range) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                ObjectManager.Player.CountEnemiesInRange(ObjectManager.Player.AttackRange) >= 2 &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.combo").IsActive() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.whitelist.{Targets.Target.ChampionName.ToLower()}").IsActive())
            {
                Variables.R.SPredictionCast(Targets.Target, HitChance.High);
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
                Variables.Q.SPredictionCast((Obj_AI_Hero)args.Target, HitChance.High);
                return;
            }

            /// <summary>
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.W.Range) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.combo").IsActive())
            {
                Variables.W.SPredictionCast((Obj_AI_Hero)args.Target, HitChance.High);
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
                if (Targets.Minions.Any())
                {
                    Variables.Q.Cast((Targets.Minions.FirstOrDefault()).Position);
                }
                else if (Targets.JungleMinions.Any())
                {
                    Variables.Q.Cast((Targets.JungleMinions.FirstOrDefault()).Position);
                }
            }
        }
    }
}
