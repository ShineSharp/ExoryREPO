using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Tristana
{
    using System;
    using ExorAIO.Utilities;
    using Orbwalking = SFXTargetSelector.Orbwalking;
    using TargetSelector = SFXTargetSelector.TargetSelector;

    /// <summary>
    /// The champion class.
    /// </summary>
    class Tristana
    {
        /// <summary>
        /// Called when the game loads itself.
        /// </summary>
        public void OnLoad()
        {
            Menus.Initialize();
            Methods.Initialize();
            Drawings.Initialize();
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void OnUpdate(EventArgs args)
        {
            Spells.Initialize();

            if (!ObjectManager.Player.IsDead &&
                Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.None)
            {
                /// <summary>
                /// The target priority.
                /// </summary>
                TargetSelector.Weights.GetItem("low-health").ValueFunction = hero => hero.Health - KillSteal.Damage(hero);
                TargetSelector.Weights.GetItem("low-health").Tooltip = "Low Health (Health - Combo Damage) = Higher Weight";
                TargetSelector.Weights.Register(
                    new TargetSelector.Weights.Item(
                        "e-charge", "E Charge", 10, false, hero => hero.HasBuff("TristanaECharge") ? 1 : 0,
                        "Has E Charge = Higher Weight"));

                if (Targets.Target != null &&
                    Targets.Target.IsValid &&
                    Bools.HasNoProtection(Targets.Target))
                {
                    Logics.ExecuteAuto(args);
                    Logics.ExecuteModes(args);
                }

                if (Variables.Orbwalker.GetTarget() != null &&
                    Variables.Orbwalker.GetTarget().IsValid<Obj_AI_Minion>())
                {
                    Logics.ExecuteFarm(args);
                }
            }
        }
    }
}
