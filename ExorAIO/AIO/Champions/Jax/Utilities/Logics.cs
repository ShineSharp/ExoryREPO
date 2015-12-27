namespace ExorAIO.Champions.Jax
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

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
        public static void ExecuteE(EventArgs args)
        {
            /// <summary>
            /// The E Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ObjectManager.Player.HasBuff("JaxCounterStrike") &&
                Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useecombo").GetValue<bool>())
            {
                Variables.E.Cast();
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteAuto(EventArgs args)
        {
            /// <summary>
            /// The Q Out of Range Gap Logic,
            /// The Q KillSteal Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Bools.HasNoProtection(Targets.Target) &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&
                !Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)) &&

                ((Variables.Q.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqks").GetValue<bool>()) ||

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    !Utility.UnderTurret(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>())))
            {
                if (Variables.E.IsReady())
                {
                    Variables.E.Cast();
                    Variables.Q.CastOnUnit(Targets.Target);
                    return;
                }

                Variables.Q.CastOnUnit(Targets.Target);
            }

            /// <summary>
            /// The R Lifesaver Logic.
            /// </summary>
            if (Variables.R.IsReady() &&

                (HealthPrediction.GetHealthPrediction(ObjectManager.Player, 2000) <= ObjectManager.Player.MaxHealth/2 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userlifesaver").GetValue<bool>()))
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
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Bools.HasNoProtection((Obj_AI_Hero)args.Target) &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewcombo").GetValue<bool>()))
            {
                Variables.W.Cast();
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

                (((Obj_AI_Base)Variables.Orbwalker.GetTarget()).CharData.BaseSkinName.Contains("SRU_") ||
                    ((Obj_AI_Base)Variables.Orbwalker.GetTarget()).CharData.BaseSkinName.Contains("Mini")) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewfarm").GetValue<bool>())
            {
                Variables.W.Cast();
            }

            /// <summary>
            /// The E Farm Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
            
                (Targets.Minions.Count() > 3 ||
                    ((Obj_AI_Base)Variables.Orbwalker.GetTarget()).CharData.BaseSkinName.Contains("SRU_") ||
                    ((Obj_AI_Base)Variables.Orbwalker.GetTarget()).CharData.BaseSkinName.Contains("Mini")) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useefarm").GetValue<bool>())
            {
                Variables.E.Cast();
            }
        }
    }
}
