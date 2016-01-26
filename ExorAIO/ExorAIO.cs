using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO
{
    using ExorAIO.Core;

    /// <summary>
    /// The AIO class.
    /// </summary>
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
