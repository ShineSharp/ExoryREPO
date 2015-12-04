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
            /*
                Q Immobile Harass Logic;
                Q KillSteal Logic;
            */
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(1000) &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqks").GetValue<bool>() && Targets.Target.Health < Variables.Q.GetDamage(Targets.Target)) ||
                (Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqimmobile").GetValue<bool>() && Bools.IsImmobile(Targets.Target)) ||
                (Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqautoharass").GetValue<bool>() && ObjectManager.Player.ManaPercent > ManaManager.NeededQMana))
            {
                Variables.Q.CastIfHitchanceEquals(Targets.Target, HitChance.VeryHigh, false);
            }

            /*
                W Immobile Harass Logic;
                W KillSteal Logic;
            */
            if (Variables.W.IsReady() &&
                !Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(900) &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewks").GetValue<bool>() && Targets.Target.Health < Variables.W.GetDamage(Targets.Target)) ||
                (Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewimmobile").GetValue<bool>() && Bools.IsImmobile(Targets.Target)) ||
                (Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewautoharass").GetValue<bool>() && ObjectManager.Player.ManaPercent > ManaManager.NeededWMana))
            {
                Variables.W.Cast(Targets.Target.Position, false);
            }
            
            /*
                R KillSteal Logic;
            */
            if (Variables.R.IsReady() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userks").GetValue<bool>() &&
                Targets.Target.IsValidTarget(1500) &&
                Targets.Target.Health < Variables.R.GetDamage(Targets.Target) &&
                !Variables.W.IsReady() &&
                (!Variables.Q.IsReady() || !Targets.Target.IsValidTarget(Variables.Q.Range)))
            {
                Variables.R.CastIfHitchanceEquals(Targets.Target, HitChance.VeryHigh, false);
            }

        }

        public static void ExecuteModes(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /*
                Q Combo Logic;
            */
            if (Variables.Q.IsReady() && 
                Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>())
            {
                Variables.Q.CastIfHitchanceEquals((Obj_AI_Hero)args.Target, HitChance.VeryHigh, false);
                return;
            }

            /*
                W & E Combo Logic;
            */
            if (Variables.W.IsReady() && 
                Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewcombo").GetValue<bool>())
            {
                Variables.W.CastIfHitchanceEquals((Obj_AI_Hero)args.Target, HitChance.VeryHigh, false);
                
                if (Variables.E.IsReady() && 
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useecombo").GetValue<bool>())
                {
                    Variables.E.Cast(((Obj_AI_Hero)args.Target).Position);
                }
                return;
            }

            /*
                R Combo Logic;
            */
            if (Variables.R.IsReady() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.usercombo").GetValue<bool>() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.rwhitelist.{((Obj_AI_Hero)args.Target).ChampionName.ToLower()}").GetValue<bool>())
            {
                Variables.R.CastIfHitchanceEquals((Obj_AI_Hero)args.Target, HitChance.VeryHigh, false);
            }
        }

        public static void ExecuteFarm(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /*
                Q Farm Logic;
            */
            if (Variables.Q.IsReady() &&
                Targets.FarmMinion != null &&
                Targets.FarmMinion.IsValid &&
                !ObjectManager.Player.IsWindingUp &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededQMana)
            {
                Variables.Q.Cast(Targets.FarmMinion.Position);
            }
        }
    }
}
