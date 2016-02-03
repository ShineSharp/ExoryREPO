using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Vayne
{
    using System;
    using System.Linq;
    using ExorAIO.Utilities;
    using SharpDX;
    using Orbwalking = SFXTargetSelector.Orbwalking;
    using TargetSelector = SFXTargetSelector.TargetSelector;

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
            /// The No AA when Stealthed Logic.
            /// </summary>
            Variables.Orbwalker.SetAttack(!Bools.ShouldStayFaded());

            /// <summary>
            /// The E Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Targets.Target.IsValidTarget(Variables.E.Range))
            {
                /// <summary>
                /// The Condemn Logic.
                /// </summary>
                if (!ObjectManager.Player.IsDashing() &&
                    ObjectManager.Player.Distance(Targets.Target) >= ObjectManager.Player.BoundingRadius*2 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.espell.auto").GetValue<bool>() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.espell.whitelist.{Targets.Target.ChampionName.ToLower()}").GetValue<bool>())
                {
                    for (int i = 1; i < 10; i++)
                    {
                        if ((Targets.Target.Position + Vector3.Normalize(Targets.Target.ServerPosition - ObjectManager.Player.Position) * i * 43).IsWall() &&
                            (Variables.E.GetPrediction(Targets.Target).UnitPosition + Vector3.Normalize(Targets.Target.ServerPosition - ObjectManager.Player.Position) * i * 43).IsWall() &&
                            (Variables.E.GetPrediction(Targets.Target).UnitPosition + Vector3.Normalize(Targets.Target.ServerPosition - ObjectManager.Player.Position) * i * 44).IsWall())
                        {
                            Variables.E.CastOnUnit(Targets.Target);
                        }
                    }
                }
            }

            /// <summary>
            /// The Q KillSteal Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&
                Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)) &&
                HealthPrediction.GetHealthPrediction(Targets.Target, (int)(250 + Game.Ping / 2f)) < ObjectManager.Player.GetAutoAttackDamage(Targets.Target) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").GetValue<bool>())
            {
                Variables.Q.Cast(Targets.Target.Position);
                TargetSelector.Selected.Target = Targets.Target;
            }
        }

        /// <summary>
        /// Called on-attack request.
        /// </summary>
        /// <param name="unit">The sender.</param>
        /// <param name="target">The target.</param>
        public static void ExecuteBetaModes(AttackableUnit unit, AttackableUnit target)
        {
            /// <summary>
            /// The Beta Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").GetValue<bool>() &&
                (((Obj_AI_Hero)target).GetBuffCount("vaynesilvereddebuff") == 1 ||
                    !Variables.Menu.Item($"{Variables.MainMenuName}.misc.stacks").GetValue<bool>()))
            {
                Utility.DelayAction.Add(
                    (int)(Game.Ping / 2f + 25), // BroScience? Pls check The Orbwalking.CanAttack method fgt.
                    () =>
                    {
                        Variables.Q.Cast(Game.CursorPos);
                    }
                );
            }

            /// <summary>
            /// The Beta E KillSteal Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ((Obj_AI_Hero)target).Health < 
                    Variables.E.GetDamage((Obj_AI_Hero)target) +
                    ObjectManager.Player.GetAutoAttackDamage((Obj_AI_Hero)target) +
                    (((Obj_AI_Hero)target).GetBuffCount("vaynesilvereddebuff") == 2 ?
                        Variables.W.GetDamage((Obj_AI_Hero)target) : 0) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.ks").GetValue<bool>())
            {
                Variables.E.CastOnUnit((Obj_AI_Hero)target);
            }
        }

        /// <summary>
        /// Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void ExecuteModes(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").GetValue<bool>() &&
                (((Obj_AI_Hero)args.Target).GetBuffCount("vaynesilvereddebuff") == 1 ||
                    !Variables.Menu.Item($"{Variables.MainMenuName}.misc.stacks").GetValue<bool>()))
            {
                Variables.Q.Cast(Game.CursorPos);
            }

            /// <summary>
            /// The E KillSteal Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ((Obj_AI_Hero)args.Target).Health < 
                    Variables.E.GetDamage((Obj_AI_Hero)args.Target) +
                    ObjectManager.Player.GetAutoAttackDamage((Obj_AI_Hero)args.Target) +
                    (((Obj_AI_Hero)args.Target).GetBuffCount("vaynesilvereddebuff") == 2 ?
                        Variables.W.GetDamage((Obj_AI_Hero)args.Target) : 0) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.ks").GetValue<bool>())
            {
                Variables.E.CastOnUnit((Obj_AI_Hero)args.Target);
            }
        }

        /// <summary>
        /// Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void ExecuteFarm(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// The Q Farm Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.Combo &&
                (Targets.Minions.Count() > 1 ||
                    GameObjects.Jungle.Contains((Obj_AI_Minion)args.Target)) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.farm").GetValue<bool>())
            {
                Variables.Q.Cast(Game.CursorPos);
            }
        }
    }
}
