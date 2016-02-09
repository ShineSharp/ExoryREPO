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
                Targets.Target.IsValidTarget(Variables.Q.Range) &&
                Variables.Q.GetSPrediction(Targets.Target).HitChance >= HitChance.VeryHigh &&

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    !Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").IsActive()) ||
                
                (Targets.Target.Health < Variables.Q.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").IsActive()) ||

                (ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    !Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.harass").IsActive())))
            {
                Variables.Q.Cast(Variables.Q.GetSPrediction(Targets.Target).UnitPosition.To3D());
            }

            /// <summary>
            /// The W KillSteal Logic,
            /// The W Immobile Harass Logic,
            /// The W AutoHarass Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&
                !Variables.Q.IsReady() || Variables.Q.GetSPrediction(Targets.Target).HitChance < HitChance.VeryHigh &&
                Variables.W.GetSPrediction(Targets.Target).HitChance != HitChance.OutOfRange &&

                ((Targets.Target.Health < Variables.W.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wspell.ks").IsActive()) ||

                (ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                    ObjectManager.Player.TotalMagicalDamage > ObjectManager.Player.FlatPhysicalDamageMod &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wspell.harass").IsActive())))
            {
                Variables.W.Cast(Variables.W.GetSPrediction(Targets.Target).UnitPosition.To3D());
            }

            /// <summary>
            /// The R KillSteal Logic,
            /// The R Combo Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(Variables.R.Range) &&
                
                ((Targets.Target.Health < Variables.R.GetDamage(Targets.Target) &&
                    (!Variables.W.IsReady() || !Targets.Target.IsValidTarget(Variables.W.Range)) &&
                    (!Variables.Q.IsReady() || !Targets.Target.IsValidTarget(Variables.Q.Range)) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.ks").IsActive()) ||
                
                (!Variables.Q.IsReady() &&
                    !Variables.W.IsReady() &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    ObjectManager.Player.CountEnemiesInRange(Orbwalking.GetRealAutoAttackRange(null)) >= 2 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.combo").IsActive() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.whitelist.{Targets.Target.ChampionName.ToLower()}").IsActive())))
            {
                Variables.R.Cast(Variables.R.GetSPrediction(Targets.Target).UnitPosition.To3D());
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
                Variables.Q.GetSPrediction((Obj_AI_Hero)args.Target).HitChance >= HitChance.VeryHigh &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").IsActive())
            {
                Variables.Q.Cast(Variables.Q.GetSPrediction((Obj_AI_Hero)args.Target).UnitPosition.To3D());
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
                Variables.W.Cast(Variables.W.GetSPrediction((Obj_AI_Hero)args.Target).UnitPosition.To3D());
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
