using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Quinn
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
            Variables.Q = new Spell(SpellSlot.Q, 1000f);
            Variables.W = new Spell(SpellSlot.W, 2100f);
            Variables.E = new Spell(SpellSlot.E, 700f);
            Variables.R = new Spell(SpellSlot.R);

            Variables.Q.SetSkillshot(0.25f, 90f, 1550f, true, SkillshotType.SkillshotLine);
            Variables.E.SetTargetted(0.25f, 2000f);
        }
    }
}
