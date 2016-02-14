using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Caitlyn
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
            Variables.Q = new Spell(SpellSlot.Q, 1250f);
            Variables.W = new Spell(SpellSlot.W, 800f);
            Variables.E = new Spell(SpellSlot.E, 750f);
            Variables.R = new Spell(SpellSlot.R, 1500f + (500f * ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).Level));

            Variables.Q.SetSkillshot(0.625f, 90f, 2200f, false, SkillshotType.SkillshotLine);
            Variables.E.SetSkillshot(0.125f, 70f, 1600f, true, SkillshotType.SkillshotLine);
        }
    }
}
