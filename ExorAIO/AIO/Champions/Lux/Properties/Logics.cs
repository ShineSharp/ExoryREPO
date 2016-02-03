using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Lux
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
            /// The E Management.
            /// </summary>
            if (Bools.CanUseE() &&
                !Variables.R.IsReady() &&
                Targets.Target.HasBuffOfType(BuffType.Slow) &&
                (!Targets.Target.HasBuff("luxilluminatingfraulein") ||
                    Targets.Target.Health < Variables.E.GetDamage(Targets.Target)))
            {
                Variables.E.Cast();
            }

            /// <summary>
            /// The W Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ObjectManager.Player.CountEnemiesInRange(1000f) >= 1 &&
                HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250f + Game.Ping / 2f)) <= ObjectManager.Player.Health/2 &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.auto").GetValue<bool>())
            {
                Variables.W.Cast(Game.CursorPos);
            }

            /// <summary>
            /// The R KillSteal Logic,
            /// The R Combo Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Targets.Target.IsValidTarget(Variables.R.Range) &&
                Targets.Target.Health > Variables.E.GetDamage(Targets.Target) &&

                ((Targets.Target.Health < Variables.R.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.ks").GetValue<bool>()) ||

                (Bools.CanUseE() &&
                    Targets.Target.HasBuff("luxilluminatingfraulein") &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.combo").GetValue<bool>())))
            {
                Variables.R.Cast(Variables.R.GetPrediction(Targets.Target).UnitPosition);
            }

            /// <summary>
            /// The Q KillSteal Logic,
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&

                ((!Variables.R.IsReady() &&
                    Targets.Target.Health < Variables.Q.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").GetValue<bool>()) ||

                (!Targets.Target.HasBuff("luxilluminatingfraulein") &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").GetValue<bool>())))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
                return;
            }

            /// <summary>
            /// The E KillSteal Logic,
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Targets.Target.IsValidTarget(Variables.E.Range) &&
                (!Variables.Q.IsReady() || Variables.Q.GetPrediction(Targets.Target).Hitchance < HitChance.High) &&
            
                ((Targets.Target.Health < Variables.E.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.espell.ks").GetValue<bool>()) ||

                (!Targets.Target.HasBuff("luxilluminatingfraulein") &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").GetValue<bool>())))
            {
                Variables.E.Cast(Variables.E.GetPrediction(Targets.Target).CastPosition);
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteFarm(EventArgs args)
        {
            /// <summary>
            /// The Q Farm Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                (Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).MinionsHit == 2 ||
                    GameObjects.Jungle.Contains((Obj_AI_Minion)Variables.Orbwalker.GetTarget())) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.farm").GetValue<bool>())
            {
                Variables.Q.Cast((Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width)).Position);
            }

            /// <summary>
            /// The E Farm Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededEMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                (Variables.E.GetCircularFarmLocation(Targets.Minions, Variables.E.Width).MinionsHit >= 2 ||
                    GameObjects.Jungle.Contains((Obj_AI_Minion)Variables.Orbwalker.GetTarget())) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.farm").GetValue<bool>())
            {
                Variables.E.Cast((Variables.E.GetCircularFarmLocation(Targets.Minions, Variables.E.Width)).Position);
            }
        }
    }
}
