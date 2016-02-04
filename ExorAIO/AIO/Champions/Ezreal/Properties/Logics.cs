using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Ezreal
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
        public static void ExecuteTearStacking(EventArgs args)
        {
            /// <summary>
            /// The Tear Stacking Logic
            /// </summary>
            if (Variables.Q.IsReady() &&
                Bools.HasTear(ObjectManager.Player) &&
                !ObjectManager.Player.IsRecalling() &&
                ObjectManager.Player.CountEnemiesInRange(1500) == 0 &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededTearMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.None &&
                Variables.Menu.Item($"{Variables.MainMenuName}.misc.tear").GetValue<bool>())
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
                Variables.Q.GetPrediction(Targets.Target).Hitchance >= HitChance.High &&

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    !Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").GetValue<bool>()) ||
                
                (Targets.Target.Health < Variables.Q.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").GetValue<bool>()) ||

                (ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    !Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.harass").GetValue<bool>())))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
            }

            /// <summary>
            /// The W KillSteal Logic,
            /// The W Immobile Harass Logic,
            /// The W AutoHarass Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&
                !Variables.Q.IsReady() || Variables.Q.GetPrediction(Targets.Target).Hitchance < HitChance.High &&
                ObjectManager.Player.Distance(Variables.W.GetPrediction(Targets.Target).UnitPosition) < Variables.W.Range &&

                ((Targets.Target.Health < Variables.W.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wspell.ks").GetValue<bool>()) ||

                (ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                    !Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wspell.harass").GetValue<bool>())))
            {
                Variables.W.Cast(Variables.W.GetPrediction(Targets.Target).UnitPosition);
            }

            /// <summary>
            /// The R KillSteal Logic,
            /// The R Combo Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(1500f) &&
                
                ((Targets.Target.Health < Variables.R.GetDamage(Targets.Target) &&
                    (!Variables.W.IsReady() || !Targets.Target.IsValidTarget(Variables.W.Range)) &&
                    (!Variables.Q.IsReady() || !Targets.Target.IsValidTarget(Variables.Q.Range)) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.ks").GetValue<bool>()) ||
                
                (!Variables.Q.IsReady() &&
                    !Variables.W.IsReady() &&
                    Targets.Target.CountEnemiesInRange(Variables.R.Width) >= 2 &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.combo").GetValue<bool>())))
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
                Variables.Q.GetPrediction((Obj_AI_Hero)args.Target).Hitchance >= HitChance.High &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").GetValue<bool>())
            {
                Variables.Q.Cast(Variables.Q.GetPrediction((Obj_AI_Hero)args.Target).UnitPosition);
                return;
            }

            /// <summary>
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.W.Range) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.combo").GetValue<bool>())
            {
                Variables.W.Cast(Variables.W.GetPrediction((Obj_AI_Hero)args.Target).UnitPosition);
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
                (Targets.Minions.Any() ||
                    GameObjects.Jungle.Contains((Obj_AI_Minion)args.Target)) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.farm").GetValue<bool>())
            {
                Variables.Q.Cast(((Obj_AI_Minion)args.Target).Position);
            }
        }
    }
}
