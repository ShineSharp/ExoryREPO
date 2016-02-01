using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Lux
{
    using System;
    using ExorAIO.Utilities;
    using Orbwalking = SFXTargetSelector.Orbwalking;
    using TargetSelector = SFXTargetSelector.TargetSelector;

    /// <summary>
    /// The champion class.
    /// </summary>
    class Lux
    {
        /// <summary>
        /// Triggers when the champion is loaded.
        /// </summary>
        public void OnLoad()
        {
            Menus.Initialize();
            Spells.Initialize();
            Methods.Initialize();
            Drawings.Initialize();
        }

        /// <summary>
        /// Called when an object gets created by the game.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void OnCreate(GameObject sender, EventArgs args)
        {
            if (sender.Name.Contains("LuxLightstrike_tar_"))
            {
                Variables.EGameObject = sender;
            }
        }

        /// <summary>
        /// Called when an object gets deleted by the game.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void OnDelete(GameObject sender, EventArgs args)
        {
            if (sender.Name.Contains("LuxLightstrike_tar_"))
            {
                Variables.EGameObject = null;
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void OnUpdate(EventArgs args)
        {
            if (!ObjectManager.Player.IsDead)
            {
                /// <summary>
                /// The Target preference.
                /// </summary>
                if (TargetSelector.Weights.GetItem("low-health") != null)
                {
                    TargetSelector.Weights.Register(
                        new TargetSelector.Weights.Item(
                            "p-stack", "Passive Stack", 10, false, hero => hero.HasBuff("luxilluminatingfraulein") ? 1 : 0,
                            "Has Passive Stack = Higher Weight"));
                }

                if (Targets.Target != null &&
                    Targets.Target.IsValid &&
                    Bools.HasNoProtection(Targets.Target) &&
                    Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.None)
                {
                    Logics.ExecuteAuto(args);
                }

                if (Variables.Orbwalker.GetTarget() != null &&
                    Variables.Orbwalker.GetTarget().IsValid)
                {
                    Logics.ExecuteFarm(args);
                }
            }
        }
    }
}
