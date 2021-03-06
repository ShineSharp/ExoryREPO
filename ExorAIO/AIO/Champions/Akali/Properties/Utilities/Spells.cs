using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Akali
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
            Variables.Q = new Spell(SpellSlot.Q, 600f);
            Variables.E = new Spell(SpellSlot.E, 325f);
            Variables.R = new Spell(SpellSlot.R, 700f);

            Variables.Q.SetTargetted(0.25f, 1000f);
            Variables.R.SetTargetted(0.25f, 2000f);
        }
    }
}
