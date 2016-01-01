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
        /// Gets a value indicating whether the target has protection or not.
        /// </summary>
        /// <value>
        /// <c>true</c> if the has no protection.; otherwise, <c>false</c>.
        /// </value>        
        public static bool HasNoProtection(Obj_AI_Base target)
        =>
            !target.IsInvulnerable &&
            !target.HasBuffOfType(BuffType.SpellShield);

        /// <summary>
        /// Gets a value indicating whether a determined champion can move or not.
        /// </summary>
        /// <value>
        /// <c>true</c> if the target can't move.; otherwise, <c>false</c>.
        /// </value>
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
        /// Gets a value indicating whether a determined champion has 2 Vayne's W stacks or not.
        /// </summary>
        /// <value>
        /// <c>true</c> if the target has 2 silver bolt stacks.; otherwise, <c>false</c>.
        /// </value>
        public static bool Has2WStacks(Obj_AI_Base target)
        =>
            target.HasBuff("vaynesilvereddebuff") &&
            target.GetBuffCount("vaynesilvereddebuff") == 2;

        /// <summary>
        /// Gets a value indicating whether the Vayne player should stay faded or not.
        /// </summary>
        /// <value>
        /// <c>true</c> if the player should stay faded.; otherwise, <c>false</c>.
        /// </value>
        public static bool ShouldStayFaded()
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.miscsettings.noaastealth").GetValue<bool>() &&
            ObjectManager.Player.HasBuff("vayneinquisition") &&
            ObjectManager.Player.HasBuff("vaynetumblefade") &&
            ObjectManager.Player.CountEnemiesInRange(1000) > 2;

        /// <summary>
        /// Gets a value indicating whether a determined champion has a stackable item.
        /// </summary>
        /// <value>
        /// <c>true</c> if the player has a tear item.; otherwise, <c>false</c>.
        /// </value>
        public static bool HasTear(Obj_AI_Hero target) 
        =>
            target.InventoryItems.Any(
                item => 
                    item.Id.Equals(ItemId.Tear_of_the_Goddess)
                    || item.Id.Equals(ItemId.Archangels_Staff)
                    || item.Id.Equals(ItemId.Manamune)
                    || item.Id.Equals(ItemId.Tear_of_the_Goddess_Crystal_Scar)
                    || item.Id.Equals(ItemId.Archangels_Staff_Crystal_Scar)
                    || item.Id.Equals(ItemId.Manamune_Crystal_Scar));

        /// <summary>
        /// Gets a value indicating whether the Corki has the Big One charged.
        /// </summary>
        /// <value>
        /// <c>true</c> if the Corki has the Big One charged.; otherwise, <c>false</c>.
        /// </value>
        public static bool HasFullPowerUltimate()
        =>
            ObjectManager.Player.HasBuff("corkimissilebarragecounterbig");

        /// <summary>
        /// Gets a value indicating whether a determined game object has Tristana's E stack on it.
        /// </summary>
        /// <value>
        /// <c>true</c> if the the target has a Tristana charge.; otherwise, <c>false</c>.
        /// </value>
        public static bool IsCharged(Obj_AI_Base obj)
        =>
            obj.HasBuff("TristanaECharge");

        /// <summary>
        /// Gets a value indicating whether a the Olaf player should use ult.
        /// </summary>
        /// <value>
        /// <c>true</c> if the cleanse should be used.; otherwise, <c>false</c>.
        /// </value>
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
        /// Gets a value indicating whether a determined root is worth cleansing.
        /// </summary>
        /// <value>
        /// <c>false</c> if the sender is Leona or Amumu.; otherwise, <c>true</c>.
        /// </value>
        public static bool IsValidRoot()
        =>
            ObjectManager.Player.Buffs
                .Any(
                    buff =>
                        buff.Type.Equals(BuffType.Snare) &&
                        !(((Obj_AI_Hero)buff.Caster).ChampionName.Equals("Leona") ||
                        ((Obj_AI_Hero)buff.Caster).ChampionName.Equals("Amumu")));
                        
        /// <summary>
        /// Gets a value indicating whether the Jinx is using Fishbones.
        /// </summary>
        /// <value>
        /// <c>true</c> if the Q is activated.; otherwise, <c>false</c>.
        /// </value>
        public static bool IsUsingFishBones()
        =>
            ObjectManager.Player.HasBuff("JinxQ");

        /// <summary>
        /// Gets a value indicating whether the e is activated.
        /// </summary>
        /// <value>
        /// <c>true</c> if the E is currently being toggled.; otherwise, <c>false</c>.
        /// </value>
        public static bool CanUseE()
        =>
            ObjectManager.Player.Spellbook.GetSpell(SpellSlot.E).ToggleState == 1 || Variables.EGameObject != null;
    }
}
