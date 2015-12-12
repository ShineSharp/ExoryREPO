namespace ExorAIO.Champions.Ezreal
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
            /// The Q KillSteal Logic,
            /// The Q Immobile Harass Logic,
            /// The Q AutoHarass Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&
                Variables.Q.GetPrediction(Targets.Target).Hitchance >= HitChance.VeryHigh &&

                ((Targets.Target.Health < Variables.Q.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqks").GetValue<bool>()) ||

                (Bools.IsImmobile(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqimmobile").GetValue<bool>()) ||

                (ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqautoharass").GetValue<bool>())))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
            }

            /// <summary>
            /// The W KillSteal Logic,
            /// The W Immobile Harass Logic,
            /// The W AutoHarass Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                !Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&

                ((Targets.Target.Health < Variables.W.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewks").GetValue<bool>()) ||

                (Bools.IsImmobile(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewimmobile").GetValue<bool>()) ||

                (ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewautoharass").GetValue<bool>())))
            {
                Variables.W.Cast(Variables.W.GetPrediction(Targets.Target).UnitPosition);
            }

            /// <summary>
            /// The R KillSteal Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                ((!Variables.W.IsReady() || !Targets.Target.IsValidTarget(Variables.W.Range)) &&
                (!Variables.Q.IsReady() || !Targets.Target.IsValidTarget(Variables.Q.Range))) &&
                Targets.Target.IsValidTarget(1500) &&

                (Targets.Target.Health < Variables.R.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userks").GetValue<bool>()))
            {
                Variables.R.Cast(Variables.R.GetPrediction(Targets.Target).UnitPosition);
            }

        }

        public static void ExecuteModes(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Variables.Q.GetPrediction(Targets.Target).Hitchance >= HitChance.High &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>()))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
                return;
            }

            /// <summary>
            /// The W Combo Logic,
            /// The E Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewcombo").GetValue<bool>()))
            {
                Variables.W.Cast(((Obj_AI_Hero)args.Target).Position);
                
                if (Variables.E.IsReady() &&
                    !(Utility.UnderTurret((Obj_AI_Hero)args.Target)) &&
                    
                    (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                        Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useecombo").GetValue<bool>()))
                {
                    Variables.E.Cast(((Obj_AI_Hero)args.Target).Position);
                }
                return;
            }

            /// <summary>
            /// The R Combo Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                !Variables.Q.IsReady() &&
                !Variables.W.IsReady() &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.usercombo").GetValue<bool>() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.rwhitelist.{((Obj_AI_Hero)args.Target).ChampionName.ToLower()}").GetValue<bool>()))
            {
                Variables.R.Cast(Variables.R.GetPrediction(Targets.Target).UnitPosition);
            }
        }

        public static void ExecuteFarm(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// The Q Farm Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Targets.Minions.Any() &&
                Targets.Minions.FirstOrDefault().Health < Variables.Q.GetDamage(Targets.Minions.FirstOrDefault()) &&
                
                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>()))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Minions.FirstOrDefault()).UnitPosition);
            }
        }
    }
}
