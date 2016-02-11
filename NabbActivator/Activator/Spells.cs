using LeagueSharp;
using LeagueSharp.Common;

namespace NabbActivator
{
    using System;
    using ItemData = LeagueSharp.Common.Data.ItemData;
 
    /// <summary>
    /// The spells class.
    /// </summary>
    class Spells
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
            if (Bools.ShouldUseCleanse(ObjectManager.Player))
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
                        Variables.Menu.Item($"{Variables.MainMenuName}.randomizer").GetValue<bool>() ?
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
            /// The Heal Logic.
            /// </summary>
            if (Bools.IsSpellAvailable(SpellSlots.Heal) &&
                !ItemData.Face_of_the_Mountain.GetItem().IsReady() &&
                !ItemData.Locket_of_the_Iron_Solari.GetItem().IsReady())
            {
                if ((ObjectManager.Player.CountEnemiesInRange(850f) > 0 &&
                        HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/6) ||

                    (Targets.Ally.IsValidTarget(850f) &&
                        Targets.Ally.CountEnemiesInRange(850f) > 0 &&
                        HealthPrediction.GetHealthPrediction(Targets.Ally, (int)(250 + Game.Ping / 2f)) <= Targets.Ally.MaxHealth/6))
                {
                    ObjectManager.Player.Spellbook.CastSpell(SpellSlots.Heal);
                    return;
                }
            }

            /// <summary>
            /// The Exhaust Logic.
            /// </summary>
            if (Bools.IsSpellAvailable(SpellSlots.Exhaust) &&
                Targets.Target.IsValidTarget(650f) &&
                (ObjectManager.Player.CountEnemiesInRange(850f) > 0 || Targets.Ally.CountEnemiesInRange(850f) > 0) &&
                (HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/4 ||
                    HealthPrediction.GetHealthPrediction(Targets.Ally, (int)(250 + Game.Ping / 2f)) <= Targets.Ally.MaxHealth/4))
            {
                ObjectManager.Player.Spellbook.CastSpell(SpellSlots.Exhaust, Targets.Target);
            }

            /// <summary>
            /// The Ignite Logic.
            /// </summary>
            if (Bools.IsSpellAvailable(SpellSlots.Ignite) &&
                Targets.Target.IsValidTarget(600f) &&
                (ObjectManager.Player.GetSummonerSpellDamage(Targets.Target, Damage.SummonerSpell.Ignite) > Targets.Target.Health ||
                    HealthPrediction.GetHealthPrediction(Targets.Target, (int)(750 + Game.Ping / 2f)) <= 0))
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
