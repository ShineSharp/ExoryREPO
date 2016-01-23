namespace NabbActivator
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    /// <summary>
    /// The variables.
    /// </summary>
    public class Variables
    {
        /// <summary>
        /// Gets or sets the assembly menu.
        /// </summary>
        public static Menu Menu { get; set; }

        /// <summary>
        /// Gets or sets the slider menu.
        /// </summary>
        public static Menu SliderMenu { get; set; }

        /// <summary>
        /// Gets or sets the W Spell.
        /// </summary>
        public static Spell W { get; set; }

        /// <summary>
        /// The main menu codename.
        /// </summary>
        public static string MainMenuName => "nabbactivator.menu";

        /// <summary>
        /// Contains a list of the names of the champions who do not use Mana as second bar.
        /// </summary>
        public static readonly string[] NoManaChampions = 
        {
            "Aatrox", "DrMundo", "Mordekaiser",
            "Vladimir", "Zac", "Garen",
            "Gnar", "Katarina", "RekSai",
            "Renekton", "Rengar", "Riven",
            "Rumble", "Shyvana", "Tryndamere",
            "Yasuo",
        };
    }
}
