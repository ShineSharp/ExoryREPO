namespace ExorAIO.Champions.Ezreal
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using ExorAIO.Utilities;

    /// <summary>
    /// The main class.
    /// </summary>
    public class Ezreal
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
            if (!ObjectManager.Player.IsDead &&
                Targets.Target != null &&
                Targets.Target.IsValid)
            {
                Logics.ExecuteAuto(args);
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
                if (args.Target.IsValid<Obj_AI_Hero>() &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo)
                {
                    Logics.ExecuteModes(sender, args);
                }
                else if (args.Target.IsValid<Obj_AI_Minion>() &&
                    Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.Combo)
                {
                    Logics.ExecuteFarm(sender, args);
                }
            }
        }
    }
}
