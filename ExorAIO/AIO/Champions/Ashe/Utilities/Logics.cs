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
            /*
                Q Combo Logic;
                Q Farm Logic;
            */
            if (Variables.Q.IsReady() &&
                ObjectManager.Player.HasBuff("AsheQCastReady") &&
                ((Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>() && Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo) ||
                (Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>() && ObjectManager.Player.ManaPercent > ManaManager.NeededQMana)))
            {
                Variables.Q.Cast();
                return;
            }

            /*
                W Immobile Harass Logic;
                W KillSteal Logic;
            */
            if (Variables.W.IsReady() &&
                ((Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewks").GetValue<bool>() && Targets.Target.Health < Variables.W.GetDamage(Targets.Target)) ||
                (Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewimmobile").GetValue<bool>() && Bools.IsImmobile(Targets.Target))))
            {
                Variables.W.CastIfHitchanceEquals(Targets.Target, HitChance.VeryHigh, false);
            }
            
            /*
                R Doublelift Mechanic;
                R KillSteal Logic;
            */
            if (Variables.R.IsReady())
            {
                if (Variables.E.IsReady() &&
                    Targets.Target.IsValidTarget(1200) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.usermechanic").GetValue<bool>() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.rwhitelist.{Targets.Target.ChampionName.ToLower()}").GetValue<bool>())
                {
                    Variables.E.Cast(Variables.E.GetPrediction(Targets.Target).UnitPosition);
                    Variables.R.Cast(Variables.R.GetPrediction(Targets.Target).UnitPosition);
                    return;
                }

                if (Targets.Target.IsValidTarget(1200) &&
                    (!Variables.W.IsReady() || !Targets.Target.IsValidTarget(Variables.W.Range)) &&
                    (Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userks").GetValue<bool>() && Targets.Target.Health < Variables.R.GetDamage(Targets.Target)))
                {
                    Variables.R.Cast(Variables.R.GetPrediction(Targets.Target).UnitPosition);
                }
            }
        }

        public static void ExecuteModes(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /*
                W Combo Logic;
            */
            if (Variables.W.IsReady() && 
                (Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewcombo").GetValue<bool>() && Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo))
            {
                Variables.W.CastIfHitchanceEquals((Obj_AI_Hero)args.Target, HitChance.VeryHigh, false);
                return;
            }
            
            /*
                R Combo Logic;
            */
            if (Variables.R.IsReady() &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.usercombo").GetValue<bool>() && Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.rwhitelist.{((Obj_AI_Hero)args.Target).ChampionName.ToLower()}").GetValue<bool>())
            {
                Variables.R.Cast(Variables.R.GetPrediction(Targets.Target).UnitPosition);
            }
        }

        public static void ExecuteFarm(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /*
                W Farm Logic;
            */
            if (Variables.W.IsReady() &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewfarm").GetValue<bool>() && Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear) &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                Variables.W.GetLineFarmLocation(Targets.Minions, Variables.W.Width).MinionsHit >= 3)
            {
                Variables.W.Cast(Variables.W.GetLineFarmLocation(Targets.Minions, Variables.W.Width).Position);
            }
        }
    }
}
