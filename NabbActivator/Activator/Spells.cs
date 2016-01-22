namespace NabbActivator
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;
    
    using ItemData = LeagueSharp.Common.Data.ItemData;
 
    /// <summary>
    /// The spells class.
    /// </summary>
    public class Spells
    {
        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void Execute(EventArgs args)
        {
            /// <summary>
            /// The Cleanse Logic.
            /// </summary>    
            if (Bools.HasNoProtection(ObjectManager.Player) &&
                Bools.ShouldUseCleanse(ObjectManager.Player))
            {
                if (Variables.W.IsReady() &&
                    ObjectManager.Player.ChampionName.Equals("Gangplank"))
                {
                    Variables.W.Cast();
                    return;
                }

                if (Bools.IsSpellAvailable(SpellSlots.Cleanse))
                {
                    Utility.DelayAction.Add(
                        Bools.MustRandomize() ?
                            WeightedRandom.Next(100, 200) :
                            0,
                        () => 
                        {
                            ObjectManager.Player.Spellbook.CastSpell(SpellSlots.Cleanse);
                        }
                    );
                }
            }

            /// <summary>
            /// The Barrier Logic.
            /// </summary>
            if (Bools.IsSpellAvailable(SpellSlots.Barrier) &&
                HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/6)
            {
                ObjectManager.Player.Spellbook.CastSpell(SpellSlots.Barrier);
                return;
            }

            /// <summary>
            /// The Exhaust Logic.
            /// </summary>
            if (Bools.IsSpellAvailable(SpellSlots.Exhaust) &&
                Targets.Target.IsValidTarget(650f) &&
                (ObjectManager.Player.CountEnemiesInRange(850f) > 0 || Targets.Ally.CountEnemiesInRange(850f) > 0) &&
                (HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/6 ||
                    HealthPrediction.GetHealthPrediction(Targets.Ally, (int)(250 + Game.Ping / 2f)) <= Targets.Ally.MaxHealth/6))
            {
                ObjectManager.Player.Spellbook.CastSpell(SpellSlots.Exhaust, Targets.Target);
            }

            /// <summary>
            /// The Heal Logic.
            /// </summary>
            if (Bools.IsSpellAvailable(SpellSlots.Heal) &&
                (ObjectManager.Player.CountEnemiesInRange(850f) > 0 || Targets.Ally.CountEnemiesInRange(850f) > 0) &&
                (HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/6 ||
                    HealthPrediction.GetHealthPrediction(Targets.Ally, (int)(250 + Game.Ping / 2f)) <= Targets.Ally.MaxHealth/6))
            {
                ObjectManager.Player.Spellbook.CastSpell(SpellSlots.Heal);
                return;
            }

            /// <summary>
            /// The Ignite Logic.
            /// </summary>
            if (Bools.IsSpellAvailable(SpellSlots.Ignite) &&
                Targets.Target != null &&
                Targets.Target.IsValidTarget(600f) &&
                ObjectManager.Player.GetSummonerSpellDamage(Targets.Target, Damage.SummonerSpell.Ignite) > Targets.Target.Health)
            {
                ObjectManager.Player.Spellbook.CastSpell(SpellSlots.Ignite, Targets.Target);
            }

            /// <summary>
            /// The Clarity logic.
            /// </summary>
            if (Bools.IsSpellAvailable(SpellSlots.Clarity) &&
                ObjectManager.Player.ManaPercent <= 20)
            {
                ObjectManager.Player.Spellbook.CastSpell(SpellSlots.Clarity);
            }
        }
    }
}
