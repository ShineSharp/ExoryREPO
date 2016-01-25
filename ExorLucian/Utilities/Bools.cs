namespace ExorLucian
{
    using LeagueSharp;
    using LeagueSharp.Common;

    /// <summary>
    /// The bools class.
    /// </summary>
    class Bools
    {
        /// <summary>
        /// Returns true if the target is immobile.
        /// </summary>
        public static bool IsImmobile(Obj_AI_Hero Target)
        => 
            Target.HasBuffOfType(BuffType.Stun) ||
            Target.HasBuffOfType(BuffType.Snare) ||
            Target.HasBuffOfType(BuffType.Knockup) ||
            Target.HasBuffOfType(BuffType.Charm) ||
            Target.HasBuffOfType(BuffType.Flee) || 
            Target.HasBuffOfType(BuffType.Taunt) ||
            Target.HasBuffOfType(BuffType.Suppression);

        /// <summary>
        /// Returns true if the target has no protection.
        /// </summary>   
        public static bool HasNoProtection(Obj_AI_Base target)
        =>
            target != null &&
            !target.IsInvulnerable &&
            !target.HasBuffOfType(BuffType.SpellShield);
    }
}
