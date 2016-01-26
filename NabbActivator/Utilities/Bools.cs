using LeagueSharp;
using LeagueSharp.Common;

namespace NabbActivator
{
    using System.Linq;

    /// <summary>
    /// The bools.
    /// </summary>
    class Bools
    {
        /// <summary>
        /// Defines whether a Health Potion is running.
        /// </summary>
        public static bool IsHealthPotRunning()
        =>
            ObjectManager.Player.HasBuff("ItemCrystalFlask") ||
            ObjectManager.Player.HasBuff("RegenerationPotion") ||
            ObjectManager.Player.HasBuff("ItemMiniRegenPotion") ||
            ObjectManager.Player.HasBuff("ItemDarkCrystalFlask") ||
            ObjectManager.Player.HasBuff("ItemCrystalFlaskJungle");

        /// <summary>
        /// Defines whether a Mana Potion is running.
        /// </summary>
        public static bool IsManaPotRunning()
        =>
            ObjectManager.Player.HasBuff("ItemDarkCrystalFlask") ||
            ObjectManager.Player.HasBuff("ItemCrystalFlaskJungle");

        /// <summary>
        /// Defines whether a target has no protection.
        /// </summary>
        public static bool HasNoProtection(Obj_AI_Hero target)
        =>
            !target.IsInvulnerable &&
            !target.HasBuffOfType(BuffType.SpellShield) &&
            target.Type.Equals(GameObjectType.obj_AI_Hero);

        /// <summary>
        /// Defines whether the player should use cleanse.
        /// </summary>
        public static bool ShouldUseCleanse(Obj_AI_Hero target)
        =>
            Bools.IsValidSnare() ||
            target.HasBuff("summonerdot") ||
            target.HasBuff("summonerexhaust") ||
            Bools.HasNoProtection(ObjectManager.Player) &&
            (
                target.HasBuffOfType(BuffType.Flee) ||
                target.HasBuffOfType(BuffType.Stun) ||
                target.HasBuffOfType(BuffType.Charm) ||
                target.HasBuffOfType(BuffType.Taunt) ||
                target.HasBuffOfType(BuffType.Polymorph)
            );

        /// <summary>
        /// Defines whether the player should use a cleanser.
        /// </summary>
        public static bool ShouldUseCleanser()
        =>
            Bools.HasNoProtection(ObjectManager.Player) &&
            ObjectManager.Player.HasBuffOfType(BuffType.Suppression) &&
            (
                ObjectManager.Player.HasBuff("FizzMarinerDoom") ||
                ObjectManager.Player.HasBuff("zedulttargetmark") ||
                ObjectManager.Player.HasBuff("VladimirHemoplague") ||
                ObjectManager.Player.HasBuff("PoppyDiplomaticImmunity") ||
                ObjectManager.Player.HasBuff("MordekaiserChildrenOfTheGrave")
            );

        /// <summary>
        /// Defines whether the casted root is worth cleansing.
        /// </summary>
        public static bool IsValidSnare()
        =>
            ObjectManager.Player.Buffs
                .Any(buff =>buff.Type.Equals(BuffType.Snare) &&
                    !((Obj_AI_Hero)buff.Caster).ChampionName.Equals("Leona") &&
                    !((Obj_AI_Hero)buff.Caster).ChampionName.Equals("Amumu"));

        /// <summary>
        /// Defines whether the arg spell is available and ready.
        /// </summary>
        public static bool IsSpellAvailable(SpellSlot arg)
        =>
            arg != SpellSlot.Unknown &&
            ObjectManager.Player.Spellbook.CanUseSpell(arg) == SpellState.Ready;
    }
}
