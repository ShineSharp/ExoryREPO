using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace ExorJhin
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        static void Game_OnGameLoad(EventArgs args)
        {
            if (!ObjectManager.Player.ChampionName.Equals("Jhin"))
            {
                return;
            }

            Jhin.OnLoad();
            Game.PrintChat("<b><font color='#009aff'>Exor</font></b>Jhin: <font color='#009aff'>Ultima</font> - Loaded!");
        }
    }
}