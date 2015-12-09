namespace ExorAIO.Champions.Olaf
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using ItemData = LeagueSharp.Common.Data.ItemData;

    using Orbwalking = SFXTargetSelector.Orbwalking;

    using ExorAIO.Utilities;

    public class Logics
    {
        public static void ExecuteAuto(EventArgs args)
        {
            /// <summary>
            /// The Q Harass/Farm Logic.
            /// The Q KillSteal.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqautoharass").GetValue<bool>() && ObjectManager.Player.ManaPercent >= ManaManager.NeededQMana) ||
                (Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqks").GetValue<bool>() && Variables.Q.GetDamage(Targets.Target) > Targets.Target.Health))
            {
                Variables.Q.Cast(Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition));
            }

            /// <summary>
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(null)) &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewcombo").GetValue<bool>() && Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo))
            {
                Variables.W.Cast();
            }

            /// <summary>
            /// The R Anti-HardCC Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.useranticc").GetValue<bool>() && Bools.ShouldCleanse()))
            {
                Variables.R.Cast();
            }
        }

        public static void ExecuteModes(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.Q.Range) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>())
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
                return;
            }

            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.E.Range) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useecombo").GetValue<bool>())
            {
                Variables.E.Cast((Obj_AI_Hero)args.Target);

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

        public static void ExecuteFarm(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// The Q Farm Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).MinionsHit >= 3 &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>() && ObjectManager.Player.ManaPercent > ManaManager.NeededQMana))
            {
                Variables.Q.Cast(Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).Position);
            }
        }
    }
}
