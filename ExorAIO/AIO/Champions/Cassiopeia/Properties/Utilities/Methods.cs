using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Cassiopeia
{
    /// <summary>
    /// The methods class.
    /// </summary>
    class Methods
    {  
        /// <summary>
        /// Sets the methods.
        /// </summary>
        public static void Initialize()
        {
            Game.OnUpdate += Cassiopeia.OnUpdate;
        }
    }
}
