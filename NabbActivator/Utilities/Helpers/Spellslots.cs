using LeagueSharp;
using LeagueSharp.Common;

namespace NabbActivator
{
    /// <summary>
    /// The spellslots.
    /// </summary>
    class SpellSlots
    {
        /// <summary>
        /// Gets the Heal SpellSlot.
        /// </summary>
        public static SpellSlot Heal => ObjectManager.Player.GetSpellSlot("summonerheal");

        /// <summary>
        /// Gets the Ignite SpellSlot.
        /// </summary>
        public static SpellSlot Ignite => ObjectManager.Player.GetSpellSlot("summonerdot");

        /// <summary>
        /// Gets the Ghost SpellSlot.
        /// </summary>
        public static SpellSlot Ghost => ObjectManager.Player.GetSpellSlot("summonerhaste");
        
        /// <summary>
        /// Gets the Clarity SpellSlot.
        /// </summary>
        public static SpellSlot Clarity => ObjectManager.Player.GetSpellSlot("summonermana");

        /// <summary>
        /// Gets the Cleanse SpellSlot.
        /// </summary>
        public static SpellSlot Cleanse => ObjectManager.Player.GetSpellSlot("summonerboost");

        /// <summary>
        /// Gets the Exhaust SpellSlot.
        /// </summary>
        public static SpellSlot Exhaust => ObjectManager.Player.GetSpellSlot("summonerexhaust");

        /// <summary>
        /// Gets the Barrier SpellSlot.
        /// </summary>
        public static SpellSlot Barrier => ObjectManager.Player.GetSpellSlot("summonerbarrier");
    }
}
