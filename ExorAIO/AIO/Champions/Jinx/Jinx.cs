namespace ExorAIO.Champions.Jinx
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using Orbwalking = SFXTargetSelector.Orbwalking;

    using ExorAIO.Utilities;

    /// <summary>
    /// The main class.
    /// </summary>
    public class Jinx
    {
        public void OnLoad()
        {
            Settings.SetSpells();
            Settings.SetMenu();
            Settings.SetMethods();
            Drawings.Load();
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
            }
        }

        /// <summary>
        /// Called when a cast gets executed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void Obj_AI_Base_OnDoCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe &&
                Orbwalking.IsAutoAttack(args.SData.Name) &&
                Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.None)
            {
                if (args.Target.IsValid<Obj_AI_Hero>())
                {
                    Logics.ExecuteModes(sender, args);
                }
            }
        }

        /// <summary>
        /// Called when a cast gets executed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsEnemy &&
                sender.IsValid<Obj_AI_Hero>())
            {
                Logics.ExecuteSpellCast(sender, args);
            }
        }

        /// <summary>
        /// The E on Gapcloser Logic.
        /// </summary>
        public static void OnGapcloser(ActiveGapcloser gapcloser)
        {
            if (Variables.E.IsReady() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeauto").GetValue<bool>())
            {
                if (Variables.E.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeauto").GetValue<bool>() &&
                    ObjectManager.Player.Distance(gapcloser.End) <= Variables.E.Range)
                {
                    Variables.E.Cast(gapcloser.End);
                }
            }
        }
    }
}
