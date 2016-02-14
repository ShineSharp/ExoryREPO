using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Nautilus
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
            Variables.Q = new Spell(SpellSlot.Q, 1100f);
            Variables.W = new Spell(SpellSlot.W, 175f);
            Variables.E = new Spell(SpellSlot.E, 600f);
            Variables.R = new Spell(SpellSlot.R, 825f);

            Variables.Q.SetSkillshot(0.25f, 90f, 2000f, true, SkillshotType.SkillshotLine);
        }
    }
}
