namespace NabbActivator
{
    using System;
    using System.Linq;
    using LeagueSharp;
    using LeagueSharp.Common;

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
                (Bools.ShouldUseCleanse(ObjectManager.Player) &&
                !Bools.IsSpellAvailable(SpellSlots.Cleanse)))
            {
                /// <summary>
                /// The Quicksilver Sash.
                /// </summary>
                if (ItemData.Quicksilver_Sash.GetItem().IsReady())
                {
                    Utility.DelayAction.Add(
                        Bools.MustRandomize() ?
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
                    foreach (var Ally in HeroManager.Allies
                        .Where(
                            h =>
                                h.IsValidTarget(750f, false) &&
                                Bools.ShouldUseCleanse(h) &&
                                Bools.HasNoProtection(h)))
                    {
                        Utility.DelayAction.Add(
                            Bools.MustRandomize() ?
                                WeightedRandom.Next(100, 200) :
                                0,
                            () =>
                            {
                                ItemData.Mikaels_Crucible.GetItem().Cast(Ally);
                            }
                        );
                        break;
                    }
                }
            }
        }
    }
}
