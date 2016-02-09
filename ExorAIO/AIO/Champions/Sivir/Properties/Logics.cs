using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Sivir
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
        public static void ExecuteAuto(EventArgs args)
        {
            /// <summary>
            /// The Q KillSteal Logic,
            /// The Q Immobile Harass Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&

                ((Variables.Q.GetDamage(Targets.Target)*2 > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").IsActive()) ||

                (Bools.IsImmobile(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.immobile").IsActive())))
            {
                Variables.Q.Cast(Variables.Q.GetSPrediction(Targets.Target).UnitPosition.To3D());
            }
        }

        /// <summary>
        /// Called while processing Spellcast operations.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void ExecuteShield(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// The E Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.auto").IsActive())
            {
                if (args.SData.IsAutoAttack() ||
                    (!args.SData.Name.Equals("MockingShout") &&
                        args.SData.TargettingType.Equals(SpellDataTargetType.SelfAoe)))
                {
                    return;
                }

                Utility.DelayAction.Add(
                    sender.CharData.BaseSkinName.Equals("Zed") &&
                    args.SData.TargettingType.Equals(SpellDataTargetType.Self) ? 200 : 0,
                () =>
                    {
                        Variables.E.Cast();
                    }
                );
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
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.combo").IsActive())
            {
                Orbwalking.ResetAutoAttackTimer();
                Variables.W.Cast();
                return;
            }

            /// <summary>
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.Q.Range) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").IsActive())
            {
                Variables.Q.Cast(Variables.Q.GetSPrediction((Obj_AI_Hero)args.Target).UnitPosition.To3D());
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
            /// The W Farm Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                (Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).MinionsHit >= 2 || Targets.JungleMinions.Any()) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.farm").IsActive())
            {
                Orbwalking.ResetAutoAttackTimer();
                Variables.W.Cast();
                return;
            }

            /// <summary>
            /// The Q Farm Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.farm").IsActive())
            {
                if (Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).MinionsHit >= 2)
                {
                    Variables.Q.Cast(Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).Position);
                }
                else if (Targets.JungleMinions.Any())
                {
                    Variables.Q.Cast((Targets.JungleMinions.FirstOrDefault()).Position);
                }
            }
        }
    }
}
