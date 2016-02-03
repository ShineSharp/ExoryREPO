using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Jinx
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
            Variables.Q = new Spell(SpellSlot.Q, ObjectManager.Player.BoundingRadius*2 + 525f);
            Variables.Q2 = new Spell(SpellSlot.Q, Variables.Q.Range + (50 + (25f * ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Q).Level)));
            Variables.W = new Spell(SpellSlot.W, 1500f);
            Variables.E = new Spell(SpellSlot.E, 900f);
            Variables.R = new Spell(SpellSlot.R, 4000f);

            Variables.W.SetSkillshot(0.6f, 60f, 3300f, true, SkillshotType.SkillshotLine);
            Variables.E.SetSkillshot(0.4f, 50f, 2000f, false, SkillshotType.SkillshotCircle);
            Variables.R.SetSkillshot(0.6f, 140f, 1700f, false, SkillshotType.SkillshotLine);
        }
    }
}
