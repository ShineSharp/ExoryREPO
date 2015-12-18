using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace AsunaCondemn
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        static void Game_OnGameLoad(EventArgs args)
        {
            if (!ObjectManager.Player.ChampionName.Equals("Vayne"))
            {
                return;
            }

            Condem.OnLoad();
            Game.PrintChat("<b><font color='#009aff'>Asuna</font></b>Condemn: <font color='#009aff'>Ultima</font> - Loaded!");
        }
    }
}