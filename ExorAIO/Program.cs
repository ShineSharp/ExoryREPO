using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += OnGameLoad;
        }

        private static void OnGameLoad(EventArgs args)
        {
            AIO.OnLoad();
        }
    }
}
