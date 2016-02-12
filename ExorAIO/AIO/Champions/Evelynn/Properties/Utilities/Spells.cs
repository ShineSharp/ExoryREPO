using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Evelynn
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
            Variables.Q = new Spell(SpellSlot.Q, 500f);
            Variables.W = new Spell(SpellSlot.W);
            Variables.E = new Spell(SpellSlot.E, ObjectManager.Player.BoundingRadius + 225f);
            Variables.R = new Spell(SpellSlot.R, 650f);

            Variables.E.SetTargetted(0.25f, 1000f);
            Variables.R.SetSkillshot(0.25f, 250f, float.MaxValue, false, SkillshotType.SkillshotCircle);
        }
    }
}
