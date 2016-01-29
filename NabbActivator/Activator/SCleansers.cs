using LeagueSharp;
using LeagueSharp.Common;

namespace NabbActivator
{
    using System;
    using ItemData = LeagueSharp.Common.Data.ItemData;
 
    /// <summary>
    /// The super-cleansers class.
    /// </summary>
    class SCleansers
    {
        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void Execute(EventArgs args)
        {
            if ((Bools.ShouldUseCleanser() ||
                (Bools.ShouldUseCleanse(ObjectManager.Player) &&
                    !Bools.IsSpellAvailable(SpellSlots.Cleanse))) ||
                HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(500 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/6)
            {
                /// <summary>
                /// The Dervish Blade.
                /// </summary>
                if (ItemData.Dervish_Blade.GetItem().IsReady())
                {
                    Utility.DelayAction.Add(
                        Variables.Menu.Item($"{Variables.MainMenuName}.randomizer").GetValue<bool>() ?
                            WeightedRandom.Next(100, 200) : 0,
                        () =>
                        {
                            ItemData.Dervish_Blade.GetItem().Cast();
                            return;
                        }
                    );
                }

                /// <summary>
                /// The Mercurial Scimitar.
                /// </summary>
                if (ItemData.Mercurial_Scimitar.GetItem().IsReady())
                {
                    Utility.DelayAction.Add(
                        Variables.Menu.Item($"{Variables.MainMenuName}.randomizer").GetValue<bool>() ?
                            WeightedRandom.Next(100, 200) : 0,
                        () =>
                        {
                            ItemData.Mercurial_Scimitar.GetItem().Cast();
                        }
                    );
                }
            }
        }
    }
}
