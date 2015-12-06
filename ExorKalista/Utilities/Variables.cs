namespace ExorKalista
{
    using System;
    using System.Collections.Generic;

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
        /// Gets or sets the Whitelist menu.
        /// </summary>
        public static Menu WhiteListMenu { get; set; }

        /// <summary>
        /// Gets or sets the Drawings menu.
        /// </summary>
        public static Menu DrawingsMenu { get; set; }

        /// <summary>
        /// Gets or sets the Soulbound.
        /// </summary>
        public static Obj_AI_Hero SoulBound { get; set; }

        /// <summary>
        /// Gets or sets the orbwalker.
        /// </summary>
        public static Orbwalking.Orbwalker Orbwalker { get; set; }

        /// <summary>
        /// The main menu name.
        /// </summary>
        public static readonly string MainMenuCodeName = "[Exor<font color='#FFF000'>AIO</font>]: Ultima";

        /// <summary>
        /// The main menu codename.
        /// </summary>
        public static readonly string MainMenuName = $"exor.{ObjectManager.Player.ChampionName}";

        /// <summary>
        /// Gets the perfect damage reduction from sources.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>
        /// The damage dealt against all the sources
        /// </returns>
        public static float GetPerfectRendDamage(Obj_AI_Base target)
        {
            /// <summary>
            /// Gets the reduction from protected and the revivable targets.
            /// </summary>
            /// <param name="target">The target.</param>
            /// <returns>
            /// 0
            /// </returns>
            if (!Bools.HasNoProtection((Obj_AI_Hero)target))
            {
                return 0f;
            }
            
            var RendDamage = (float)(ObjectManager.Player.CalcDamage(target, LeagueSharp.Common.Damage.DamageType.Physical, Variables.E.GetDamage(target)));

            /// <summary>
            /// Gets the reduction from the baron nashor.
            /// </summary>
            /// <returns>
            /// Common calculation *= 0.5 since the baron halves all the incoming damage.
            /// </returns>
            if (ObjectManager.Player.HasBuff("barontarget"))
            {
                RendDamage *= 0.5f;
            }

            /// <summary>
            /// Gets the reduction from the dragon.
            /// </summary>
            /// <returns>
            /// Common calculation with dragonbuffstacking management (1 - .07f per stack).
            /// </returns>
            if (ObjectManager.Player.HasBuff("s5test_dragonslayerbuff"))
            {
                RendDamage *= (1 - (.07f * ObjectManager.Player.GetBuffCount("s5test_dragonslayerbuff")));
            }
            /// <summary>
            /// Gets the reduction from the exhaust spell.
            /// </summary>
            /// <returns>
            /// Common calculation *= 0.4f since you only do 40% of you total damage while exhausted.
            /// </returns>
            if (ObjectManager.Player.HasBuff("summonerexhaust"))
            {
                RendDamage *= 0.4f;
            }

            /// <summary>
            /// Gets the reduction from the Alistar R.
            /// </summary>
            /// <returns>
            /// Common calculation *= 0.35f since you only do 35% of you total damage while alistar is in ultimate stance.
            /// </returns>
            if (target.HasBuff("FerociousHowl"))
            {
                RendDamage *= 0.35f;
            }
            
            return RendDamage - 20; // First world problems Kappa
        }
    }
}
