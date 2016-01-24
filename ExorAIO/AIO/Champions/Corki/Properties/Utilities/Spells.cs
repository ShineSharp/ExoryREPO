namespace ExorAIO.Champions.Corki
{
    using System;
    using LeagueSharp;
    using LeagueSharp.Common;
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
            Variables.Q = new Spell(SpellSlot.Q, 775f);
            Variables.E = new Spell(SpellSlot.E, 600f);
            Variables.R = new Spell(SpellSlot.R, 1250f);

            Variables.Q.SetSkillshot(0.3f, 250f, 1000f, false, SkillshotType.SkillshotCircle);
            Variables.E.SetSkillshot(0.3f, (float)(35f * Math.PI / 180), 1500f, false, SkillshotType.SkillshotCone);
            Variables.R.SetSkillshot(0.2f, 40f, 2000f, true, SkillshotType.SkillshotLine);
        }
    }
}
