namespace NabbActivator
{
    using System;
    using LeagueSharp;
    using LeagueSharp.Common;
    using ItemData = LeagueSharp.Common.Data.ItemData;

    /// <summary>
    /// The defensive items class.
    /// </summary>
    class Defensives
    {
        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void Execute(EventArgs args)
        {
            /// <summary>
            /// The Face of the Mountain
            /// </summary>
            if (ItemData.Face_of_the_Mountain.GetItem().IsReady())
            {
                if (Targets.Ally != null &&
                    HealthPrediction.GetHealthPrediction(Targets.Ally, (int)(250 + Game.Ping / 2f)) <= Targets.Ally.MaxHealth/4)
                {
                    ItemData.Face_of_the_Mountain.GetItem().Cast(Targets.Ally);
                }
                else if (HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/4)
                {
                    ItemData.Face_of_the_Mountain.GetItem().Cast(ObjectManager.Player);
                }
                return;
            }

            /// <summary>
            /// The Locket of the Iron Solari.
            /// </summary>             
            if (ItemData.Locket_of_the_Iron_Solari.GetItem().IsReady() &&
                (Targets.Ally != null &&
                    HealthPrediction.GetHealthPrediction(Targets.Ally, (int)(250 + Game.Ping / 2f)) <= Targets.Ally.MaxHealth/1.5) ||
                HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/1.5)
            {
                ItemData.Locket_of_the_Iron_Solari.GetItem().Cast();
                return;
            }

            /// <summary>
            /// The Seraph's Embrace.
            /// </summary>    
            if (ItemData.Seraphs_Embrace.GetItem().IsReady() &&
                HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/4)
            {
                ItemData.Seraphs_Embrace.GetItem().Cast();
                return;
            }

            /// <summary>
            /// The Zhonya's Hourglass.
            /// </summary>             
            if (ItemData.Zhonyas_Hourglass.GetItem().IsReady() &&
                HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/4)
            {
                ItemData.Zhonyas_Hourglass.GetItem().Cast();
                return;
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
            /// The Frost Queen's Claim.
            /// </summary>
            if (ItemData.Frost_Queens_Claim.GetItem().IsReady() &&
                (Targets.Target.CountEnemiesInRange(1000f) < ObjectManager.Player.CountAlliesInRange(600f) ||
                Targets.Target.CountEnemiesInRange(1000f) > ObjectManager.Player.CountAlliesInRange(600f)))
            {
                ItemData.Frost_Queens_Claim.GetItem().Cast();
            }

            /// <summary>
            /// The Guardian's Horn.
            /// </summary>
            if (ItemData.Guardians_Horn.GetItem().IsReady() &&
                (Targets.Target.CountEnemiesInRange(1000f) < ObjectManager.Player.CountAlliesInRange(600f) ||
                Targets.Target.CountEnemiesInRange(1000f) > ObjectManager.Player.CountAlliesInRange(600f)))
            {
                ItemData.Guardians_Horn.GetItem().Cast();
            }

            /// <summary>
            /// The Banner of Command.
            /// </summary> 
            if (ItemData.Banner_of_Command.GetItem().IsReady() &&
                Targets.Minion != null)
            {
                ItemData.Banner_of_Command.GetItem().Cast(Targets.Minion);
            }

            /// <summary>
            /// The Talisman of Ascension.
            /// </summary> 
            if (ItemData.Talisman_of_Ascension.GetItem().IsReady() &&
                (Targets.Target.CountEnemiesInRange(1000f) < ObjectManager.Player.CountAlliesInRange(600f) ||
                Targets.Target.CountEnemiesInRange(1000f) > ObjectManager.Player.CountAlliesInRange(600f)))
            {
                ItemData.Talisman_of_Ascension.GetItem().Cast();
            }

            /// <summary>
            /// The Randuin's Omen.
            /// </summary>             
            if (ItemData.Randuins_Omen.GetItem().IsReady() &&
                ObjectManager.Player.CountEnemiesInRange(500f) >= 2)
            {
                ItemData.Randuins_Omen.GetItem().Cast();
            }

            /// <summary>
            /// The Righteous Glory.
            /// </summary>             
            if (ItemData.Righteous_Glory.GetItem().IsReady())
            {
                if ((!ObjectManager.Player.HasBuff("ItemRighteousGlory") &&
                    (Targets.Target.CountEnemiesInRange(1000f) < ObjectManager.Player.CountAlliesInRange(600f) ||
                    Targets.Target.CountEnemiesInRange(1000f) > ObjectManager.Player.CountAlliesInRange(600f))) ||

                    (ObjectManager.Player.HasBuff("ItemRighteousGlory") &&
                    ObjectManager.Player.CountEnemiesInRange(450f) >= 2))
                {
                    ItemData.Righteous_Glory.GetItem().Cast();
                }
            }
        }
    }

    /// <summary>
    /// The ohmwrecker class.
    /// </summary>
    public class Ohmwrecker
    {
        /// <summary>
        /// Called when a cast has been executed.
        /// </summary>
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
