using LeagueSharp;
using LeagueSharp.Common;

namespace ExorKalista
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        static void Game_OnGameLoad(EventArgs args)
        {
            if (!ObjectManager.Player.ChampionName.Equals("Kalista"))
            {
                return;
            }

            Kalista.OnLoad();
            Game.PrintChat("<b><font color='#009aff'>Exor</font></b>Kalista: <font color='#009aff'>Ultima</font> - Loaded!");
        }
    }
}