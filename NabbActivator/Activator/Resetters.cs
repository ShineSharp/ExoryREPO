using LeagueSharp;
using LeagueSharp.Common;

namespace NabbActivator
{
    using System;
    using ItemData = LeagueSharp.Common.Data.ItemData;
 
    /// <summary>
    /// The resetter items class.
    /// </summary>
    class Resetters
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
