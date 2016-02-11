using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Kalista
{
    using ExorAIO.Utilities;

    /// <summary>
    /// The settings class.
    /// </summary>
    class Spells
    {
        /// <summary>
        /// Sets the spells.
        /// </summary>
        public static void Initialize()
        {
            Variables.Q = new Spell(SpellSlot.Q, 1150f);
            Variables.W = new Spell(SpellSlot.W, 5000f);
            Variables.E = new Spell(SpellSlot.E, 1000f);
            Variables.R = new Spell(SpellSlot.R, 1400f);

            Variables.Q.SetSkillshot(0.25f, 40f, 2400f, true, SkillshotType.SkillshotLine);
        }
    }
}
