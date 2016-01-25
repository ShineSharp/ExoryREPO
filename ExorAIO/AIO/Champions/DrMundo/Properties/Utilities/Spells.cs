namespace ExorAIO.Champions.DrMundo
{
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
            Variables.Q = new Spell(SpellSlot.Q, 1000f);
            Variables.W = new Spell(SpellSlot.W, ObjectManager.Player.BoundingRadius + 162.5f);
            Variables.E = new Spell(SpellSlot.E, ObjectManager.Player.BoundingRadius + 150f);
            Variables.R = new Spell(SpellSlot.R);
            
            Variables.Q.SetSkillshot(0.25f, 60f, 2000f, true, SkillshotType.SkillshotLine);
        }
    }
}
