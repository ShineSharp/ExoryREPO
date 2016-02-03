using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Tryndamere
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
            Variables.W = new Spell(SpellSlot.W, 400f);
            Variables.E = new Spell(SpellSlot.E, 660f);
            Variables.R = new Spell(SpellSlot.R);
            
            Variables.E.SetSkillshot(0.25f, 93f, 1300f, false, SkillshotType.SkillshotLine);
        }
    }
}
