namespace ExorAIO.Champions.Varus
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
    public class Varus
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

            if (!ObjectManager.Player.IsDead)
            {
                if (Targets.Target != null &&
                    Targets.Target.IsValid)
                {
                    Logics.ExecuteAuto(args);
                    
                    if (Targets.Target.GetBuffCount("varuswdebuff") == 3)
                    {
                        Logics.ExecuteModes(args);
                    }
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
