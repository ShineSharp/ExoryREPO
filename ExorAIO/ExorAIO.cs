using LeagueSharp;
using LeagueSharp.Common;
using ExorAIO.Core;

namespace ExorAIO
{
    class AIO
    {
        /// <summary>
        /// Loads the Assembly's core processes.
        /// </summary>
        public static void OnLoad()
        {
            Bootstrap.BuildMenu();
            Bootstrap.LoadChampion();
        }
    }
}
