using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Draven
{
    using ExorAIO.Utilities;

    /// <summary>
    /// The spell class.
    /// </summary>
    class Spells
    {
        /// <summary>
        /// Sets the spells.
        /// </summary>
        public static void Initialize()
        {
            Variables.Q = new Spell(SpellSlot.Q);
            Variables.W = new Spell(SpellSlot.W);
            Variables.E = new Spell(SpellSlot.E, 1050f);
            Variables.R = new Spell(SpellSlot.R, 1500f);

            Variables.E.SetSkillshot(0.25f, 130f, 1400f, false, SkillshotType.SkillshotLine);
            Variables.R.SetSkillshot(0.4f, 160f, 2000f, false, SkillshotType.SkillshotLine);
        }
    }
}
