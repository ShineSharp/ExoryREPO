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
            /// The Clarity logic.
            /// </summary>
            if (Bools.IsSpellAvailable(SpellSlots.Clarity) &&
                ObjectManager.Player.ManaPercent <= 20)
            {
                ObjectManager.Player.Spellbook.CastSpell(SpellSlots.Clarity);
            }

            /// <summary>
            /// The Ghost Logic.
            /// </summary>
            /*
            if (Bools.IsSpellAvailable(SpellSlots.Ghost) &&
                Targets.Target.Distance(ObjectManager.Player) > Orbwalking.GetRealAutoAttackRange(Targets.Target) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.combo_button").GetValue<KeyBind>().Active)
            {
                ObjectManager.Player.Spellbook.CastSpell(SpellSlots.Ghost);
            }
            */

            /// <summary>
            /// The Ignite Logic.
            /// </summary>
            if (Bools.IsSpellAvailable(SpellSlots.Ignite) &&
                Targets.Target != null &&
                Targets.Target.IsValidTarget(610f) &&
                ObjectManager.Player.GetSummonerSpellDamage(Targets.Target, Damage.SummonerSpell.Ignite) > Targets.Target.Health)
            {
                ObjectManager.Player.Spellbook.CastSpell(SpellSlots.Ignite, Targets.Target);
            }

            /// <summary>
            /// The Barrier Logic.
            /// </summary>
            if (Bools.IsSpellAvailable(SpellSlots.Barrier) &&
                HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/6)
            {
                ObjectManager.Player.Spellbook.CastSpell(SpellSlots.Barrier);
            }

            /// <summary>
            /// The Heal Logic.
            /// </summary>
            if (Bools.IsSpellAvailable(SpellSlots.Heal) &&
                (HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/6 ||
                    HealthPrediction.GetHealthPrediction(Targets.Ally, (int)(250 + Game.Ping / 2f)) <= Targets.Ally.MaxHealth/6))
            {
                ObjectManager.Player.Spellbook.CastSpell(SpellSlots.Heal, Targets.Ally);
            }

            /// <summary>
            /// The Exhaust Logic.
            /// </summary>
            if (Bools.IsSpellAvailable(SpellSlots.Exhaust) &&
                (HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/6 ||
                    HealthPrediction.GetHealthPrediction(Targets.Ally, (int)(250 + Game.Ping / 2f)) <= Targets.Ally.MaxHealth/6))
            {
                ObjectManager.Player.Spellbook.CastSpell(SpellSlots.Exhaust, Targets.Target);
            }
        }
    }
}
