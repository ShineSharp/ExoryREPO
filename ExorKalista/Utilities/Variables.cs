namespace ExorKalista
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LeagueSharp;
    using LeagueSharp.Common;

    using Orbwalking = SFXTargetSelector.Orbwalking;
    using TargetSelector = SFXTargetSelector.TargetSelector;

    /// <summary>
    /// The Variables class.
    /// </summary>
    class Variables
    {
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
        /// Gets or sets the Drawings menu.
        /// </summary>
        public static Menu DrawingsMenu { get; set; }

        /// <summary>
        /// Gets or sets the Whitelist menu.
        /// </summary>
        public static Menu WhiteListMenu { get; set; }

        /// <summary>
        /// Gets or sets the Soulbound.
        /// </summary>
        public static Obj_AI_Hero SoulBound { get; set; }

        /// <summary>
        /// Gets or sets the orbwalker.
        /// </summary>
        public static Orbwalking.Orbwalker Orbwalker { get; set; }

        /// <summary>
        /// The main menu codename.
        /// </summary>
        public static readonly string MainMenuName = $"exor.{ObjectManager.Player.ChampionName}";

        /// <summary>
        /// The jungle HP bar offset.
        /// </summary>      
        internal class JungleHpBarOffset
        {
            internal string BaseSkinName;
            internal int Height;
            internal int Width;
            internal int XOffset;
            internal int YOffset;
        }

        /// <summary>
        /// The jungle HP bar offset list.
        /// </summary>
        internal static readonly List<JungleHpBarOffset> JungleHpBarOffsetList = new List<JungleHpBarOffset>
        {
            new JungleHpBarOffset{BaseSkinName = "SRU_Dragon", Width = 140, Height = 4, XOffset = 12, YOffset = 24},
            new JungleHpBarOffset{BaseSkinName = "SRU_Baron", Width = 190, Height = 10, XOffset = 16, YOffset = 24},
            new JungleHpBarOffset{BaseSkinName = "SRU_RiftHerald", Width = 139, Height = 6, XOffset = 12, YOffset = 22},
            new JungleHpBarOffset{BaseSkinName = "SRU_Red", Width = 139, Height = 4, XOffset = 12, YOffset = 24},
            new JungleHpBarOffset{BaseSkinName = "SRU_RedMini", Width = 49, Height = 2, XOffset = 1, YOffset = 5},
            new JungleHpBarOffset{BaseSkinName = "SRU_Blue", Width = 139, Height = 4, XOffset = 12, YOffset = 24},
            new JungleHpBarOffset{BaseSkinName = "SRU_BlueMini", Width = 49, Height = 2, XOffset = 1, YOffset = 5},
            new JungleHpBarOffset{BaseSkinName = "SRU_BlueMini2", Width = 49, Height = 2, XOffset = 1, YOffset = 5},
            new JungleHpBarOffset{BaseSkinName = "SRU_Gromp", Width = 86, Height = 2, XOffset = 1, YOffset = 7},
            new JungleHpBarOffset{BaseSkinName = "Sru_Crab", Width = 61, Height = 2, XOffset = 1, YOffset = 5},
            new JungleHpBarOffset{BaseSkinName = "SRU_Krug", Width = 79, Height = 2, XOffset = 1, YOffset = 7},
            new JungleHpBarOffset{BaseSkinName = "SRU_KrugMini", Width = 55, Height = 2, XOffset = 1, YOffset = 5},
            new JungleHpBarOffset{BaseSkinName = "SRU_Razorbeak", Width = 74, Height = 2, XOffset = 1, YOffset = 7},
            new JungleHpBarOffset{BaseSkinName = "SRU_RazorbeakMini", Width = 49, Height = 2, XOffset = 1, YOffset = 5},
            new JungleHpBarOffset{BaseSkinName = "SRU_Murkwolf", Width = 74, Height = 2, XOffset = 1, YOffset = 7},
            new JungleHpBarOffset{BaseSkinName = "SRU_MurkwolfMini", Width = 55, Height = 2, XOffset = 1, YOffset = 5}
        };
    }
}
