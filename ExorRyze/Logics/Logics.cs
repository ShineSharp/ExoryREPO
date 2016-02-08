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
        public static void ExecuteTearStacking(EventArgs args)
        {
            /// <summary>
            /// The Tear Stacking Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Bools.HasTear(ObjectManager.Player) &&
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
        public static void ExecuteStacking(EventArgs args)
        {
            /// <summary>
            /// The Tear Stacking Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ObjectManager.Player.GetBuffCount("RyzePassiveStack") < 3 &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededTearMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.None &&
                Variables.Menu.Item($"{Variables.MainMenuName}.misc.manager").IsActive())
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
            /// The W Combo Logic,
            /// The W KillSteal Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wspell.combo").IsActive()) ||

                (Variables.W.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wspell.ks").IsActive())))
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
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").IsActive()) ||
                
                (ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    Variables.Q.GetPrediction(Targets.Target).Hitchance >= HitChance.High &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.harass").IsActive()) ||

                (Variables.Q.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").IsActive())))
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
                    Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").IsActive()) ||

                (Variables.E.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.espell.ks").IsActive())))
            {
                Variables.E.CastOnUnit(Targets.Target);
            }

            /// <summary>
            /// The Smart R Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Targets.Target.HasBuffOfType(BuffType.Snare) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.combo").IsActive())
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

            /// <summary>
            /// The W LaneClear Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.farm").IsActive())
            {
                if (Targets.Minions.Any())
                {
                    Variables.W.CastOnUnit(Targets.Minions.FirstOrDefault());
                }
                else if (Targets.JungleMinions.Any())
                {
                    Variables.W.CastOnUnit(Targets.JungleMinions.FirstOrDefault());
                }
            }

            /// <summary>
            /// The R LaneClear Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Variables.E.IsReady() &&
                Targets.Minions?.Count() >= 3 &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.farm").IsActive())
            {
                Variables.R.Cast();
            }

            /// <summary>
            /// The E LaneClear Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededEMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.farm").IsActive())
            {
                if (Targets.Minions?.Count() >= 3)
                {
                    Variables.E.CastOnUnit(Targets.Minions.FirstOrDefault());
                }
                else if (Targets.JungleMinions.Any())
                {
                    Variables.E.CastOnUnit(Targets.JungleMinions.FirstOrDefault());
                }
            }
        }
    }
}
