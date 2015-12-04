using System;
using System.Collections.Generic;

using LeagueSharp;
using LeagueSharp.Common;

using ExorAIO.Utilities;

using ExorAIO.Champions.Ashe;
using ExorAIO.Champions.Cassiopeia;
using ExorAIO.Champions.Corki;
using ExorAIO.Champions.DrMundo;
using ExorAIO.Champions.Ezreal;
using ExorAIO.Champions.Graves;
using ExorAIO.Champions.KogMaw;
using ExorAIO.Champions.Olaf;
using ExorAIO.Champions.Renekton;
using ExorAIO.Champions.Sivir;
using ExorAIO.Champions.Tristana;
using ExorAIO.Champions.Vayne;

using Variables = ExorAIO.Utilities.Variables;

namespace ExorAIO.Core
{
    class Bootstrap
    {
        /// <summary>
        /// Builds the general Menu and loads the Common Orbwalker.
        /// </summary>
        public static void BuildMenu()
        {
            Variables.Menu = new Menu($"[ExorAIO]: {ObjectManager.Player.ChampionName}", $"{Variables.MainMenuName}", true);
            {
                Variables.OrbwalkerMenu = new Menu($"Orbwalker", $"{Variables.MainMenuName}.orbwalker");
                {
                    Variables.Orbwalker = new Orbwalking.Orbwalker(Variables.OrbwalkerMenu);
                }
                Variables.Menu.AddSubMenu(Variables.OrbwalkerMenu);
            }
            Variables.Menu.AddToMainMenu();
        }

        /// <summary>
        /// Tries to load the currently playing champion.
        /// </summary>
        public static void LoadChampion()
        {
            if (Variables.LoadableChampions.Contains(ObjectManager.Player.ChampionName))
            {
                switch (ObjectManager.Player.ChampionName)
                {
                    case "Ashe":       new Ashe()      .OnLoad(); break;
                    case "Cassiopeia": new Cassiopeia().OnLoad(); break;
                    case "Corki":      new Corki()     .OnLoad(); break;
                    case "DrMundo":    new DrMundo()   .OnLoad(); break;
                    case "Ezreal":     new Ezreal()    .OnLoad(); break;
                    case "Graves":     new Graves()    .OnLoad(); break;
                    case "KogMaw":     new KogMaw()    .OnLoad(); break;
                    case "Olaf":       new Olaf()      .OnLoad(); break;
                    case "Renekton":   new Renekton()  .OnLoad(); break;
                    case "Sivir":      new Sivir()     .OnLoad(); break;
                    case "Tristana":   new Tristana()  .OnLoad(); break;
                    case "Vayne":      new Vayne()     .OnLoad(); break;
                }

                Game.PrintChat($"{Variables.MainMenuCodeName} - {ObjectManager.Player.ChampionName} Loaded.");

                if (Variables.Kappa.Contains(ObjectManager.Player.Name))
                {
                    Game.PrintChat("~Thanks Kurumi <3!");
                }
                return;
            }
            Game.PrintChat($"{Variables.MainMenuCodeName}- {ObjectManager.Player.ChampionName} not supported.");
        }
    }
}
