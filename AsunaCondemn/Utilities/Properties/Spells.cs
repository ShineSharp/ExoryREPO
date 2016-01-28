using LeagueSharp;
using LeagueSharp.Common;

namespace AsunaCondemn
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
            Variables.E = new Spell(SpellSlot.E, ObjectManager.Player.BoundingRadius*2 + 550f);
            Variables.Flash = ObjectManager.Player.GetSpellSlot("summonerflash");

            Variables.E.SetTargetted(0.413f, 1250f);
        }
    }
}
