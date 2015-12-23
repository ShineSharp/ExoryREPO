namespace NabbActivator
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    /// <summary>
    /// The spellslots.
    /// </summary>
    public class Managers
    {
        /// <summary>
        /// Sets the minimum necessary health percent to use a health potion.
        /// </summary>
        public static int MinHealthPercent
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.consumables.on_health_percent").GetValue<Slider>().Value;

        /// <summary>
        /// Sets the minimum necessary mana percent to use a mana potion.
        /// </summary>
        public static int MinManaPercent
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.consumables.on_mana_percent").GetValue<Slider>().Value;
    }

    /// <summary>
    /// The spellslots.
    /// </summary>
    public class SpellSlots
    {
        /// <summary>
        /// Gets the Cleanse SpellSlot.
        /// </summary>
        public static SpellSlot Cleanse
        =>
            ObjectManager.Player.GetSpellSlot("summonerboost");

        /// <summary>
        /// Gets the Heal SpellSlot.
        /// </summary>
        public static SpellSlot Heal
        =>
            ObjectManager.Player.GetSpellSlot("summonerheal");

        /// <summary>
        /// Gets the Ignite SpellSlot.
        /// </summary>
        public static SpellSlot Ignite
        =>
            ObjectManager.Player.GetSpellSlot("summonerdot");

        /// <summary>
        /// Gets the Barrier SpellSlot.
        /// </summary>
        public static SpellSlot Barrier
        =>
            ObjectManager.Player.GetSpellSlot("summonerbarrier");

        /// <summary>
        /// Gets the Clarity SpellSlot.
        /// </summary>
        public static SpellSlot Clarity
        => 
            ObjectManager.Player.GetSpellSlot("summonermana");

        /// <summary>
        /// Gets the Exhaust SpellSlot.
        /// </summary>
        public static SpellSlot Exhaust
        => 
            ObjectManager.Player.GetSpellSlot("summonerexhaust");

        /// <summary>
        /// Gets the Ghost SpellSlot.
        /// </summary>
        public static SpellSlot Ghost 
        => 
            ObjectManager.Player.GetSpellSlot("summonerhaste");
    }
}
