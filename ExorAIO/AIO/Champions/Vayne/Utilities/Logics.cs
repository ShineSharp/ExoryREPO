namespace ExorAIO.Champions.Vayne
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using SharpDX;

    using ExorAIO.Utilities;

    using Orbwalking = SFXTargetSelector.Orbwalking;
    using TargetSelector = SFXTargetSelector.TargetSelector;

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
            /// <summary>
            /// The Target preference.
            /// </summary>
            if (TargetSelector.Weights.GetItem("low-health") != null)
            {
                TargetSelector.Weights.GetItem("low-health").ValueFunction = hero => hero.Health - KillSteal.GetDamage(hero)*2;
                TargetSelector.Weights.GetItem("low-health").Tooltip = "Low Health (Health < 2W + E Damage) = Higher Weight";
                TargetSelector.Weights.Register(
                    new TargetSelector.Weights.Item(
                        "w-stack", "W Stack", 10, false, hero => hero.GetBuffCount("vaynesilvereddebuff") == 2 ? 1 : 0,
                        "Has 2W Stacks = Higher Weight"));
            }

            /// <summary>
            /// The No AA when Stealthed Logic.
            /// </summary>
            Variables.Orbwalker.SetAttack(!Bools.ShouldStayFaded());

            /// <summary>
            /// The E Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Bools.HasNoProtection(Targets.Target) &&
                Targets.Target.IsValidTarget(Variables.E.Range))
            {
                /// <summary>
                /// The Condemn Logic.
                /// </summary>
                if (!ObjectManager.Player.IsDashing() &&
                    ObjectManager.Player.Distance(Targets.Target) >= ObjectManager.Player.BoundingRadius + 75f &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeauto").GetValue<bool>() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.ewhitelist.{Targets.Target.ChampionName.ToLower()}").GetValue<bool>())
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

                /// <summary>
                /// The E KillSteal Logic.
                /// </summary>
                if (Targets.Target.Distance(ObjectManager.Player) > 500f &&

                    (Targets.Target.Health < KillSteal.GetDamage(Targets.Target) &&
                        Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeks").GetValue<bool>()))
                {
                    Variables.E.CastOnUnit(Targets.Target);
                }
            }

            /// <summary>
            /// The Q KillSteal Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&

                (Targets.Target.ServerPosition.Distance(ObjectManager.Player.ServerPosition) >= Orbwalking.GetRealAutoAttackRange(Targets.Target) &&
                    HealthPrediction.GetHealthPrediction(Targets.Target, (int)(250 + Game.Ping / 2f)) < ObjectManager.Player.GetAutoAttackDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqks").GetValue<bool>()))
            {
                Variables.Q.Cast(Targets.Target.Position);
                TargetSelector.SetTarget(Targets.Target);
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
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>()) &&

                (((Obj_AI_Hero)target).GetBuffCount("vaynesilvereddebuff") == 1 ||
                    !Variables.Menu.Item($"{Variables.MainMenuName}.miscsettings.q2wstacks").GetValue<bool>()))
            {
                Utility.DelayAction.Add(
                    (int)(Game.Ping / 2f + 25), // BroScience? Pls check The Orbwalking.CanAttack method fgt.
                    () =>
                    {
                        Variables.Q.Cast(Game.CursorPos);
                    }
                );
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

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>()) &&

                (((Obj_AI_Hero)args.Target).GetBuffCount("vaynesilvereddebuff") == 1 ||
                    !Variables.Menu.Item($"{Variables.MainMenuName}.miscsettings.q2wstacks").GetValue<bool>()))
            {
                Variables.Q.Cast(Game.CursorPos);
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

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LastHit ||
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Mixed) &&

                    ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    (Targets.Minions.Count() > 1 ||
                        GameObjects.Jungle.Contains((Obj_AI_Minion)args.Target)) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>()))
            {
                Variables.Q.Cast(Game.CursorPos);
            }
        }
    }
}
