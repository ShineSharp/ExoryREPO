using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.KogMaw
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
            Variables.W = new Spell(SpellSlot.W, ObjectManager.Player.AttackRange + ObjectManager.Player.BoundingRadius*2 + (60f + (30f * ObjectManager.Player.Spellbook.GetSpell(SpellSlot.W).Level)));
            Variables.E = new Spell(SpellSlot.E, 1300f);
            Variables.R = new Spell(SpellSlot.R, 900f + (300f * ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).Level));

            Variables.Q.SetSkillshot(0.25f, 50f, 2000f, true, SkillshotType.SkillshotLine);
            Variables.E.SetSkillshot(0.25f, 120f, 1350f, false, SkillshotType.SkillshotLine);
            Variables.R.SetSkillshot(1.2f, 100f, float.MaxValue, false, SkillshotType.SkillshotCircle);
        }
    }
}
