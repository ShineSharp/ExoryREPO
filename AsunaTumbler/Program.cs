using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace AsunaTumbler
{
    class WallTumble
    {
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        static void Game_OnGameLoad(EventArgs args)
        {
            if (!ObjectManager.Player.ChampionName.Equals("Vayne") ||
                Utility.Map.GetMap().Type != Utility.Map.MapType.SummonersRift)
            {
                return;
            }

            WallTumbler.OnLoad();
            Game.PrintChat("<b><font color='#009aff'>Asuna</font></b>Tumbler: <font color='#009aff'>Ultima</font> - Loaded!");
        }
    }
}