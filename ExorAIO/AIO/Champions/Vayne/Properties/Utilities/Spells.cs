using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Vayne
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
            Variables.Q = new Spell(SpellSlot.Q, ObjectManager.Player.BoundingRadius + (ObjectManager.Player.AttackRange + 300f));
            Variables.W = new Spell(SpellSlot.W);
            Variables.E = new Spell(SpellSlot.E, ObjectManager.Player.BoundingRadius*2 + 550f);

            //Variables.E.SetTargetted(0.25f, 1250f);
            Variables.E.SetTargetted(0.413f, 1000f);
        }
    }
}
