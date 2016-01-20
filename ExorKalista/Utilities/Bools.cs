namespace ExorKalista
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    /// <summary>
    /// The bools class.
    /// </summary>
    public class Bools
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
            !target.IsInvulnerable &&
            !target.HasBuffOfType(BuffType.SpellShield);

        /// <summary>
        /// Returns true if the target is a perfectly valid rend target.
        /// </summary>   
        public static bool IsPerfectRendTarget(Obj_AI_Base target)
        =>
            target != null &&
            !target.IsDead &&
            !target.IsZombie &&
            Variables.E.CanCast(target) &&
            Bools.HasNoProtection(target) &&
            target.IsValidTarget(Variables.E.Range) &&
            target.GetBuffCount("kalistaexpungemarker") > 0;

        /// <summary>
        /// Returns true if the target is killable by Rend.
        /// </summary>   
        public static bool IsKillableRendTarget(Obj_AI_Base target)
        =>
            target.Health < Variables.GetPerfectRendDamage(target);
    }
}
