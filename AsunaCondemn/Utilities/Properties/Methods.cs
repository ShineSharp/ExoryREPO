using LeagueSharp;
using LeagueSharp.Common;

namespace AsunaCondemn
{
    /// <summary>
    /// The methods class.
    /// </summary>
    class Methods
    {
        /// <summary>
        /// The methods.
        /// </summary>
        public static void Initialize()
        {
            Game.OnUpdate += Condem.OnUpdate;
        }
    }
}
