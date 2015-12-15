using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace ExorLucian
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        static void Game_OnGameLoad(EventArgs args)
        {
            if (!ObjectManager.Player.ChampionName.Equals("Lucian"))
            {
                return;
            }

            Lucian.OnLoad();
            Game.PrintChat("<b><font color='#009aff'>Exor</font></b>Lucian: <font color='#009aff'>Ultima</font> - Loaded!");
        }
    }
}