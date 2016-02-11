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
        /// Defines whether the casted root is worth cleansing.
        /// </summary>
        public static bool IsValidSnare()
        =>
            ObjectManager.Player.Buffs
                .Any(buff => buff.Type.Equals(BuffType.Snare) &&
                    !((Obj_AI_Hero)buff.Caster).ChampionName.Equals("Leona"));

        /// <summary>
        /// Defines whether the casted stun is worth cleansing.
        /// </summary>
        public static bool IsValidStun()
        =>
            ObjectManager.Player.Buffs
                .Any(buff => buff.Type.Equals(BuffType.Stun) &&
                    !((Obj_AI_Hero)buff.Caster).ChampionName.Equals("Amumu") &&
                    !((Obj_AI_Hero)buff.Caster).ChampionName.Equals("Alistar") &&
                    !((Obj_AI_Hero)buff.Caster).ChampionName.Equals("Blitzcrank"));

        /// <summary>
        /// Defines whether the player should use cleanse.
        /// </summary>
        public static bool ShouldUseCleanse(Obj_AI_Hero target)
        =>
            !ObjectManager.Player.IsInvulnerable &&
            !IsSpellShielded(ObjectManager.Player) &&
            (
                Bools.IsValidStun() ||
                Bools.IsValidSnare() ||
                target.HasBuff("summonerdot") ||
                target.HasBuff("summonerexhaust") ||
                target.HasBuffOfType(BuffType.Flee) ||
                target.HasBuffOfType(BuffType.Charm) ||
                target.HasBuffOfType(BuffType.Taunt) ||
                target.HasBuffOfType(BuffType.Polymorph)
            );

        /// <summary>
        /// Defines whether the player should use a cleanser.
        /// </summary>
        public static bool ShouldUseCleanser()
        =>
            !ObjectManager.Player.IsInvulnerable &&
            !IsSpellShielded(ObjectManager.Player) &&
            (
                ObjectManager.Player.HasBuffOfType(BuffType.Suppression) ||
                ObjectManager.Player.HasBuff("FizzMarinerDoom") ||
                ObjectManager.Player.HasBuff("zedulttargetmark") ||
                ObjectManager.Player.HasBuff("VladimirHemoplague") ||
                ObjectManager.Player.HasBuff("MordekaiserChildrenOfTheGrave")
            );

        /// <summary>
        /// Defines whether the arg spell is available and ready.
        /// </summary>
        public static bool IsSpellAvailable(SpellSlot arg)
        =>
            arg != SpellSlot.Unknown &&
            ObjectManager.Player.Spellbook.CanUseSpell(arg) == SpellState.Ready;
    }
}
