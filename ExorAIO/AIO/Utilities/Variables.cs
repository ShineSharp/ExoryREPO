using System;
using System.Collections.Generic;

using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Utilities
{
    /// <summary>
    /// The Variables class.
    /// </summary>
    class Variables
    {
        /// <summary>
        /// Gets the name of the playing character.
        /// </summary>
        public static string Name => ObjectManager.Player.ChampionName;
        
        /// <summary>
        /// Gets or sets the assembly menu.
        /// </summary>
        public static Menu Menu { get; set; }
        public static Menu SettingsMenu { get; set; }
        public static Menu QMenu { get; set; }
        public static Menu WMenu { get; set; }
        public static Menu EMenu { get; set; }
        public static Menu RMenu { get; set; } 
        public static Menu MiscMenu { get; set; } 
        public static Menu WhiteListMenu { get; set; } 
        public static Menu DrawingsMenu { get; set; }

        /// <summary>
        /// Gets the real attack speed float value.
        /// </summary>
        public static float AttackSpeed => 1 / ObjectManager.Player.AttackDelay;

        /// <summary>
        /// Gets the real attack damage float value.
        /// </summary> 
        public static float TotalAttackDamage(Obj_AI_Hero target)
        =>
            target.BaseAttackDamage + target.FlatPhysicalDamageMod;

        /// <summary>
        /// Orbwalker limiter for Kog'Maw.
        /// </summary>
        public static float OrbwalkingLimit =>
            (float)(Variables.Menu.Item($"{Variables.MainMenuName}.misc.wlimiterc").GetValue<Slider>().Value) + 
            (float)(Variables.Menu.Item($"{Variables.MainMenuName}.misc.wlimiterd").GetValue<Slider>().Value) / 100;

        /// <summary>
        /// Gets the R Stacks for Kog'Maw.
        /// </summary>
        public static int GetRStacks() => ObjectManager.Player.GetBuffCount("kogmawlivingartillerycost");

        /// <summary>
        /// Defines the spells.
        /// </summary>
        public static Spell Q, W, E ,R;
        
        /// <summary>
        /// Gets or sets the orbwalker menu.
        /// </summary>
        public static Menu OrbwalkerMenu { get; set; }

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
        /// The supported champions.
        /// </summary>
        public static readonly string LoadableChampions = "Ashe, Cassiopeia, Corki, Graves, KogMaw, Renekton, Sivir, Tristana, Vayne.";
    }
}
