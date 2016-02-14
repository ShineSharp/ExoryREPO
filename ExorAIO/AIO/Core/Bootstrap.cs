using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Core
{
    using System.Linq;
    using ExorAIO.Utilities;
    using ExorAIO.Champions.Akali;
    using ExorAIO.Champions.Anivia;   
    using ExorAIO.Champions.Ashe;
    using ExorAIO.Champions.Caitlyn;
    using ExorAIO.Champions.Cassiopeia;
    using ExorAIO.Champions.Corki;
    using ExorAIO.Champions.Darius;
    using ExorAIO.Champions.Draven;
    using ExorAIO.Champions.DrMundo;
    using ExorAIO.Champions.Evelynn;
    using ExorAIO.Champions.Ezreal;
    using ExorAIO.Champions.Graves;
    using ExorAIO.Champions.Jax;
    using ExorAIO.Champions.Jhin;
    using ExorAIO.Champions.Jinx;
    using ExorAIO.Champions.Kalista;
    using ExorAIO.Champions.KogMaw;
    using ExorAIO.Champions.Lucian;
    using ExorAIO.Champions.Lux;
    using ExorAIO.Champions.Nautilus;
    using ExorAIO.Champions.Olaf;
    using ExorAIO.Champions.Quinn;
    using ExorAIO.Champions.Renekton;
    using ExorAIO.Champions.Ryze;
    using ExorAIO.Champions.Sivir;
    using ExorAIO.Champions.Tristana;
    using ExorAIO.Champions.Tryndamere;
    using ExorAIO.Champions.Vayne;
    using Orbwalking = SFXTargetSelector.Orbwalking;
    using TargetSelector = SFXTargetSelector.TargetSelector;

    /// <summary>
    /// The bootstrap class.
    /// </summary>
    class Bootstrap
    {
        /// <summary>
        /// Builds the general Menu and loads the Common Orbwalker.
        /// </summary>
        public static void BuildMenu()
        {
            Variables.Menu = new Menu($"[ExorAIO]: {ObjectManager.Player.ChampionName}", $"{Variables.MainMenuName}", true);
            {
                Variables.OrbwalkerMenu = new Menu("Orbwalker", $"{Variables.MainMenuName}.orbwalker");
                {
                    Variables.Orbwalker = new Orbwalking.Orbwalker(Variables.OrbwalkerMenu);
                }
                Variables.Menu.AddSubMenu(Variables.OrbwalkerMenu);

                Variables.TargetSelectorMenu = new Menu("[SFX]Target Selector", $"{Variables.MainMenuName}.targetselector");
                {
                    TargetSelector.AddToMenu(Variables.TargetSelectorMenu);
                }
                Variables.Menu.AddSubMenu(Variables.TargetSelectorMenu);
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
                    case "Akali":      new Akali()     .OnLoad(); break;
                    case "Anivia":     new Anivia()    .OnLoad(); break;
                    case "Ashe":       new Ashe()      .OnLoad(); break;
                    case "Caitlyn":    new Caitlyn()   .OnLoad(); break;
                    case "Cassiopeia": new Cassiopeia().OnLoad(); break;
                    case "Corki":      new Corki()     .OnLoad(); break;
                    case "Darius":     new Darius()    .OnLoad(); break;
                    case "Draven":     new Draven()    .OnLoad(); break;
                    case "DrMundo":    new DrMundo()   .OnLoad(); break;
                    case "Evelynn":    new Evelynn()   .OnLoad(); break;
                    case "Ezreal":     new Ezreal()    .OnLoad(); break;
                    case "Graves":     new Graves()    .OnLoad(); break;
                    case "Jax":        new Jax()       .OnLoad(); break;
                    case "Jhin":       new Jhin()      .OnLoad(); break;
                    case "Jinx":       new Jinx()      .OnLoad(); break;
                    case "Kalista":    new Kalista()   .OnLoad(); break;
                    case "KogMaw":     new KogMaw()    .OnLoad(); break;
                    case "Lucian":     new Lucian()    .OnLoad(); break;
                    case "Lux":        new Lux()       .OnLoad(); break;
                    case "Nautilus":   new Nautilus()  .OnLoad(); break;
                    case "Olaf":       new Olaf()      .OnLoad(); break;
                    case "Quinn":      new Quinn()     .OnLoad(); break;
                    case "Renekton":   new Renekton()  .OnLoad(); break;
                    case "Ryze":       new Ryze()      .OnLoad(); break;
                    case "Sivir":      new Sivir()     .OnLoad(); break;
                    case "Tristana":   new Tristana()  .OnLoad(); break;
                    case "Tryndamere": new Tryndamere().OnLoad(); break;
                    case "Vayne":      new Vayne()     .OnLoad(); break;
                }

                Game.PrintChat($"<b><font color='#009aff'>Exor</font></b>AIO: <font color='#009aff'>Ultima</font> - {ObjectManager.Player.ChampionName} Loaded.");
            }
            else
            {
                Game.PrintChat($"{Variables.MainMenuCodeName} - {ObjectManager.Player.ChampionName} not supported.");
            }
        }
    }
}
