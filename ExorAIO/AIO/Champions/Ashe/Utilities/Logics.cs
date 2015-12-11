namespace ExorAIO.Champions.Ashe
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
            /// The Q Combo Logic,
            /// The Q Farm Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ObjectManager.Player.HasBuff("AsheQCastReady") &&
                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>()) ||

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>())))
            {
                Variables.Q.Cast();
            }

            /// <summary>
            /// The W Immobile Harass Logic,
            /// The W KillSteal Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ((Targets.Target.Health < Variables.W.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewks").GetValue<bool>())) ||

                (Bools.IsImmobile(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewimmobile").GetValue<bool>()))
            {
                Variables.W.CastIfHitchanceEquals(Targets.Target, HitChance.VeryHigh, false);
            }
            
            /// <summary>
            /// The R Logics.
            /// </summary>
            if (Variables.R.IsReady() &&
                Targets.Target.IsValidTarget(1200))
            {
                /// <summary>
                /// R KillSteal Logic.
                /// </summary>
                if ((!Variables.W.IsReady() || !Targets.Target.IsValidTarget(Variables.W.Range)) &&
                    (Targets.Target.Health < Variables.R.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userks").GetValue<bool>()))
                {
                    Variables.R.Cast(Variables.R.GetPrediction(Targets.Target).UnitPosition);
                }

                /// <summary>
                /// The R Doublelift Mechanic Logic,
                /// The R Normal Combo Logic.
                /// </summary>
                if (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.usercombo").GetValue<bool>() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.rwhitelist.{Targets.Target.ChampionName.ToLower()}").GetValue<bool>())
                {
                    if (Variables.E.IsReady() &&
                        !Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(null)) &&
                        Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.usermechanic").GetValue<bool>())
                    {
                        Variables.E.Cast(Variables.E.GetPrediction(Targets.Target).UnitPosition);
                    }

                    Variables.R.Cast(Variables.R.GetPrediction(Targets.Target).UnitPosition);
                }
            }
        }

        public static void ExecuteModes(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewcombo").GetValue<bool>()))
            {
                Variables.W.CastIfHitchanceEquals((Obj_AI_Hero)args.Target, HitChance.High, false);
            }
        }

        public static void ExecuteFarm(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// The W Farm Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewfarm").GetValue<bool>() &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                    Variables.W.GetLineFarmLocation(Targets.Minions, Variables.W.Width).MinionsHit >= 3))
            {
                Variables.W.Cast(Variables.W.GetLineFarmLocation(Targets.Minions, Variables.W.Width).Position);
            }
        }
    }
}
