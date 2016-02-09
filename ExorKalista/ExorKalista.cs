using LeagueSharp;
using LeagueSharp.Common;

namespace ExorKalista
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
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
        public static void OnLoad()
        {
            Menus.Initialize();
            Spells.Initialize();
            Methods.Initialize();
            Drawings.InitializeDamage();
            Drawings.InitializeDrawings();
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void OnUpdate(EventArgs args)
        {
            if (!ObjectManager.Player.IsDead &&
                !ObjectManager.Player.IsDashing() &&
                !ObjectManager.Player.Spellbook.IsCastingSpell)
            {
                /// <summary>
                /// The Soulbound declaration.
                /// </summary>
                if (Variables.SoulBound == null)
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
        /// Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void OnDoCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe &&
                args.Target.IsValid<Obj_AI_Hero>() &&
                Orbwalking.IsAutoAttack(args.SData.Name) &&
                Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.None)
            {
                Logics.ExecuteModes(sender, args);
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
                Bools.IsKillableByRend((Obj_AI_Minion)minion) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.helper").IsActive())
            {
                Variables.E.Cast();
            }
        }
    }
}
