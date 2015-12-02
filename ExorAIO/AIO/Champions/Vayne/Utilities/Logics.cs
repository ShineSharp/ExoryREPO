namespace ExorAIO.Champions.Vayne
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using SharpDX;

    using ExorAIO.Utilities;

    public class Logics
    {
        public static void ExecuteAuto(EventArgs args)
        {
            /// <summary>
            /// The No AA when Stealthed Logic.
            /// </summary>
            Variables.Orbwalker.SetAttack(!Bools.ShouldStayFaded());

            if (Variables.E.IsReady() &&
                Targets.Target.IsValidTarget(Variables.E.Range))
            {
                /// <summary>
                /// The Condemn Logic.
                /// </summary>
                if (Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeauto").GetValue<bool>() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.ewhitelist.{Targets.Target.ChampionName.ToLower()}").GetValue<bool>())
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        if ((Variables.E.GetPrediction(Targets.Target).UnitPosition + Vector3.Normalize(Targets.Target.ServerPosition - ObjectManager.Player.Position) * i * 50).IsWall())
                        {
                            Orbwalking.ResetAutoAttackTimer();
                            Variables.E.CastOnUnit(Targets.Target);
                        }
                    }
                }

                /// <summary>
                /// The E KillSteal Logic.
                /// </summary>
                if (Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeks").GetValue<bool>() &&
                    Targets.Target.Health < KillSteal.Damage())
                {
                    Orbwalking.ResetAutoAttackTimer();
                    Variables.E.CastOnUnit(Targets.Target);
                }
            }

            /// <summary>
            /// The Q KillSteal Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqks").GetValue<bool>() &&
                Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(null) + 240f) &&
                Targets.Target.ServerPosition.Distance(ObjectManager.Player.ServerPosition) >= Orbwalking.GetRealAutoAttackRange(null) &&
                HealthPrediction.GetHealthPrediction(Targets.Target, (int) (250 + Game.Ping / 2f)) < ObjectManager.Player.GetAutoAttackDamage(Targets.Target))
            {
                Orbwalking.ResetAutoAttackTimer();
                Variables.Q.Cast(Targets.Target.Position);
                TargetSelector.SetTarget(Targets.Target);
                return;
            }
        }

        public static void ExecuteFarm(EventArgs args)
        {
            /// <summary>
            /// The Q Farm Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                Targets.FarmMinion.IsValidTarget(Orbwalking.GetRealAutoAttackRange(null) + 240f) &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>() && Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.Combo)
            {
                Orbwalking.ResetAutoAttackTimer();
                Variables.Q.Cast(Targets.FarmMinion);
                Variables.Orbwalker.ForceTarget(Targets.FarmMinion);
            }
        }

        public static void ExecuteModes(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>() && Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo)
            {
                Orbwalking.ResetAutoAttackTimer();
                Variables.Q.Cast(Game.CursorPos);
            }
        }
    }
}
