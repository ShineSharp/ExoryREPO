namespace ExorAIO.Champions.Corki
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
            /// Q KillSteal Logic,
            /// Q Immobile Harass Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ((Variables.Q.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqks").GetValue<bool>()) ||

                (Bools.IsImmobile(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqimmobile").GetValue<bool>())))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).CastPosition);
            }

            /// <summary>
            /// E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useecombo").GetValue<bool>()))
            {
                Variables.E.Cast();
            }

            /// <summary>
            /// R KillSteal Logic,
            /// R AutoHarass Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Variables.R.GetPrediction(Targets.Target).Hitchance >= HitChance.VeryHigh &&
                ((Variables.R.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userks").GetValue<bool>()) ||

                (ObjectManager.Player.ManaPercent > ManaManager.NeededRMana &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userharass").GetValue<bool>() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.rwhitelist.{Targets.Target.ChampionName.ToLower()}").GetValue<bool>())))
            {
                Variables.R.Cast(Variables.R.GetPrediction(Targets.Target).UnitPosition);
            }
        }

        public static void ExecuteModes(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>()))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction((Obj_AI_Hero)args.Target).CastPosition);
            }

            /// <summary>
            /// R Combo Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Variables.R.GetPrediction(Targets.Target).Hitchance >= HitChance.VeryHigh &&
                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.usercombo").GetValue<bool>()))
            {
                Variables.R.CastIfHitchanceEquals((Obj_AI_Hero)args.Target, HitChance.VeryHigh, false);
            }
        }

        public static void ExecuteFarm(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// Q Farm Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).MinionsHit >= 3))
            {
                Variables.Q.Cast(Variables.Q.GetCircularFarmLocation(Targets.Minions, Variables.Q.Width).Position);
            }

            /// <summary>
            /// E Farm Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useefarm").GetValue<bool>() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededEMana &&
                Targets.Minions.Count() > 3))
            {
                Variables.E.Cast();
            }

            /// <summary>
            /// R Farm Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userfarm").GetValue<bool>() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededRMana &&
                Variables.R.GetLineFarmLocation(Targets.Minions, Variables.R.Width).MinionsHit >= 2))
            {
                Variables.Q.Cast(Variables.R.GetLineFarmLocation(Targets.Minions, Variables.R.Width).Position);
            }
        }
    }
}
