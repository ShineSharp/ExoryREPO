namespace ExorAIO.Champions.Olaf
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using ItemData = LeagueSharp.Common.Data.ItemData;

    using ExorAIO.Utilities;

    public class Logics
    {
        public static void ExecuteAuto(EventArgs args)
        {
            /*
                Q Harass/Farm Logic;
                Q KillSteal Logic;
            */
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqautoharass").GetValue<bool>() && 
                    ObjectManager.Player.ManaPercent >= ManaManager.NeededQMana) ||

                (Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqks").GetValue<bool>() &&
                    Variables.Q.GetDamage(Targets.Target) > Targets.Target.Health))
            {
                Variables.Q.CastIfHitchanceEquals(Targets.Target, HitChance.VeryHigh, false);
            }

            /*
                W Combo Logic;
            */
            if (Variables.W.IsReady() &&
                Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(null)) &&
                ObjectManager.Player.IsWindingUp &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewcombo").GetValue<bool>() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo)
            {
                Variables.W.Cast();
            }

            /*
                R LifeSaver Logic;
            */
            if (Variables.R.IsReady() &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.useranticc").GetValue<bool>() && Bools.ShouldCleanse()))
            {
                Variables.R.Cast();
            }
        }

        public static void ExecuteModes(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /*
                Q Combo Logic;
            */
            if (Variables.Q.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.Q.Range) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>())
            {
                Variables.Q.Cast();
                return;
            }

            /*
                E Combo Logic;
            */
            if (Variables.E.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.E.Range) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useecombo").GetValue<bool>())
            {
                Variables.E.Cast();

                if (Variables.Menu.Item($"{Variables.MainMenuName}.miscsettings.useresetters").GetValue<bool>())
                {
                    if (ItemData.Tiamat_Melee_Only.GetItem().IsReady())
                    {
                        ItemData.Tiamat_Melee_Only.GetItem().Cast();
                    }

                    if (ItemData.Ravenous_Hydra_Melee_Only.GetItem().IsReady())
                    {
                        ItemData.Ravenous_Hydra_Melee_Only.GetItem().Cast();
                    }

                    if (ItemData.Titanic_Hydra_Melee_Only.GetItem().IsReady())
                    {
                        ItemData.Titanic_Hydra_Melee_Only.GetItem().Cast();
                    }
                }
            }
        }
    }
}
