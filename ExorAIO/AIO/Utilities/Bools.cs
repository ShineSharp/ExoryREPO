using System;
using System.Linq;
using System.Collections.Generic;

using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Utilities
{
    /// <summary>
    /// The Bools class.
    /// </summary>
    class Bools
    {
        /// <summary>
        /// Returns true if the target has no protection.
        /// </summary>   
        public static bool HasNoProtection(Obj_AI_Base target)
        =>
            !target.IsInvulnerable &&
            !target.HasBuffOfType(BuffType.SpellShield);

        /// <summary>
        /// Defines if a determined champion can move or not.
        /// </summary>
        public static bool IsImmobile(Obj_AI_Hero Target)
        => 
            Target.HasBuffOfType(BuffType.Stun)
            || Target.HasBuffOfType(BuffType.Snare)
            || Target.HasBuffOfType(BuffType.Knockup)
            || Target.HasBuffOfType(BuffType.Charm)
            || Target.HasBuffOfType(BuffType.Flee)
            || Target.HasBuffOfType(BuffType.Taunt)
            || Target.HasBuffOfType(BuffType.Suppression);

        /// <summary>
        /// Defines whether a determined champion has 2 Vayne's W stacks or not.
        /// </summary>
        public static bool Has2WStacks(Obj_AI_Base target)
        =>
            target.HasBuff("vaynesilvereddebuff") &&
            target.GetBuffCount("vaynesilvereddebuff") == 2;

        /// <summary>
        /// Defines whether the Vayne player should stay faded or not.
        /// </summary>
        public static bool ShouldStayFaded()
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.miscsettings.noaastealth").GetValue<bool>() &&
            ObjectManager.Player.HasBuff("vayneinquisition") &&
            ObjectManager.Player.HasBuff("vaynetumblefade") &&
            ObjectManager.Player.CountEnemiesInRange(1000) > 2;

        /// <summary>
        /// Defines whether a determined champion has a stackable item.
        /// </summary>
        public static bool HasTear(Obj_AI_Hero target) 
        =>
            target.InventoryItems.Any(
                item => item.Id.Equals(ItemId.Tear_of_the_Goddess)
                    || item.Id.Equals(ItemId.Archangels_Staff)
                    || item.Id.Equals(ItemId.Manamune)
                    || item.Id.Equals(ItemId.Tear_of_the_Goddess_Crystal_Scar)
                    || item.Id.Equals(ItemId.Archangels_Staff_Crystal_Scar)
                    || item.Id.Equals(ItemId.Manamune_Crystal_Scar));

        /// <summary>
        /// Defines whether the Corki player has the Big One charged.
        /// </summary>
        public static bool HasFullPowerUltimate()
        =>
            ObjectManager.Player.HasBuff("corkimissilebarragecounterbig");

        /// <summary>
        /// Defines whether a determined game object has Tristana's E stack on it.
        /// </summary>
        public static bool IsCharged(Obj_AI_Base obj)
        =>
            obj.HasBuff("TristanaECharge");

        /// <summary>
        /// Defines whether a the Olaf player should use ult.
        /// </summary>
        public static bool ShouldCleanse()
        =>  
            ObjectManager.Player.HasBuffOfType(BuffType.Charm)
            || ObjectManager.Player.HasBuffOfType(BuffType.Flee)
            || ObjectManager.Player.HasBuffOfType(BuffType.Polymorph)
            || ObjectManager.Player.HasBuffOfType(BuffType.Knockback)
            || ObjectManager.Player.HasBuffOfType(BuffType.Knockup)
            || (ObjectManager.Player.HasBuffOfType(BuffType.Snare) && Bools.IsValidRoot())
            || ObjectManager.Player.HasBuffOfType(BuffType.Stun)
            || ObjectManager.Player.HasBuffOfType(BuffType.Taunt)
            || ObjectManager.Player.HasBuffOfType(BuffType.Suppression)
            || ObjectManager.Player.HasBuff("summonerexhaust");

        /// <summary>
        /// Defines whether a determined root is worth cleansing.
        /// </summary>
        public static bool IsValidRoot()
        =>
            ObjectManager.Player.Buffs
                .Any(
                    buff =>
                        buff.Type.Equals(BuffType.Snare) &&
                        !(((Obj_AI_Hero)buff.Caster).ChampionName.Equals("Leona") ||
                        ((Obj_AI_Hero)buff.Caster).ChampionName.Equals("Amumu")));
                        
        /// <summary>
        /// Defines whether The Jinx player is using Fishbones.
        /// </summary>
        public static bool IsUsingFishBones()
        =>
            ObjectManager.Player.HasBuff("JinxQ");

        /// <summary>
        /// Defines whether a the Nasus player should attack or wait for Q to execute an object.
        /// </summary>
        public static bool ShouldAttack(Obj_AI_Base target)
        =>
            target != null &&
            target.IsValid &&
            (ObjectManager.Player.HasBuff("NasusQ") ?
                Variables.Q.GetDamage(target) > target.Health :
                ObjectManager.Player.GetAutoAttackDamage(target) < target.Health);
    }
}
