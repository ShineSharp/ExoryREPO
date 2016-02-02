using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Tristana
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
            Variables.Q = new Spell(SpellSlot.Q);
            Variables.E = new Spell(SpellSlot.E, ObjectManager.Player.BoundingRadius + 543f + (7 * ObjectManager.Player.Level));
            Variables.R = new Spell(SpellSlot.R, ObjectManager.Player.BoundingRadius + 543f + (7 * ObjectManager.Player.Level));

            Variables.E.SetTargetted(0.25f, 2400f);
            Variables.R.SetTargetted(0.25f, 2000f);
        }
    }
}
