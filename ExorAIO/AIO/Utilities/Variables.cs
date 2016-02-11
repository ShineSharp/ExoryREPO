using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Utilities
{
    using System.Collections.Generic;
    using SharpDX;
    using Orbwalking = SFXTargetSelector.Orbwalking;

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
        /// Gets or sets the 2nd stage of the Q Spell.
        /// </summary>
        public static Spell Q2 { get; set; }

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
        /// Gets or sets the Soulbound.
        /// </summary>
        public static Obj_AI_Hero SoulBound { get; set; }

        /// <summary>
        /// The main menu name.
        /// </summary>
        public static readonly string MainMenuCodeName = "ExorAIO: Ultima";

        /// <summary>
        /// The main menu codename.
        /// </summary>
        public static readonly string MainMenuName = $"exoraio.{ObjectManager.Player.ChampionName}";

        /// <summary>
        /// The supported champions list.
        /// </summary>
        public static readonly string[] LoadableChampions = 
        {
            "Akali", "Anivia", "Ashe", "Cassiopeia", "Corki",
            "Darius", "Draven", "DrMundo", "Ezreal", "Graves",
            "Jax", "Jhin", "Jinx", "Kalista", "KogMaw",
            "Lucian", "Lux", "Olaf", "Quinn", "Renekton",
            "Ryze", "Sivir", "Tristana", "Tryndamere", "Vayne"
        };

        /// <summary>
        /// The default enemy HP bar offset.
        /// </summary>
        public static int XOffset = 10;
        public static int YOffset = 20;
        public static int Width = 103;
        public static int Height = 8;

        /// <summary>
        /// The jungle HP bar offset.
        /// </summary>      
        internal class JungleHpBarOffset
        {
            internal int Height;
            internal int Width;
            internal int XOffset;
            internal int YOffset;
            internal string BaseSkinName;
        }

        /// <summary>
        /// Gets all the important jungle locations.
        /// </summary>
        internal static readonly List<Vector3> Locations = new List<Vector3>
        {
            new Vector3(9827.56f,  4426.136f, -71.2406f),
            new Vector3(4951.126f, 10394.05f, -71.2406f),
            new Vector3(10998.14f, 6954.169f, 51.72351f),
            new Vector3(7082.083f, 10838.25f, 56.2041f),
            new Vector3(3804.958f, 7875.456f, 52.11121f),
            new Vector3(7811.249f, 4034.486f, 53.81299f)
        };

        /// <summary>
        /// The jungle HP bar offset list.
        /// </summary>
        internal static readonly List<JungleHpBarOffset> JungleHpBarOffsetList = new List<JungleHpBarOffset>
        {
            new JungleHpBarOffset{BaseSkinName = "SRU_Dragon",        Width = 140, Height = 4,  XOffset = 12, YOffset = 24},
            new JungleHpBarOffset{BaseSkinName = "SRU_Baron",         Width = 190, Height = 10, XOffset = 16, YOffset = 24},
            new JungleHpBarOffset{BaseSkinName = "SRU_RiftHerald",    Width = 139, Height = 6,  XOffset = 12, YOffset = 22},
            new JungleHpBarOffset{BaseSkinName = "SRU_Red",           Width = 139, Height = 4,  XOffset = 12, YOffset = 24},
            new JungleHpBarOffset{BaseSkinName = "SRU_Blue",          Width = 139, Height = 4,  XOffset = 12, YOffset = 24},
            new JungleHpBarOffset{BaseSkinName = "SRU_Gromp",         Width = 86,  Height = 2,  XOffset = 1,  YOffset = 7},
            new JungleHpBarOffset{BaseSkinName = "SRU_Crab",          Width = 61,  Height = 2,  XOffset = 1,  YOffset = 5},
            new JungleHpBarOffset{BaseSkinName = "SRU_Krug",          Width = 79,  Height = 2,  XOffset = 1,  YOffset = 7},
            new JungleHpBarOffset{BaseSkinName = "SRU_Razorbeak",     Width = 74,  Height = 2,  XOffset = 1,  YOffset = 7},
            new JungleHpBarOffset{BaseSkinName = "SRU_Murkwolf",      Width = 74,  Height = 2,  XOffset = 1,  YOffset = 7},
        };
    }
}
