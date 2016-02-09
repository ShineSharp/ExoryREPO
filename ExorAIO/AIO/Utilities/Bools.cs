using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Utilities
{
    using System.Linq;

    /// <summary>
    /// The Bools class.
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

        /// <summary>
        /// Gets a value indicating whether a determined root is worth cleansing.
        /// </summary>
        /// <value>
        /// <c>true</c> if it is a valid snare.; otherwise, <c>false</c>.
        /// </value>
        public static bool IsValidSnare()
        =>
            ObjectManager.Player.Buffs
                .Any(buff => buff.Type.Equals(BuffType.Snare) &&
                    !((Obj_AI_Hero)buff.Caster).ChampionName.Equals("Leona") &&
                    !((Obj_AI_Hero)buff.Caster).ChampionName.Equals("Amumu"));

        /// <summary>
        /// Gets a value indicating whether a determined Stun is worth cleansing.
        /// </summary>
        /// <value>
        /// <c>true</c> if it is a valid stun.; otherwise, <c>false</c>.
        /// </value>
        public static bool IsValidStun()
        =>
            ObjectManager.Player.Buffs
                .Any(buff => buff.Type.Equals(BuffType.Stun) &&
                    !((Obj_AI_Hero)buff.Caster).ChampionName.Equals("Alistar"));

        /// <summary>
        /// Gets a value indicating whether BuffType is worth cleansing.
        /// </summary>
        /// <value>
        /// <c>true</c> if the cleanse should be used.; otherwise, <c>false</c>.
        /// </value>
        public static bool ShouldCleanse(Obj_AI_Hero target)
        =>
            Bools.IsValidStun() ||
            Bools.IsValidSnare() ||
            target.HasBuff("summonerexhaust") ||
            (!target.IsInvulnerable && !IsSpellShielded(target)) &&
            (
                target.HasBuffOfType(BuffType.Flee) ||
                target.HasBuffOfType(BuffType.Stun) ||
                target.HasBuffOfType(BuffType.Charm) ||
                target.HasBuffOfType(BuffType.Taunt) ||
                target.HasBuffOfType(BuffType.Polymorph)
            );
    }
}
