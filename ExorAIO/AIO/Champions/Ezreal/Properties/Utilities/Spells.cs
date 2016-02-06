using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Ezreal
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
            Variables.Q = new Spell(SpellSlot.Q, 1150f);
            Variables.W = new Spell(SpellSlot.W, 1000f);
            Variables.R = new Spell(SpellSlot.R, 1500f);

            Variables.Q.SetSkillshot(0.25f, 60f, 2000f, true, SkillshotType.SkillshotLine);
            Variables.W.SetSkillshot(0.25f, 80f, 1600f, false, SkillshotType.SkillshotLine);
            Variables.R.SetSkillshot(1f, 160f, 2000f, false, SkillshotType.SkillshotLine);
        }
    }
}
