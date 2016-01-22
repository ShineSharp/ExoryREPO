namespace NabbActivator
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;
    
    using ItemData = LeagueSharp.Common.Data.ItemData;

    /// <summary>
    /// The offensive items class.
    /// </summary>
    public class Offensives
    {
        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void Execute(EventArgs args)
        {
            /// <summary>
            /// The Muramana.
            /// </summary>
            if (ItemData.Muramana.GetItem().IsReady() &&
                ((!ObjectManager.Player.HasBuff("muramana") && Variables.Menu.Item($"{Variables.MainMenuName}.combo_button").GetValue<KeyBind>().Active) ||
                (ObjectManager.Player.HasBuff("muramana") && !Variables.Menu.Item($"{Variables.MainMenuName}.combo_button").GetValue<KeyBind>().Active)))
            {
                ItemData.Muramana.GetItem().Cast();
            }

            /// <summary>
            /// The Youmuu's Ghostblade.
            /// </summary>
            if (ItemData.Youmuus_Ghostblade.GetItem().IsReady() &&
                ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)))
            {
                ItemData.Youmuus_Ghostblade.GetItem().Cast();
            }

            /// <summary>
            /// The Bilgewater Cutlass.
            /// </summary>
            if (ItemData.Bilgewater_Cutlass.GetItem().IsReady() &&
                Targets.Target.IsValidTarget(550f))
            {
                ItemData.Bilgewater_Cutlass.GetItem().Cast(Targets.Target);
            }

            /// <summary>
            /// The Hextech Gunblade.
            /// </summary>
            if (ItemData.Hextech_Gunblade.GetItem().IsReady() &&
                Targets.Target.IsValidTarget(550f))
            {
                ItemData.Hextech_Gunblade.GetItem().Cast(Targets.Target);
            }

            /// <summary>
            /// The Blade of the Ruined King.
            /// </summary>    
            if (ItemData.Blade_of_the_Ruined_King.GetItem().IsReady() &&
                Targets.Target.IsValidTarget(550f) &&
                ObjectManager.Player.HealthPercent <= 90)
            {
                ItemData.Blade_of_the_Ruined_King.GetItem().Cast(Targets.Target);
            }

            /// <summary>
            /// The Entropy.
            /// </summary>     
            if (ItemData.Entropy.GetItem().IsReady() &&
                ObjectManager.Player.IsWindingUp)
            {            
                ItemData.Entropy.GetItem().Cast();
            }

            /// <summary>
            /// The Odyn's Veil.
            /// </summary>  
            if (ItemData.Odyns_Veil.GetItem().IsReady() &&
                HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth - 300)
            {
                ItemData.Odyns_Veil.GetItem().Cast();
            }
        }
    }
    
    /// <summary>
    /// The resetter items class.
    /// </summary>
    public class Resetters
    {
        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void Execute(EventArgs args)
        {
            /// <summary>
            /// The Tiamat.
            /// </summary>
            if (ItemData.Tiamat_Melee_Only.GetItem().IsReady())
            {
                ItemData.Tiamat_Melee_Only.GetItem().Cast();
                return;
            }

            /// <summary>
            /// The Ravenous Hydra.
            /// </summary>
            if (ItemData.Ravenous_Hydra_Melee_Only.GetItem().IsReady())
            {
                ItemData.Ravenous_Hydra_Melee_Only.GetItem().Cast();
                return;
            }

            /// <summary>
            /// The Titanic Hydra.
            /// </summary>
            if (ItemData.Titanic_Hydra_Melee_Only.GetItem().IsReady())
            {
                ItemData.Titanic_Hydra_Melee_Only.GetItem().Cast();
            }
        }
    }
}
