namespace ExorAIO.Champions.Vayne
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using SharpDX;

    using Orbwalking = SFXTargetSelector.Orbwalking;

    using ExorAIO.Utilities;

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
            /// The No AA when Stealthed Logic.
            /// </summary>
            Variables.Orbwalker.SetAttack(!Bools.ShouldStayFaded());

            /// <summary>
            /// The E Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                !ObjectManager.Player.IsDashing() &&
                Bools.HasNoProtection(Targets.Target) &&
                Targets.Target.IsValidTarget(Variables.E.Range))
            {
                /// <summary>
                /// The Condemn Logic.
                /// </summary>
                if (ObjectManager.Player.Distance(Targets.Target) >= ObjectManager.Player.BoundingRadius + 75f &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeauto").GetValue<bool>() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.ewhitelist.{Targets.Target.ChampionName.ToLower()}").GetValue<bool>())
                {
                    for (int i = 1; i < 10; i++)
                    {
                        if ((Variables.E.GetPrediction(Targets.Target).UnitPosition + Vector3.Normalize(Targets.Target.ServerPosition - ObjectManager.Player.Position) * i * 44).IsWall() &&
                            (Variables.E.GetPrediction(Targets.Target).UnitPosition + Vector3.Normalize(Targets.Target.ServerPosition - ObjectManager.Player.Position) * i * 44 + Targets.Target.BoundingRadius).IsWall())
                        {
                            Variables.E.CastOnUnit(Targets.Target);
                            return;
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
                Targets.Target.IsValidTarget(Variables.Q.Range) &&
                !ObjectManager.Player.IsWindingUp &&

                (Targets.Target.ServerPosition.Distance(ObjectManager.Player.ServerPosition) >= Orbwalking.GetRealAutoAttackRange(null) &&
                    HealthPrediction.GetHealthPrediction(Targets.Target, (int) (250 + Game.Ping / 2f)) < ObjectManager.Player.GetAutoAttackDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqks").GetValue<bool>()))
            {
                Variables.Q.Cast(Targets.Target.Position);
                TargetSelector.SetTarget(Targets.Target);
            }
        }

        /*
        /// <summary>
        /// Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void ExecuteModes(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
        */

        /// <summary>
        /// Called on-attack request.
        /// </summary>
        /// <param name="unit">The sender.</param>
        /// <param name="target">The target.</param>
        public static void ExecuteBetaModes(AttackableUnit unit, AttackableUnit target)
        {
            Utility.DelayAction.Add(
                (int)(Game.Ping / 2f + 25) < 30 ?
                    30 :
                    (int)(Game.Ping / 2f + 25),
                () =>
                {
                    /// <summary>
                    /// The Q Combo Logic.
                    /// </summary>
                    if (Variables.Q.IsReady() &&

                        (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                            Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>()))
                    {
                        Variables.Q.Cast(Game.CursorPos);
                    }
                }
            );
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
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>()))
            {
                Variables.Q.Cast(Game.CursorPos);
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteFarm(EventArgs args)
        {
            /// <summary>
            /// The Q Farm Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&

                (Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.None &&
                    Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.Combo &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    (Targets.Minion != null ||
                        GameObjects.JungleLarge.Contains((Obj_AI_Minion)Variables.Orbwalker.GetTarget()) ||
                        GameObjects.JungleLegendary.Contains((Obj_AI_Minion)Variables.Orbwalker.GetTarget())) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>()))
            {
                Variables.Q.Cast(Game.CursorPos);
                Variables.Orbwalker.ForceTarget(Targets.Minion);
            }
        }
    }
}
