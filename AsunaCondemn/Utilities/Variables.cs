using LeagueSharp;
using LeagueSharp.Common;

namespace AsunaCondemn
{
    /// <summary>
    /// The Variables class.
    /// </summary>
    class Variables
    {
        /// <summary>
        /// Gets or sets the E Spell.
        /// </summary>
        public static Spell E { get; set; }

        /// <summary>
        /// Gets or sets the Flash SpellSlot.
        /// </summary>
        public static SpellSlot Flash { get; set; }

        /// <summary>
        /// Gets or sets the assembly menu.
        /// </summary>
        public static Menu Menu { get; set; }

        /// <summary>
        /// Gets or sets the E Spell menu.
        /// </summary>
        public static Menu EMenu { get; set; }

        /// <summary>
        /// Gets or sets the Drawings menu.
        /// </summary>
        public static Menu DrawingsMenu { get; set; }

        /// <summary>
        /// The main menu codename.
        /// </summary>
        public static readonly string MainMenuName = $"asuna.{ObjectManager.Player.ChampionName}";
    }
}
