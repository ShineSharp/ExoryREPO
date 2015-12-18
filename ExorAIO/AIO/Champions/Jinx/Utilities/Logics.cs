namespace ExorAIO.Champions.Jinx
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using ExorAIO.Utilities;

    using ItemData = LeagueSharp.Common.Data.ItemData;

    using Orbwalking = SFXTargetSelector.Orbwalking;

    /// <summary>
    /// The logics class.
    /// </summary>
    public class Logics
    {
        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteAuto(EventArgs args)
        {
            var fishbonesRange = Variables.Q.Range + (25f * ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Q).Level);

            /// <summary>
            /// The Q Switching Logic.
            /// </summary>
            if (Variables.Q.IsReady())
            {
                switch (Variables.Orbwalker.ActiveMode)
                {
                    /// <summary>
                    /// The Q Combo Logic.
                    /// </summary>
                    case Orbwalking.OrbwalkingMode.Combo:

                        if (((Bools.IsUsingFishBones() && Targets.Target.IsValidTarget(Ranges.StaticMinigunRange)) ||
                                (!Bools.IsUsingFishBones() &&
                                    (!Targets.Target.IsValidTarget(Ranges.StaticMinigunRange) &&
                                    Targets.Target.IsValidTarget(fishbonesRange)))) &&

                                Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqauto").GetValue<bool>())
                        {
                            Variables.Q.Cast();
                        }

                    break;

                    /// <summary>
                    /// The Q Farm Logic.
                    /// </summary>
                    case Orbwalking.OrbwalkingMode.LaneClear:

                        if ((Bools.IsUsingFishBones() && ObjectManager.Player.ManaPercent < ManaManager.NeededQMana) &&
                            (((GameObjects.EnemyMinions.Count(
                                minions =>
                                    minions.Distance(
                                        GameObjects.EnemyMinions.Where(
                                            qminion =>
                                                qminion.IsValidTarget(fishbonesRange)).FirstOrDefault()) < 225f) > 2 &&
                                !Bools.IsUsingFishBones()) ||

                                (GameObjects.EnemyMinions.Count(
                                    minions =>
                                        minions.Distance(
                                            GameObjects.EnemyMinions.Where(
                                                qminion =>
                                                    qminion.IsValidTarget(fishbonesRange)).FirstOrDefault()) < 225f) < 2 &&
                                    Bools.IsUsingFishBones())) &&

                                Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>()))
                        {
                            Variables.Q.Cast();
                        }

                    break;
                }
            }

            /// <summary>
            /// The W KillSteal Logic,
            /// The W Immobile Harass Logic,
            /// The W Combo Part.1 Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Variables.W.GetPrediction(Targets.Target).Hitchance >= HitChance.High &&

                (!Targets.Target.IsValidTarget(fishbonesRange) &&
                    Targets.Target.IsValidTarget(Variables.W.Range)) &&

                ((Targets.Target.Health < Variables.W.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewks").GetValue<bool>()) ||

                (Bools.IsImmobile(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewimmobile").GetValue<bool>()) ||

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewcombo").GetValue<bool>())))
            {
                Variables.W.Cast(Variables.W.GetPrediction(Targets.Target).UnitPosition);
            }

            /// <summary>
            /// The E against Immobile Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Targets.Target.IsValidTarget(Variables.E.Range) &&
                
                ((Targets.Target.GetEnemiesInRange(350f)
                    .Count(
                        enemy =>
                            Variables.E.GetPrediction(enemy).Hitchance >= HitChance.VeryHigh) >= 2 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeauto").GetValue<bool>()) ||
                            
                (Bools.IsImmobile(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeimmobile").GetValue<bool>())))
            {
                Variables.E.Cast(Variables.E.GetPrediction(Targets.Target).CastPosition);
            }

            /// <summary>
            /// The R KillSteal Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Bools.HasNoProtection(Targets.Target) &&

                (!Variables.W.IsReady() &&
                    !Targets.Target.IsValidTarget(fishbonesRange) &&
                    Targets.Target.IsValidTarget(Variables.W.Range)) &&

                (HealthPrediction.GetHealthPrediction(Targets.Target, 550) < Variables.R.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userks").GetValue<bool>()))
            {
                Variables.R.Cast(Variables.R.GetPrediction(Targets.Target).UnitPosition);
            }
        }

        /// <summary>
        /// Called when a cast gets executed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void ExecuteSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// The E on SpellCast Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeauto").GetValue<bool>())
            {
                if (args.Slot == SpellSlot.R ||
                    (args.Slot == SpellSlot.Q && 
                        (((Obj_AI_Hero)sender).ChampionName.Equals("Blitzcrank") ||
                        ((Obj_AI_Hero)sender).ChampionName.Equals("Thresh"))))
                {
                    if (ObjectManager.Player.Distance(sender) / 2000 < 0.4f)
                    {
                        Variables.E.Cast(sender.ServerPosition);
                    }
                }
            }
        }

        /// <summary>
        /// Called when a cast gets executed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void ExecuteModes(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            var fishbonesRange = Variables.Q.Range + (25f * ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Q).Level);

            /// <summary>
            /// The W Combo Part2 Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Variables.W.GetPrediction(Targets.Target).Hitchance >= HitChance.High &&

                (!((Obj_AI_Hero)args.Target).IsValidTarget(fishbonesRange) &&
                    ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.W.Range)) &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewcombo").GetValue<bool>()))
            {
                Variables.W.Cast(Variables.W.GetPrediction((Obj_AI_Hero)args.Target).UnitPosition);
            }
        }
    }
}
