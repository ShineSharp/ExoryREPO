using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Graves
{
    using ExorAIO.Utilities;

    /// <summary>
    /// The spells class.
    /// </summary>
    class Spells
    {
        /// <summary>
        /// Sets the spells.
        /// </summary>
        public static void Initialize()
        {
            Variables.Q = new Spell(SpellSlot.Q, 800f);
            Variables.W = new Spell(SpellSlot.W, 900f);
            Variables.E = new Spell(SpellSlot.E);
            Variables.R = new Spell(SpellSlot.R, 1050f);

            Variables.Q.SetSkillshot(0.25f, 40f, 3000f, false, SkillshotType.SkillshotLine);
            Variables.W.SetSkillshot(0.25f, 250f, 1000f, false, SkillshotType.SkillshotCircle);
            Variables.R.SetSkillshot(0.25f, 100f, 2100f, false, SkillshotType.SkillshotLine);
        }
    }
}
