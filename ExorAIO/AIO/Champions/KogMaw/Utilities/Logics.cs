namespace ExorAIO.Champions.KogMaw
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
            /// The Q Immobile Harass Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqks").GetValue<bool>() && Targets.Target.Health < Variables.Q.GetDamage(Targets.Target)) ||
                (Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqimmobile").GetValue<bool>() && Bools.IsImmobile(Targets.Target)))
            {
                Variables.Q.CastIfHitchanceEquals(Targets.Target, HitChance.VeryHigh, false);
            }
            
            /// <summary>
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewcombo").GetValue<bool>() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo))
            {
                Variables.W.Cast();
            }

            /// <summary>
            /// The E KillSteal Logic,
            /// The E Immobile Harass Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeks").GetValue<bool>() && Targets.Target.Health < Variables.E.GetDamage(Targets.Target)) ||
                (Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeimmobile").GetValue<bool>() && Bools.IsImmobile(Targets.Target)))
            {
                Variables.E.CastIfHitchanceEquals(Targets.Target, HitChance.VeryHigh, false);
            }

            /// <summary>
            /// The R Combo Logic,
            /// The R KillSteal Logic,
            /// The R Farm Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.user").GetValue<bool>())
            {
                if (Targets.Target.HealthPercent < 50 &&
                    (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                        Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.rcombokeepstacks").GetValue<Slider>().Value >= ObjectManager.Player.GetBuffCount("kogmawlivingartillerycost")) ||
                        (Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userks").GetValue<bool>() && Targets.Target.Health < Variables.R.GetDamage(Targets.Target)))
                {
                    Variables.R.CastIfHitchanceEquals(Targets.Target, HitChance.VeryHigh, false);
                    return;
                }
                else if (Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userfarm").GetValue<bool>() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.rfarmkeepstacks").GetValue<Slider>().Value <= ObjectManager.Player.GetBuffCount("kogmawlivingartillerycost") &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                    Variables.R.GetCircularFarmLocation(Targets.Minions, Variables.R.Width).MinionsHit >= 3 &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededRMana)
                {
                    Variables.R.Cast(Variables.R.GetCircularFarmLocation(Targets.Minions, Variables.R.Width).Position);
                }
            }
        }

        public static void ExecuteModes(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ((Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>() && Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo) ||
                (Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqharass").GetValue<bool>() && ObjectManager.Player.ManaPercent > ManaManager.NeededQMana)))
            {
                Variables.Q.CastIfHitchanceEquals((Obj_AI_Hero)args.Target, HitChance.VeryHigh, false);
                return;
            }

            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useecombo").GetValue<bool>() && Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo) ||
                (Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeharassfarm").GetValue<bool>() && ObjectManager.Player.ManaPercent > ManaManager.NeededEMana))
            {
                Variables.E.CastIfHitchanceEquals((Obj_AI_Hero)args.Target, HitChance.VeryHigh, false);
            }
        }

        public static void ExecuteFarm(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// The E Farm Logic.
            /// </summary>
            if (Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeharassfarm").GetValue<bool>() &&
                Variables.E.GetLineFarmLocation(Targets.Minions, Variables.E.Width).MinionsHit >= 3 &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededEMana)
            {
                Variables.E.Cast(Variables.E.GetLineFarmLocation(Targets.Minions, Variables.E.Width).Position);
            }
        }
    }    
}
