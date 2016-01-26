using LeagueSharp;
using LeagueSharp.Common;

namespace NabbActivator
{
    /// <summary>
    /// The spells class.
    /// </summary>
    class ISpells
    {
        /// <summary>
        /// Sets the spells.
        /// </summary>
        public static void Initialize()
        {
            Variables.W = new Spell(SpellSlot.W);
        }
    }
}
