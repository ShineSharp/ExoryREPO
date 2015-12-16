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
            /// The Q Harass/Farm Logic.
            /// The Q KillSteal.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo ||
                    (ObjectManager.Player.ManaPercent >= ManaManager.NeededQMana &&
                        Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqautoharass").GetValue<bool>())) ||

                (Variables.Q.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqks").GetValue<bool>())))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
            }

            /// <summary>
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(null)) &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewcombo").GetValue<bool>()))
            {
                Variables.W.Cast();
            }

            /// <summary>
            /// The R Anti-HardCC Logic.
            /// </summary>
            if (Variables.R.IsReady() &&

                (Bools.ShouldCleanse() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.useranticc").GetValue<bool>() && Bools.ShouldCleanse()))
            {
                Variables.R.Cast();
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
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.Q.Range) &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>()))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
                return;
            }

            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.E.Range) &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>()))
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

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    (Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).MinionsHit >= 3 ||
                        Targets.Minions.FirstOrDefault().CharData.BaseSkinName.Contains("SRU_") ||
                        Targets.Minions.FirstOrDefault().CharData.BaseSkinName.Contains("Mini")) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>()))
            {
                Variables.Q.Cast(Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).Position);
            }
        }
    }
}
