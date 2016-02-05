using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Quinn
{
    using System;
    using System.Linq;
    using ExorAIO.Utilities;
    using SharpDX;
    using Orbwalking = SFXTargetSelector.Orbwalking;
    using TargetSelector = SFXTargetSelector.TargetSelector;

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
            /// The Q Against Impaired Targets Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&
                Variables.Q.GetPrediction(Targets.Target).Hitchance >= HitChance.High &&

                ((Targets.Target.Health < Variables.Q.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").GetValue<bool>()) ||

                (Bools.IsImmobile(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.immobile").GetValue<bool>())))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
            }

            /// <summary>
            /// The W Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
               !ObjectManager.Player.IsRecalling() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.auto").GetValue<bool>())
            {
                if (Variables.Locations
                    .Any(h => ObjectManager.Player.Distance(h) < 350f))
                {
                    Variables.W.Cast();
                }
            }

            /// <summary>
            /// The E KillSteal Logic.
            /// The E Valor Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(Variables.E.Range) &&

                ((Targets.Target.Health < Variables.E.GetDamage(Targets.Target) + ObjectManager.Player.GetAutoAttackDamage(Targets.Target)*2 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.espell.ks").GetValue<bool>()) ||

                (!Targets.Target.IsMelee() &&
                    Targets.Target.CountEnemiesInRange(1000f) == 0 &&
                    Variables.R.Instance.Name.Equals("quinnrfinale") &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").GetValue<bool>())))
            {
                Variables.E.CastOnUnit(Targets.Target);
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
                !((Obj_AI_Hero)args.Target).HasBuff("quinnw") &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Q.GetPrediction((Obj_AI_Hero)args.Target).Hitchance >= HitChance.High &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").GetValue<bool>())
            {
                Variables.Q.Cast(Variables.Q.GetPrediction((Obj_AI_Hero)args.Target).UnitPosition);
                return;
            }

            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                !((Obj_AI_Hero)args.Target).HasBuff("quinnw") &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").GetValue<bool>())
            {
                Variables.E.CastOnUnit((Obj_AI_Hero)args.Target);
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
                (Variables.Q.GetCircularFarmLocation(Targets.Minions, Variables.Q.Width).MinionsHit >= 3 ||
                    GameObjects.Jungle.Contains((Obj_AI_Minion)args.Target)) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.farm").GetValue<bool>())
            {
                Variables.Q.Cast(Variables.Q.GetCircularFarmLocation(Targets.Minions, Variables.Q.Width).Position);
            }
        }
    }
}
