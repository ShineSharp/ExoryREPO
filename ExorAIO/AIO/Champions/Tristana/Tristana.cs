using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Tristana
{
    using System;
    using System.Linq;
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
                /// The Focus Logic (E Charges).
                /// </summary>
                foreach (var tg in HeroManager.Enemies
                    .Where(h =>
                        h.HasBuff("TristanaECharge") &&
                        h.IsValidTarget(ObjectManager.Player.AttackRange)))
                {
                    TargetSelector.Selected.Target = tg ?? null;
                    Variables.Orbwalker.ForceTarget(tg ?? null);
                }

                if (Targets.Target != null &&
                    Targets.Target.IsValid &&
                    !Targets.Target.IsInvulnerable &&
                    !Bools.IsSpellShielded(Targets.Target))
                {
                    Logics.ExecuteAuto(args);
                    Logics.ExecuteModes(args);
                }

                if (Variables.Orbwalker.GetTarget() != null &&
                    Variables.Orbwalker.GetTarget().Type != GameObjectType.obj_AI_Hero)
                {
                    Logics.ExecuteFarm(args);
                }
            }
        }
    }
}
