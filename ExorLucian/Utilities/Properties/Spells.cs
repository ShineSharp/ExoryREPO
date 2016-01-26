using LeagueSharp;
using LeagueSharp.Common;

namespace ExorLucian
{
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
            Variables.Q = new Spell(SpellSlot.Q, ObjectManager.Player.BoundingRadius*3 + 500f);
            Variables.W = new Spell(SpellSlot.W, 1000f);
            Variables.E = new Spell(SpellSlot.E, ObjectManager.Player.BoundingRadius + (ObjectManager.Player.AttackRange + 450f));
            Variables.R = new Spell(SpellSlot.R, 1350f);
            
            Variables.Q.SetTargetted(0.5f, float.MaxValue);
            Variables.W.SetSkillshot(0.25f, 55f, 1600f, true, SkillshotType.SkillshotLine);
            Variables.R.SetSkillshot(0.5f, 110f, 2800f, false, SkillshotType.SkillshotLine);
        }
    }
}
