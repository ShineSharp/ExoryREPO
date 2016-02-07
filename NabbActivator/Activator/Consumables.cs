using LeagueSharp;
using LeagueSharp.Common;

namespace NabbActivator
{
    using System;
    using System.Linq;
    using ItemData = LeagueSharp.Common.Data.ItemData;
 
    /// <summary>
    /// The consumables class.
    /// </summary>
    class Consumables
    {
        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void Execute(EventArgs args)
        {
            if (ObjectManager.Player.IsRecalling() ||
                ObjectManager.Player.InFountain())
            {
                return;
            }

            /// <summary>
            /// The Health Potions Logic.
            /// </summary>
            if (!Bools.IsHealthPotRunning())
            {
                if (ObjectManager.Player.HealthPercent <= Managers.MinHealthPercent)
                {
                    /// <summary>
                    /// The Refillable Potion.
                    /// </summary>
                    if (ItemData.Refillable_Potion.GetItem().IsReady())
                    {
                        ItemData.Refillable_Potion.GetItem().Cast();
                        return;
                    }  
                    
                    /// <summary>
                    // The Health Potion.
                    /// </summary>
                    if (ItemData.Health_Potion.GetItem().IsReady())
                    {
                        ItemData.Health_Potion.GetItem().Cast();
                        return;
                    }

                    /// <summary>
                    ///  The Total Biscuit of Rejuvenation.
                    /// </summary>
                    if (ItemData.Total_Biscuit_of_Rejuvenation2.GetItem().IsReady())
                    {
                        ItemData.Total_Biscuit_of_Rejuvenation2.GetItem().Cast();
                        return;
                    }
                    
                    /// <summary>
                    /// The Corrupting Potion.
                    /// </summary>
                    if (ItemData.Corrupting_Potion.GetItem().IsReady())
                    {
                        ItemData.Corrupting_Potion.GetItem().Cast();
                        return;
                    }

                    /// <summary>
                    /// The Hunter's Potion.
                    /// </summary>
                    if (ItemData.Hunters_Potion.GetItem().IsReady())
                    {
                        ItemData.Hunters_Potion.GetItem().Cast();
                        return;
                    }
                }
            }

            /// <summary>
            /// The Mana Potions Logic.
            /// </summary>
            if (!Bools.IsManaPotRunning())
            {
                if (ObjectManager.Player.ManaPercent <= Managers.MinManaPercent &&
                    !Variables.NoManaChampions.Contains(ObjectManager.Player.ChampionName))
                {
                    /// <summary>
                    /// The Corrupting Potion.
                    /// </summary>
                    if (ItemData.Corrupting_Potion.GetItem().IsReady())
                    {
                        ItemData.Corrupting_Potion.GetItem().Cast();
                        return;
                    }

                    /// <summary>
                    /// The Hunter's Potion.
                    /// </summary>
                    if (ItemData.Hunters_Potion.GetItem().IsReady())
                    {
                        ItemData.Hunters_Potion.GetItem().Cast();
                        return;
                    }
                }
            }
        }
    }
}
