namespace ExorKalista
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using Orbwalking = SFXTargetSelector.Orbwalking;
    using TargetSelector = SFXTargetSelector.TargetSelector;

    /// <summary>
    /// The main class.
    /// </summary>
    public class Kalista
    {
        /// <summary>
        /// Triggers when the champion is loaded.
        /// </summary>
        public static void OnLoad()
        {
            Settings.SetSpells();
            Settings.SetMenu();
            Settings.SetMethods();
            Drawings.LoadRanges();
            Drawings.LoadDamage();
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void Game_OnGameUpdate(EventArgs args)
        {
            if (!ObjectManager.Player.IsDead &&
                !ObjectManager.Player.IsDashing())
            {
                Logics.ExecuteAuto(args);

                if (Variables.Orbwalker.GetTarget().IsValid<Obj_AI_Minion>())
                {
                    Logics.ExecuteFarm(args);
                }

                SentinelManager.ExecuteSentinels(args);
            }
        }

        /// <summary>
        /// Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void Obj_AI_Base_OnDoCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
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
            
                (Bools.IsKillableRendTarget((Obj_AI_Base)minion) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useefarm").GetValue<bool>()))
            {
                Variables.E.Cast();
            }
        }

        /// <summary>
        /// Triggers when a spell is being processed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe &&
                args.SData.Name == "kalistaexpungewrapper")
            {
                Orbwalking.ResetAutoAttackTimer();
            }
        }
    }
}
