using LeagueSharp;
using LeagueSharp.Common;

namespace NabbActivator
{
    using System;
    using ItemData = LeagueSharp.Common.Data.ItemData;
 
    /// <summary>
    /// The cleansers class.
    /// </summary>
    class Cleansers
    {
        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void Execute(EventArgs args)
        {
            if (Bools.ShouldUseCleanser() ||
                (!Bools.IsSpellAvailable(SpellSlots.Cleanse) &&
                    Bools.ShouldUseCleanse(ObjectManager.Player)))
            {
                /// <summary>
                /// The Quicksilver Sash.
                /// </summary>
                if (ItemData.Quicksilver_Sash.GetItem().IsReady())
                {
                    Utility.DelayAction.Add(
                        Variables.Menu.Item($"{Variables.MainMenuName}.randomizer").GetValue<bool>() ?
                            WeightedRandom.Next(100, 200) :
                            0,
                        () =>
                        {
                            ItemData.Quicksilver_Sash.GetItem().Cast();
                            return;
                        }
                    );
                }

                /// <summary>
                /// The Mikaels Crucible.
                /// </summary>
                if (ItemData.Mikaels_Crucible.GetItem().IsReady())
                {
                    if (Targets.Ally != null &&
                        Bools.ShouldUseCleanse(Targets.Ally))
                    {
                        Utility.DelayAction.Add(
                            Variables.Menu.Item($"{Variables.MainMenuName}.randomizer").GetValue<bool>() ?
                                WeightedRandom.Next(100, 200) : 0,
                            () =>
                            {
                                ItemData.Mikaels_Crucible.GetItem().Cast(Targets.Ally);
                            }
                        );
                    }
                    else if (Bools.ShouldUseCleanse(ObjectManager.Player))
                    {
                        Utility.DelayAction.Add(
                            Variables.Menu.Item($"{Variables.MainMenuName}.randomizer").GetValue<bool>() ?
                                WeightedRandom.Next(100, 200) : 0,
                            () =>
                            {
                                ItemData.Mikaels_Crucible.GetItem().Cast(ObjectManager.Player);
                            }
                        );
                    }
                }
            }
        }
    }
}
