namespace ExorAIO.Champions.Olaf
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using SharpDX;

    using ExorAIO.Utilities;
    
    using ItemData = LeagueSharp.Common.Data.ItemData;

    using Orbwalking = SFXTargetSelector.Orbwalking;

    /// <summary>
    /// The logics class.
    /// </summary>
    public class Logics
    {
        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteAuto(EventArgs args)
        {
            /// <summary>
            /// The Q Harass/Farm Logic.
            /// The Q KillSteal.
            /// </summary>
            if (Variables.Q.IsReady() &&
                !Targets.Target.IsMovementImpaired() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&
                ObjectManager.Player.Distance(Variables.Q.GetPrediction(Targets.Target).UnitPosition) < Variables.Q.Range - 100 &&

                ((ObjectManager.Player.ManaPercent >= ManaManager.NeededQMana &&
                    !Utility.UnderTurret(ObjectManager.Player) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqautoharass").GetValue<bool>()) ||

                (Variables.Q.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqks").GetValue<bool>())))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
            }

            /// <summary>
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)) &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewcombo").GetValue<bool>()))
            {
                Variables.W.Cast();
            }

            /// <summary>
            /// The R Anti-HardCC Logic.
            /// </summary>
            if (Variables.R.IsReady() &&

                (Bools.ShouldCleanse() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.useranticc").GetValue<bool>()))
            {
                Variables.R.Cast();
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
                ((Obj_AI_Hero)args.Target).IsValidTarget(Orbwalking.GetRealAutoAttackRange((Obj_AI_Hero)args.Target)) &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>()))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
                return;
            }

            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Bools.HasNoProtection((Obj_AI_Hero)args.Target) &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.E.Range) &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>()))
            {
                Variables.E.Cast((Obj_AI_Hero)args.Target);
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

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    (Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).MinionsHit >= 3 ||
                        GameObjects.Jungle.Contains((Obj_AI_Minion)Variables.Orbwalker.GetTarget())) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>()))
            {
                Variables.Q.Cast(Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).Position);
            }
        }
    }
}
