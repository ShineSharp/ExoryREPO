namespace NabbActivator
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    /// <summary>
    /// The bools.
    /// </summary>
    public class Bools
    {
        /// <summary>
        /// Defines whether a Health Potion is running.
        /// </summary>
        public static bool IsHealthPotRunning()
        =>
            ObjectManager.Player.HasBuff("ItemMiniRegenPotion") ||
            ObjectManager.Player.HasBuff("ItemCrystalFlask") ||
            ObjectManager.Player.HasBuff("ItemDarkCrystalFlask") ||
            ObjectManager.Player.HasBuff("ItemCrystalFlaskJungle") ||
            ObjectManager.Player.HasBuff("RegenerationPotion");

        /// <summary>
        /// Defines whether a Mana Potion is running.
        /// </summary>
        public static bool IsManaPotRunning()
        =>
            ObjectManager.Player.HasBuff("ItemCrystalFlaskJungle") ||
            ObjectManager.Player.HasBuff("ItemDarkCrystalFlask");

        /// <summary>
        /// Defines whether a target has no protection.
        /// </summary>
        public static bool HasNoProtection(Obj_AI_Hero target)
        =>
            !target.IsInvulnerable &&
            !target.HasBuffOfType(BuffType.SpellShield);

        /// <summary>
        /// Defines whether the player should use cleanse.
        /// </summary>
        public static bool ShouldUseCleanse(Obj_AI_Hero target)
        =>  
            Bools.HasNoProtection(ObjectManager.Player) &&
            (target.HasBuffOfType(BuffType.Charm) ||
            target.HasBuffOfType(BuffType.Flee) ||
            target.HasBuffOfType(BuffType.Polymorph) ||
            (target.HasBuffOfType(BuffType.Snare) && Bools.IsValidRoot()) ||
            target.HasBuffOfType(BuffType.Stun) ||
            target.HasBuffOfType(BuffType.Taunt) ||
            target.HasBuff("summonerexhaust") ||
            target.HasBuff("summonerdot"));

        /// <summary>
        /// Defines whether the player should use a cleanser.
        /// </summary>
        public static bool ShouldUseCleanser()
        =>
            Bools.HasNoProtection(ObjectManager.Player) &&
            (ObjectManager.Player.HasBuff("zedulttargetmark") ||
            ObjectManager.Player.HasBuff("VladimirHemoplague") ||
            ObjectManager.Player.HasBuff("MordekaiserChildrenOfTheGrave") ||
            ObjectManager.Player.HasBuff("PoppyDiplomaticImmunity") ||
            ObjectManager.Player.HasBuff("FizzMarinerDoom") ||
            ObjectManager.Player.HasBuffOfType(BuffType.Suppression));

        /// <summary>
        /// Defines whether the casted root is worth cleansing.
        /// </summary>
        public static bool IsValidRoot()
        =>
            ObjectManager.Player.Buffs
                .Any(
                    buff =>
                        buff.Type.Equals(BuffType.Snare) &&
                        !((Obj_AI_Hero)buff.Caster).ChampionName.Equals("Leona") &&
                        !((Obj_AI_Hero)buff.Caster).ChampionName.Equals("Amumu"));

        /// <summary>
        /// Defines whether the arg spell is available and ready.
        /// </summary>
        public static bool IsSpellAvailable(SpellSlot arg)
        =>
            arg != SpellSlot.Unknown &&
            ObjectManager.Player.Spellbook.CanUseSpell(arg) == SpellState.Ready;

        /// <summary>
        /// Defines whether it has to randomize actions.
        /// </summary>
        public static bool MustRandomize()
        => 
            Variables.Menu.Item($"{Variables.MainMenuName}.randomizer").GetValue<bool>();
    }
}
