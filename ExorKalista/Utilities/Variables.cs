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
            float RendDamage = (float)ObjectManager.Player.CalcDamage(target, LeagueSharp.Common.Damage.DamageType.Physical, Variables.E.GetDamage(target));
            float Calculator = 1f;
            float healthDebuffer = 0f;

            /// <summary>
            /// Gets the reduction from the exhaust spell.
            /// </summary>
            /// <returns>
            /// You deal 40% of you total damage while exhausted.
            /// </returns>
            if (ObjectManager.Player.HasBuff("summonerexhaust"))
            {
                Calculator *= 0.60f;
            }

            /// <summary>
            /// Gets the reduction from the baron nashor.
            /// </summary>
            /// <returns>
            /// You deal 50% reduced damage to Baron Nashor.
            /// </returns>
            if (target.CharData.BaseSkinName.Equals("SRU_Baron") &&
                ObjectManager.Player.HasBuff("barontarget"))
            {
                Calculator *= 0.50f;
            }

            /// <summary>
            /// Gets the reduction from the dragon.
            /// </summary>
            /// <returns>
            /// The Dragon receives 7% reduced damage per stack.
            /// </returns>
            if (target.CharData.BaseSkinName.Equals("SRU_Dragon") &&
                ObjectManager.Player.GetBuffCount("s5test_dragonslayerbuff") > 0)
            {
                Calculator *= 1f - (ObjectManager.Player.GetBuffCount("s5test_dragonslayerbuff") * 0.07f);
            }

            /// <summary>
            /// Gets the reduction from Alistar's R.
            /// </summary>
            /// <returns>
            /// You deal 70% reducted damage to Alistar if he's in Ultimate Stance.
            /// </returns>
            if (target.HasBuff("FerociousHowl"))
            {
                Calculator *= 0.30f;
            }

            /// <summary>
            /// Gets the reduction from Garen's W.
            /// </summary>
            /// <returns>
            /// You deal 30% reducted damage to Garen if he's in W Stance.
            /// </returns>
            if (target.HasBuff("GarenW"))
            {
                Calculator *= 0.70f;
            }

            /// <summary>
            /// Gets the reduction from Master Yi's E.
            /// </summary>
            /// <returns>
            /// You deal 45% reduced damage + Master Yi's W Level (70% Reducted Damage at Level 5).
            /// </returns>
            if (target.HasBuff("Medidate"))
            {
                Calculator *= 0.55f - (target.Spellbook.GetSpell(SpellSlot.W).Level * 0.05f);
            }

            /// <summary>
            /// Gets the reduction from Gragas's W.
            /// </summary>
            /// <returns>
            /// You deal 8% reduced damage + Gragas's W Level (18% Reducted Damage at Level 5).
            /// </returns>
            if (target.HasBuff("gragaswself"))
            {
                Calculator *= 0.92f + (target.Spellbook.GetSpell(SpellSlot.W).Level * 0.02f);
            }

            /// <summary>
            /// Gets the predicted reduction from Blitacrank Shield.
            /// </summary>
            if (!target.HasBuff("BlitzcrankManaBarrierCD") &&
                !target.HasBuff("ManaBarrier"))
            {
                healthDebuffer += target.Mana / 2;
            }
            
            return RendDamage * Calculator;
        }
    }
}
