namespace ExorAIO.Champions.Lux
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using ExorAIO.Utilities;

    using Orbwalking = SFXTargetSelector.Orbwalking;

    /// <summary>
    /// The main class.
    /// </summary>
    public class Lux
    {
        /// <summary>
        /// Triggers when the champion is loaded.
        /// </summary>
        public void OnLoad()
        {
            Settings.SetSpells();
            Settings.SetMenu();
            Settings.SetMethods();
            Drawings.Load();
        }

        /// <summary>
        /// Called when an object gets created by the game.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            if (sender.Name.Contains("LuxLightstrike_tar_green") ||
                sender.Name.Contains("LuxLightstrike_tar_red"))
            {
                Variables.EGameObject = sender;
            }
        }

        /// <summary>
        /// Called when an object gets deleted by the game.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void GameObject_OnDelete(GameObject sender, EventArgs args)
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
        public static void Game_OnGameUpdate(EventArgs args)
        {
            if (!ObjectManager.Player.IsDead)
            {
                if (Targets.Target != null &&
                    Targets.Target.IsValid)
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

        /// <summary>
        /// Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void Obj_AI_Base_OnDoCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe &&
                Orbwalking.IsAutoAttack(args.SData.Name))
            {
                if (args.Target.IsValid<Obj_AI_Hero>())
                {
                    Logics.ExecuteModes(sender, args);
                }
            }
        }
    }
}