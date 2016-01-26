using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Jax
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
            Variables.Q = new Spell(SpellSlot.Q, 700f);
            Variables.W = new Spell(SpellSlot.W);
            Variables.E = new Spell(SpellSlot.E, ObjectManager.Player.BoundingRadius*2 + 187.5f);
            Variables.R = new Spell(SpellSlot.R);
        }
    }
}
