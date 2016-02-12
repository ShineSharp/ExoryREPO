using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Cassiopeia
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
        public static void ExecuteModes(EventArgs args)
        {
            /// <summary>
            /// The No AA while in Combo option.
            /// </summary>
            if (!Variables.Menu.Item($"{Variables.MainMenuName}.misc.aa").IsActive())
            {
                Variables.Orbwalker.SetAttack(Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.Combo);
            }

            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Targets.Target.HasBuffOfType(BuffType.Poison) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").IsActive())
            {
                Utility.DelayAction.Add(Variables.Menu.Item($"{Variables.MainMenuName}.espell.delay").GetValue<Slider>().Value,
                () =>
                    {
                        Variables.E.CastOnUnit(Targets.Target);
                    }
                ); 
            }

            /// <summary>
            /// The R Combo Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.combo").IsActive() &&
                Targets.RTargets.Count() >= Variables.Menu.Item($"{Variables.MainMenuName}.rspell.enemies").GetValue<Slider>().Value)
            {
                Variables.R.CastIfWillHit(Targets.Target, Targets.RTargets.Count());
            }

            /// <summary>
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                !Targets.Target.HasBuffOfType(BuffType.Poison) &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").IsActive())
            {
                Variables.Q.SPredictionCast(Targets.Target, HitChance.High);
                return;
            }

            /// <summary>
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                !Variables.Q.IsReady() &&
                !Targets.Target.HasBuffOfType(BuffType.Poison) &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.combo").IsActive())
            {
                Variables.W.SPredictionCast(Targets.Target, HitChance.High);
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
                (Variables.Q.GetCircularFarmLocation(Targets.Minions, Variables.Q.Width).MinionsHit >= 3 || Targets.JungleMinions.Any()) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.farm").IsActive())
            {
                Variables.Q.Cast(Variables.Q.GetCircularFarmLocation(Targets.Minions, Variables.Q.Width).Position);
                return;
            }

            /// <summary>
            /// The W Farm Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                !Variables.Q.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                (Variables.W.GetCircularFarmLocation(Targets.Minions, Variables.W.Width).MinionsHit >= 3 || Targets.JungleMinions.Any()) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.farm").IsActive())
            {
                Variables.W.Cast(Variables.W.GetCircularFarmLocation(Targets.Minions, Variables.W.Width).Position);
            }

            /// <summary>
            /// The E Farm Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                !Variables.Q.IsReady() &&
                !Variables.W.IsReady() &&

                Variables.E.GetDamage(Targets.Minions?.FirstOrDefault()) >
                    (Targets.Minions?.FirstOrDefault()).Health &&

                ((Targets.Minions?.FirstOrDefault()).HasBuffOfType(BuffType.Poison) || 
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Mixed ||
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LastHit) &&

                Variables.Menu.Item($"{Variables.MainMenuName}.espell.farm").IsActive())
            {
                Utility.DelayAction.Add(Variables.Menu.Item($"{Variables.MainMenuName}.espell.delay").GetValue<Slider>().Value, 
                () =>
                    {
                        Variables.E.CastOnUnit(((Obj_AI_Minion)Variables.Orbwalker.GetTarget()));
                    }
                );
            }
        }
    }
}
