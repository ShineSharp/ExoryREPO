using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Ryze
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
            Variables.Q = new Spell(SpellSlot.Q, 900f);
            Variables.W = new Spell(SpellSlot.W, 600f);
            Variables.E = new Spell(SpellSlot.E, 600f);
            Variables.R = new Spell(SpellSlot.R);
            
            Variables.Q.SetSkillshot(0.25f, 55f, 1400f, true, SkillshotType.SkillshotLine);
            Variables.W.SetTargetted(0.25f, float.MaxValue);
            Variables.E.SetTargetted(0.25f, 1400f);
        }
    }
}
