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
                        if ((Targets.Target.Position + Vector3.Normalize(Targets.Target.ServerPosition - ObjectManager.Player.Position) * i * 47).IsWall() &&
                            (Variables.E.GetPrediction(Targets.Target).UnitPosition + Vector3.Normalize(Targets.Target.ServerPosition - ObjectManager.Player.Position) * i * 47).IsWall())
                        {
                            Variables.E.CastOnUnit(Targets.Target);
                            return;
                        }
                    }
                }

                /// <summary>
                /// The E KillSteal Logic.
                /// </summary>
                if (Bools.HasNoProtection(Targets.Target) &&

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

                (Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.Combo &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    (Targets.Minions.Count() > 1 ||
                        Targets.Minions.FirstOrDefault().CharData.BaseSkinName.Contains("SRU_") ||
                        Targets.Minions.FirstOrDefault().CharData.BaseSkinName.Contains("Mini")) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>()))
            {
                Variables.Q.Cast(Game.CursorPos);
            }
        }
    }
}
