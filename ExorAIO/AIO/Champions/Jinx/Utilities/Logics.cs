namespace ExorAIO.Champions.Jinx
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using Orbwalking = SFXTargetSelector.Orbwalking;

    using ExorAIO.Utilities;

    public class Logics
    {
        public static void ExecuteAuto(EventArgs args)
        {
            /// <summary>
            /// The Q Switching Logic.
            /// </summary>
            if (Variables.Q.IsReady())
            {
                switch (Variables.Orbwalker.ActiveMode)
                {
                    case Orbwalking.OrbwalkingMode.Combo:
                        if (Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqauto").GetValue<bool>() &&
                            ((Bools.IsUsingFishBones() && Targets.Target.IsValidTarget(ObjectManager.Player.BoundingRadius + 525f)) ||
                            (!Bools.IsUsingFishBones() && 
                                (Targets.Target.IsValidTarget(Variables.Q.Range) &&
                                !Targets.Target.IsValidTarget(ObjectManager.Player.BoundingRadius + 525f)))))
                        {
                            Variables.Q.Cast();
                        }
                    break;

                    case Orbwalking.OrbwalkingMode.LaneClear:
                        if (Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>() &&
                            (Bools.IsUsingFishBones() && ObjectManager.Player.ManaPercent < ManaManager.NeededQMana) &&
                            ((GameObjects.EnemyMinions.Count(minions => minions.Distance(Targets.QMinions.FirstOrDefault()) < 225f) > 2 && !Bools.IsUsingFishBones()) ||
                            (GameObjects.EnemyMinions.Count(minions => minions.Distance(Targets.QMinions.FirstOrDefault()) < 225f) < 2 && Bools.IsUsingFishBones())))
                        {
                            Variables.Q.Cast();
                        }
                    break;

                    default:
                        if (Bools.IsUsingFishBones())
                        {
                            Variables.Q.Cast();
                        }
                    break;
                }
            }

            /// <summary>
            /// The W KillSteal Logic,
            /// The W Immobile Harass Logic.
            /// The W Combo Part.1 Logic
            /// </summary>
            if (Variables.W.IsReady() &&
                !Targets.Target.IsValidTarget(Variables.Q.Range) &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&
                ((Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewks").GetValue<bool>() && Targets.Target.Health <= ObjectManager.Player.GetSpellDamage(Targets.Target, SpellSlot.W)) ||
                (Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewimmobile").GetValue<bool>() && Bools.IsImmobile(Targets.Target)) ||
                (Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewcombo").GetValue<bool>() && Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo)))
            {
                //Variables.W.Cast(Variables.W.GetPrediction(Targets.Target).UnitPosition);
                Variables.W.CastIfHitchanceEquals(Targets.Target, HitChance.VeryHigh, false);
            }

            /// <summary>
            /// The E against Immobile Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Targets.Target.IsValidTarget(Variables.E.Range) &&
                ((Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeimmobile").GetValue<bool>() && Bools.IsImmobile(Targets.Target)) ||
                (Targets.Target.GetEnemiesInRange(350f)
                    .Count(
                        enemy =>
                            Variables.E.GetPrediction(enemy).Hitchance >= HitChance.VeryHigh) >= 2)))
            {
                Variables.E.Cast(Variables.E.GetPrediction(Targets.Target).CastPosition);
            }

            /// <summary>
            /// The R KillSteal Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                !Targets.Target.IsValidTarget(Variables.W.Range) &&
                Targets.Target.IsValidTarget(Variables.W.Range + 200f) &&
                (!Variables.W.IsReady() || !Targets.Target.IsValidTarget(Variables.W.Range)) &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userks").GetValue<bool>() && Targets.Target.Health <= Variables.R.GetDamage(Targets.Target)))
            {
                Variables.R.Cast(Variables.R.GetPrediction(Targets.Target).UnitPosition);
                return;
            }
        }

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

        public static void ExecuteModes(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// The W Combo Part2 Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                !((Obj_AI_Hero)args.Target).IsValidTarget(ObjectManager.Player.BoundingRadius + 525f) &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.W.Range) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewcombo").GetValue<bool>() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo)
            {
                //Variables.W.Cast(Variables.W.GetPrediction((Obj_AI_Hero)args.Target).UnitPosition);
                Variables.W.CastIfHitchanceEquals(Targets.Target, HitChance.VeryHigh, false);
            }
        }
    }
}
