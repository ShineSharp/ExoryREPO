using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Jinx
{
    using System;
    using System.Linq;
    using ExorAIO.Utilities;
    using SharpDX;
    using Orbwalking = SFXTargetSelector.Orbwalking;

    /// <summary>
    /// The logics class.
    /// </summary>
    class Logics
    {
        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteAuto(EventArgs args)
        {
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
                    case Orbwalking.OrbwalkingMode.Mixed:

                        if (((!ObjectManager.Player.HasBuff("JinxQ") &&
                                !Targets.Target.IsValidTarget(Variables.Q.Range) &&
                                Targets.Target.IsValidTarget(Variables.Q2.Range)) ||

                            (ObjectManager.Player.HasBuff("JinxQ") &&
                                Targets.Target.IsValidTarget(Variables.Q.Range))) &&
                            Variables.Menu.Item($"{Variables.MainMenuName}.qspell.auto").GetValue<bool>())
                        {
                            Variables.Q.Cast();
                        }

                    break;

                    /// <summary>
                    /// The Q Farm Logic.
                    /// </summary>
                    case Orbwalking.OrbwalkingMode.LaneClear:

                        if (((!ObjectManager.Player.HasBuff("JinxQ") &&
                                GameObjects.EnemyMinions
                                    .Count(minions => minions.Distance(GameObjects.EnemyMinions
                                    .Where(qminion => qminion.IsValidTarget(Variables.Q2.Range))
                                    .FirstOrDefault()) < 225f) > 2) ||

                            (ObjectManager.Player.HasBuff("JinxQ") &&
                                GameObjects.EnemyMinions
                                    .Count(minions => minions.Distance(GameObjects.EnemyMinions
                                    .Where(qminion => qminion.IsValidTarget(Variables.Q2.Range))
                                    .FirstOrDefault()) < 225f) < 2) ||

                            (ObjectManager.Player.HasBuff("JinxQ") &&
                                ObjectManager.Player.ManaPercent < ManaManager.NeededQMana)) &&
                            Variables.Menu.Item($"{Variables.MainMenuName}.qspell.farm").GetValue<bool>())
                        {
                            Variables.Q.Cast();
                        }

                    break;  
                }
            }

            /// <summary>
            /// The W KillSteal Logic,
            /// The W Immobile Harass Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&
                !Targets.Target.IsValidTarget(Variables.Q2.Range) &&
                Variables.W.GetPrediction(Targets.Target).Hitchance >= HitChance.High &&

                ((Targets.Target.Health < Variables.W.GetDamage(Targets.Target) &&
                    Targets.Target.Health > ObjectManager.Player.GetAutoAttackDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wspell.ks").GetValue<bool>()) ||

                (Bools.IsImmobile(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wspell.immobile").GetValue<bool>())))
            {
                Variables.W.Cast(Variables.W.GetPrediction(Targets.Target).UnitPosition);
            }

            /// <summary>
            /// The Automatic E Logic,
            /// The E Impaired Harass Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ObjectManager.Player
                    .Distance(Targets.Target.ServerPosition
                    .Extend(Targets.Target.ServerPosition, ObjectManager.Player.Distance(Targets.Target) + Targets.Target.BoundingRadius*2)) < Variables.E.Range &&

                ((Targets.Target.GetEnemiesInRange(350f)
                    .Count(enemy => Variables.E.GetPrediction(enemy).Hitchance >= HitChance.VeryHigh) >= 1 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.espell.auto").GetValue<bool>()) ||

                (Bools.IsImmobile(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.espell.immobile").GetValue<bool>())))
            {
                Variables.E.Cast(ObjectManager.Player.ServerPosition.Extend(Targets.Target.ServerPosition, ObjectManager.Player.Distance(Targets.Target) + Targets.Target.BoundingRadius*2));
            }

            /// <summary>
            /// The R KillSteal Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                !Variables.W.IsReady() &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&
                !Targets.Target.IsValidTarget(Variables.Q2.Range) &&
                Variables.W.GetDamage(Targets.Target) < Targets.Target.Health &&
                Variables.R.GetPrediction(Targets.Target).Hitchance >= HitChance.VeryHigh &&
                HealthPrediction.GetHealthPrediction(Targets.Target, (int)(250 + Game.Ping / 2f)) < Variables.R.GetDamage(Targets.Target)*2 &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.ks").GetValue<bool>())
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
            if (Variables.E.IsReady() &&
                ObjectManager.Player
                    .Distance(sender.ServerPosition
                    .Extend(sender.ServerPosition, ObjectManager.Player.Distance(sender) + sender.BoundingRadius)) < Variables.E.Range &&
                (args.Slot.Equals(SpellSlot.R) || 
                    (args.Slot.Equals(SpellSlot.Q) && 
                    (((Obj_AI_Hero)sender).ChampionName.Equals("Blitzcrank") ||
                        ((Obj_AI_Hero)sender).ChampionName.Equals("Thresh")))))
            {
                if (ObjectManager.Player.Distance(sender) / 2000f < 0.4f)
                {
                    Variables.E.Cast(ObjectManager.Player.ServerPosition.Extend(sender.ServerPosition, ObjectManager.Player.Distance(sender) + sender.BoundingRadius));
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
            /// <summary>
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.W.Range) &&
                !((Obj_AI_Hero)args.Target).IsValidTarget(Variables.Q.Range) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.W.GetPrediction(Targets.Target).Hitchance >= HitChance.High &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.combo").GetValue<bool>())
            {
                Variables.W.Cast(Variables.W.GetPrediction((Obj_AI_Hero)args.Target).UnitPosition);
            }
        }
    }
}
