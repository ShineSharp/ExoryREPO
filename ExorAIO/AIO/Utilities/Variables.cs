using System;
using System.Collections.Generic;

using LeagueSharp;
using LeagueSharp.Common;

using ItemData = LeagueSharp.Common.Data.ItemData;

using Orbwalking = SFXTargetSelector.Orbwalking;

namespace ExorAIO.Utilities
{
    /// <summary>
    /// The Variables class.
    /// </summary>
    class Variables
    {
        /// <summary>
        /// Gets the range increasement from the rapidfire cannon item.
        /// </summary>
        public static float GetRapidFireCannonIncreasement(float sum)
        {
            float tot = (sum / 100f) * 35f;

            if (!ItemData.Rapid_Firecannon.GetItem().IsReady() ||
                ObjectManager.Player.GetBuffCount("itemstatikshankcharge") < 100)
            {
                tot = 0f;
            }
            
            if (tot > 150f)
            {
                tot = 150f;
            }
            
            return tot;
        }

        /// <summary>
        /// Gets or sets the Q Spell.
        /// </summary>
        public static Spell Q { get; set; }

        /// <summary>
        /// Gets or sets the W Spell.
        /// </summary>
        public static Spell W { get; set; }

        /// <summary>
        /// Gets or sets the E Spell.
        /// </summary>
        public static Spell E { get; set; }

        /// <summary>
        /// Gets or sets the R Spell.
        /// </summary>
        public static Spell R { get; set; }

        /// <summary>
        /// Gets or sets the assembly menu.
        /// </summary>
        public static Menu Menu { get; set; }

        /// <summary>
        /// Gets or sets the orbwalker menu.
        /// </summary>
        public static Menu OrbwalkerMenu { get; set; }

        /// <summary>
        /// Gets or sets the targetselector menu.
        /// </summary>
        public static Menu TargetSelectorMenu { get; set; }

        /// <summary>
        /// Gets or sets the settings menu.
        /// </summary>
        public static Menu SettingsMenu { get; set; }

        /// <summary>
        /// Gets or sets the Q Spell menu.
        /// </summary>
        public static Menu QMenu { get; set; }

        /// <summary>
        /// Gets or sets the W Spell menu.
        /// </summary>        
        public static Menu WMenu { get; set; }

        /// <summary>
        /// Gets or sets the E Spell menu.
        /// </summary>
        public static Menu EMenu { get; set; }

        /// <summary>
        /// Gets or sets the R Spell menu.
        /// </summary>
        public static Menu RMenu { get; set; }

        /// <summary>
        /// Gets or sets the Miscellaneous menu.
        /// </summary>
        public static Menu MiscMenu { get; set; }

        /// <summary>
        /// Gets or sets the Whitelist menu.
        /// </summary>
        public static Menu WhiteListMenu { get; set; }

        /// <summary>
        /// Gets or sets the Drawings menu.
        /// </summary>
        public static Menu DrawingsMenu { get; set; }

        /// <summary>
        /// Gets or sets the orbwalker.
        /// </summary>
        public static Orbwalking.Orbwalker Orbwalker { get; set; }

        /// <summary>
        /// The main menu name.
        /// </summary>
        public static readonly string MainMenuCodeName = "[ExorAIO]: Ultima";

        /// <summary>
        /// The main menu codename.
        /// </summary>
        public static readonly string MainMenuName = $"exoraio.{ObjectManager.Player.ChampionName}";

        /// <summary>
        /// The supported champions list.
        /// </summary>
        public static readonly string LoadableChampions = "Ashe, Cassiopeia, Corki, DrMundo, Ezreal, Graves, Jinx, KogMaw, Lucian, Nasus, Olaf, Renekton, Sivir, Tristana, Vayne.";

        /// <summary>
        /// Kurumi is a god.
        /// </summary>
        public static readonly string Kappa = "jesuske12, Im gay 69.";
    }
}
