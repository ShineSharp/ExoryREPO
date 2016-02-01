using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Lux
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
            Variables.Q = new Spell(SpellSlot.Q, 1250f);
            Variables.W = new Spell(SpellSlot.W, 1075f);
            Variables.E = new Spell(SpellSlot.E, 1100f);
            Variables.R = new Spell(SpellSlot.R, 3500f);

            Variables.Q.SetSkillshot(0.25f, 70f, 1200f, true, SkillshotType.SkillshotLine);
            Variables.W.SetSkillshot(0.25f, 150f, 1000f, false, SkillshotType.SkillshotLine);
            Variables.E.SetSkillshot(0.25f, 275f, 1300f, false, SkillshotType.SkillshotCircle);
            Variables.R.SetSkillshot(1f, 190f, float.MaxValue, false, SkillshotType.SkillshotLine);
        }
    }
}
