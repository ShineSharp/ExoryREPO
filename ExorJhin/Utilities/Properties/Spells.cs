using LeagueSharp;
using LeagueSharp.Common;

namespace ExorJhin
{
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
            Variables.Q = new Spell(SpellSlot.Q, 600f);
            Variables.W = new Spell(SpellSlot.W, 2500f);
            Variables.E = new Spell(SpellSlot.E, 750f);
            Variables.R = new Spell(SpellSlot.R, 3000f);

            Variables.W.SetSkillshot(0.75f, 40f, 5000f, false, SkillshotType.SkillshotLine);
            Variables.E.SetSkillshot(1.3f, 260f, 1600f, false, SkillshotType.SkillshotCircle);
            Variables.R.SetSkillshot(0.25f, 80f, 5000f, false, SkillshotType.SkillshotLine);
        }
    }
}
