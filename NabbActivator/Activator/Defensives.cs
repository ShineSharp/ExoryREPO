namespace NabbActivator
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;
    
    using ItemData = LeagueSharp.Common.Data.ItemData;
 
    /// <summary>
    /// The defensive items class.
    /// </summary>
    public class Defensives
    {
        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void Execute(EventArgs args)
        {
            /// <summary>
            /// The Frost Queen's Claim.
            /// </summary>
            if (ItemData.Frost_Queens_Claim.GetItem().IsReady() &&
                Targets.Target != null &&
                (Targets.Target.CountEnemiesInRange(1000f) < ObjectManager.Player.CountAlliesInRange(600f) ||
                Targets.Target.CountEnemiesInRange(1000f) > ObjectManager.Player.CountAlliesInRange(600f)))
            {
                ItemData.Frost_Queens_Claim.GetItem().Cast();
            }

            /// <summary>
            /// The Guardian's Horn.
            /// </summary>
            if (ItemData.Guardians_Horn.GetItem().IsReady() &&
                Targets.Target != null &&
                (Targets.Target.CountEnemiesInRange(1000f) < ObjectManager.Player.CountAlliesInRange(600f) ||
                Targets.Target.CountEnemiesInRange(1000f) > ObjectManager.Player.CountAlliesInRange(600f)))
            {
                ItemData.Guardians_Horn.GetItem().Cast();
            }

            /// <summary>
            /// The Seraph's Embrace.
            /// </summary>    
            if (ItemData.Seraphs_Embrace.GetItem().IsReady() &&
                HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/4)
            {
                ItemData.Seraphs_Embrace.GetItem().Cast();
            }

            /// <summary>
            /// The Banner of Command.
            /// </summary> 
            if (ItemData.Banner_of_Command.GetItem().IsReady() &&
                Targets.banTarget != null)
            {
                ItemData.Banner_of_Command.GetItem().Cast(Targets.banTarget);
            }

            /// <summary>
            /// The Talisman of Ascension.
            /// </summary> 
            if (ItemData.Talisman_of_Ascension.GetItem().IsReady() &&
                Targets.Target != null &&
                (Targets.Target.CountEnemiesInRange(1000f) < ObjectManager.Player.CountAlliesInRange(600f) ||
                Targets.Target.CountEnemiesInRange(1000f) > ObjectManager.Player.CountAlliesInRange(600f)))
            {
                ItemData.Talisman_of_Ascension.GetItem().Cast();
            }

            /// <summary>
            /// The Wooglet's Witchcap.
            /// </summary>             
            if (ItemData.Wooglets_Witchcap.GetItem().IsReady() &&
                HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/4)
            {
                ItemData.Wooglets_Witchcap.GetItem().Cast();
            }

            /// <summary>
            /// The Randuin's Omen.
            /// </summary>             
            if (ItemData.Randuins_Omen.GetItem().IsReady() &&
                ObjectManager.Player.CountEnemiesInRange(500f) > 1)
            {
                ItemData.Randuins_Omen.GetItem().Cast();
            }

            /// <summary>
            /// The Zhonya's Hourglass.
            /// </summary>             
            if (ItemData.Zhonyas_Hourglass.GetItem().IsReady() &&
                HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/4)
            {
                ItemData.Zhonyas_Hourglass.GetItem().Cast();
            }

            /// <summary>
            /// The Locket of the Iron Solari.
            /// </summary>             
            if (ItemData.Locket_of_the_Iron_Solari.GetItem().IsReady() &&
                Targets.Target != null &&
                (Targets.Target.CountEnemiesInRange(1000f) < ObjectManager.Player.CountAlliesInRange(600f) ||
                Targets.Target.CountEnemiesInRange(1000f) > ObjectManager.Player.CountAlliesInRange(600f)))
            {
                ItemData.Locket_of_the_Iron_Solari.GetItem().Cast();
            }

            /// <summary>
            /// The Righteous Glory.
            /// </summary>             
            if (ItemData.Righteous_Glory.GetItem().IsReady() &&
                Targets.Target != null &&
                (Targets.Target.CountEnemiesInRange(1000f) < ObjectManager.Player.CountAlliesInRange(600f) ||
                Targets.Target.CountEnemiesInRange(1000f) > ObjectManager.Player.CountAlliesInRange(600f)))
            {
                ItemData.Righteous_Glory.GetItem().Cast();
            }

            /// <summary>
            /// The Face of the Mountain
            /// </summary>
            if (ItemData.Face_of_the_Mountain.GetItem().IsReady())
            {
                foreach (var ally in HeroManager.Allies
                    .Where(
                        h =>
                            h.IsValidTarget(750f, false) &&
                            HealthPrediction.GetHealthPrediction(h, (int)(250 + Game.Ping / 2f)) <= h.MaxHealth/4 &&
                            Bools.HasNoProtection(h)))
                {
                    ItemData.Face_of_the_Mountain.GetItem().Cast(ally);
                }
            }
        }
    }

    /// <summary>
    /// The ohmwrecker class.
    /// </summary>
    public class Ohmwrecker
    {
        public static void Execute(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// The Ohmwrecker.
            /// </summary>        
            if (ItemData.Ohmwrecker.GetItem().IsReady())
            {
                ItemData.Ohmwrecker.GetItem().Cast((Obj_AI_Turret)args.Target);
            }
        }
    }
}
