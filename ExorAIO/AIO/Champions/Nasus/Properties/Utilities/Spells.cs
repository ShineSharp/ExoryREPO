using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Nasus
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
            Variables.Q = new Spell(SpellSlot.Q, ObjectManager.Player.BoundingRadius*2 + 150f);
            Variables.W = new Spell(SpellSlot.W, 600f);
            Variables.E = new Spell(SpellSlot.E, 650f);
            Variables.R = new Spell(SpellSlot.R, ObjectManager.Player.BoundingRadius + 175f);

            Variables.W.SetTargetted(0.25f, float.MaxValue);
            Variables.E.SetSkillshot(0.25f, 400f, float.MaxValue, false, SkillshotType.SkillshotCircle);
        }
    }
}
