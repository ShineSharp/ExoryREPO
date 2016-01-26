using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Cassiopeia
{
    using System;
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
            Variables.Q = new Spell(SpellSlot.Q, 850f);
            Variables.W = new Spell(SpellSlot.W, 850f);
            Variables.E = new Spell(SpellSlot.E, 700f);
            Variables.R = new Spell(SpellSlot.R, 775f);

            Variables.Q.SetSkillshot(0.75f, 100f, float.MaxValue, false, SkillshotType.SkillshotCircle);
            Variables.W.SetSkillshot(0.75f, 150f, float.MaxValue, false, SkillshotType.SkillshotCircle);
            Variables.E.SetTargetted(0.125f, 1000f);
            Variables.R.SetSkillshot(0.60f, (float)(80 * Math.PI / 180), float.MaxValue, false, SkillshotType.SkillshotCone);
        }
    }
}
