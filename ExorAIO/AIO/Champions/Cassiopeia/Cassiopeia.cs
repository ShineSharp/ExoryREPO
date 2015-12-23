namespace ExorAIO.Champions.Cassiopeia
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
    public class Cassiopeia
    {   
        /// <summary>
        /// Called when the game loads itself.
        /// </summary>
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
                if (!Variables.Orbwalker.GetTarget().Equals(0) &&
                    Variables.Orbwalker.GetTarget().IsValid &&
                    !Variables.Orbwalker.ActiveMode.Equals(Orbwalking.OrbwalkingMode.None))
                {
                    Logics.ExecuteModes(args);
                }
                else
                {
                    Logics.ExecuteFarm(args);
                    Logics.ExecuteTearStacking(args);
                }
            }
        }
    }
}
