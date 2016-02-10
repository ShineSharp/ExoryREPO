using LeagueSharp;
using LeagueSharp.Common;

namespace ExorLucian
{
    /// <summary>
    /// The bools class.
    /// </summary>
    class Bools
    {
        /// <summary>
        /// Gets a value indicating whether the target has protection or not.
        /// </summary>
        /// <value>
        /// <c>true</c> if the has no protection.; otherwise, <c>false</c>.
        /// </value> 
        public static bool IsSpellShielded(Obj_AI_Hero unit)
        =>
            unit.HasBuffOfType(BuffType.SpellShield) ||
            unit.HasBuffOfType(BuffType.SpellImmunity) ||
            Utils.TickCount - unit.LastCastedSpellT() < 300 &&
            (
                unit.LastCastedSpellName().Equals("SivirE") ||
                unit.LastCastedSpellName().Equals("BlackShield") ||
                unit.LastCastedSpellName().Equals("NocturneShit")
            );

        /// <summary>
        /// Gets a value indicating whether a determined champion can move or not.
        /// </summary>
        /// <value>
        /// <c>true</c> if the target can't move.; otherwise, <c>false</c>.
        /// </value>
        public static bool IsImmobile(Obj_AI_Hero target)
        => 
            target.HasBuffOfType(BuffType.Stun) ||
            target.HasBuffOfType(BuffType.Flee) ||
            target.HasBuffOfType(BuffType.Snare) ||
            target.HasBuffOfType(BuffType.Taunt) ||
            target.HasBuffOfType(BuffType.Charm) ||
            target.HasBuffOfType(BuffType.Knockup) ||
            target.HasBuffOfType(BuffType.Suppression);
    }
}
