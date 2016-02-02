using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Sivir
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
            Variables.W = new Spell(SpellSlot.W);
            Variables.E = new Spell(SpellSlot.E);

            Variables.Q.SetSkillshot(0.25f, 90f, 1350f, false, SkillshotType.SkillshotLine);
        }
    }
}
