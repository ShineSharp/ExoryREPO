namespace ExorAIO.Champions.Ashe
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
            Variables.Q = new Spell(SpellSlot.Q);
            Variables.W = new Spell(SpellSlot.W, ObjectManager.Player.BoundingRadius + 1200f);
            Variables.E = new Spell(SpellSlot.E, float.MaxValue);
            Variables.R = new Spell(SpellSlot.R, 3500f);

            Variables.W.SetSkillshot(0.25f, (float)(60f * Math.PI / 180), 1500f, true, SkillshotType.SkillshotCone);
            Variables.R.SetSkillshot(0.25f, 130f, 1600f, false, SkillshotType.SkillshotLine);
        }
    }
}
