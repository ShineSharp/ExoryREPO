using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Kalista
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using ExorAIO.Utilities;
    using Orbwalking = SFXTargetSelector.Orbwalking;
    using TargetSelector = SFXTargetSelector.TargetSelector;

    /// <summary>
    /// The champion class.
    /// </summary>
    class Kalista
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
            Healthbars.Initialize();
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
                /// The Soulbound declaration.
                /// </summary>
                if (Variables.SoulBound == null &&
                    !ObjectManager.Player.HasBuff("kalistavounbound"))
                {
                    Variables.SoulBound = HeroManager.Allies
                        .Find(b => b.HasBuff("kalistacoopstrikeally"));
                }

                /// <summary>
                /// The Focus Logic (Passive Mark).
                /// </summary>
                foreach (var tg in HeroManager.Enemies
                    .Where(h =>
                        h.HasBuff("kalistacoopstrikemarkally") &&
                        h.IsValidTarget(ObjectManager.Player.AttackRange)))
                {
                    TargetSelector.Selected.Target = tg ?? null;
                    Variables.Orbwalker.ForceTarget(tg ?? null);
                }

                Logics.ExecuteAuto(args);
                Logics.ExecuteFarm(args);
            }
        }

        /// <summary>
        /// Triggers when there is a valid unkillable minion around.
        /// </summary>
        /// <param name="minion">The unkillable minion.</param>
        public static void OnNonKillableMinion(AttackableUnit minion)
        {
            if (Variables.E.IsReady() &&
                !ObjectManager.Player.IsDashing() &&
                ((Obj_AI_Minion)minion).Health < KillSteal.GetPerfectRendDamage((Obj_AI_Minion)minion) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.helper").IsActive())
            {
                Variables.E.Cast();
            }
        }
    }
}
