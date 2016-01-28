using LeagueSharp;
using LeagueSharp.Common;

namespace ExorRyze
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using SharpDX;
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
        public static void ExecuteStacks(EventArgs args)
        {
            /// <summary>
            /// The Tear Stacking Logic,
            /// The Passive Stacking Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                !ObjectManager.Player.IsRecalling() &&
                ObjectManager.Player.CountEnemiesInRange(1500) == 0 &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.None)
            {
                if ((ObjectManager.Player.ManaPercent > ManaManager.NeededTearMana &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.misc.tear").GetValue<bool>()) ||

                    (ObjectManager.Player.GetBuffCount("RyzePassiveStack") < 2 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.misc.stackmanager").GetValue<bool>()))
                {
                    Variables.Q.Cast(Game.CursorPos);
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
            /// The W Combo Logic,
            /// The W KillSteal Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wspell.combo").GetValue<bool>()) ||

                (Variables.W.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wspell.ks").GetValue<bool>())))
            {
                Variables.W.CastOnUnit(Targets.Target);
            }

            /// <summary>
            /// The Q Combo Logic,
            /// The Q AutoHarass Logic,
            /// The Q KillSteal Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").GetValue<bool>()) ||
                
                (ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    Variables.Q.GetPrediction(Targets.Target).Hitchance >= HitChance.High &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.harass").GetValue<bool>()) ||

                (Variables.Q.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").GetValue<bool>())))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
            }
            
            /// <summary>
            /// The E Combo Logic,
            /// The E KillSteal Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Targets.Target.IsValidTarget(Variables.E.Range) &&

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").GetValue<bool>()) ||

                (Variables.E.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.espell.ks").GetValue<bool>())))
            {
                Variables.E.CastOnUnit(Targets.Target);
            }

            /// <summary>
            /// The Smart R Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Targets.Target.HasBuffOfType(BuffType.Snare) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.combo").GetValue<bool>())
            {
                Variables.R.Cast();
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteFarm(EventArgs args)
        {
            /// <summary>
            /// The Q LaneClear Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.farm").GetValue<bool>())
            {
                Variables.Q.Cast(((Obj_AI_Minion)Variables.Orbwalker.GetTarget()).Position);
            }

            /// <summary>
            /// The W LaneClear Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.farm").GetValue<bool>())
            {
                Variables.W.CastOnUnit((Obj_AI_Minion)Variables.Orbwalker.GetTarget());
            }

            /// <summary>
            /// The R LaneClear Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Targets.Minions.Any() &&
                Targets.Minions.Count() > 3 &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.farm").GetValue<bool>())
            {
                Variables.R.Cast();
            }

            /// <summary>
            /// The E LaneClear Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededEMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.farm").GetValue<bool>())
            {
                Variables.E.CastOnUnit((Obj_AI_Minion)Variables.Orbwalker.GetTarget());
            }
        }
    }
}