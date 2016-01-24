namespace ExorAIO.Champions.Jinx
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
    public class Jinx
    {
        /// <summary>
        /// Triggers when the champion is loaded.
        /// </summary>
        public void OnLoad()
        {
            Settings.SetMenu();
            Settings.SetMethods();
            Drawings.Initialize();
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void Game_OnGameUpdate(EventArgs args)
        {
            Settings.SetSpells();

            if (!ObjectManager.Player.IsDead &&
                Targets.Target != null &&
                Targets.Target.IsValid)
            {
                Logics.ExecuteAuto(args);
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
