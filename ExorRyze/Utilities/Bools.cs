using LeagueSharp;
using LeagueSharp.Common;

namespace ExorRyze
{
    using System.Linq;

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

        /// <summary>
        /// Gets a value indicating whether a determined champion has a stackable item.
        /// </summary>
        /// <value>
        /// <c>true</c> if the player has a tear item.; otherwise, <c>false</c>.
        /// </value>
        public static bool HasTear(Obj_AI_Hero target) 
        =>
            target.InventoryItems
                .Any(item => 
                    item.Id.Equals(ItemId.Tear_of_the_Goddess) ||
                    item.Id.Equals(ItemId.Archangels_Staff) ||
                    item.Id.Equals(ItemId.Manamune) ||
                    item.Id.Equals(ItemId.Tear_of_the_Goddess_Crystal_Scar) ||
                    item.Id.Equals(ItemId.Archangels_Staff_Crystal_Scar) ||
                    item.Id.Equals(ItemId.Manamune_Crystal_Scar));
    }
}
