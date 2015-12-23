namespace ExorAIO.Champions.Cassiopeia
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using ExorAIO.Utilities;

    using Orbwalking = SFXTargetSelector.Orbwalking;

    /// <summary>
    /// The logics class.
    /// </summary>
    public class Logics
    {
        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteTearStacking(EventArgs args)
        {
            /// <summary>
            /// The Tear Stacking Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Bools.HasTear(ObjectManager.Player) &&
                !ObjectManager.Player.IsRecalling() &&
                ObjectManager.Player.CountEnemiesInRange(1500).Equals(0) &&

                (Variables.Orbwalker.ActiveMode.Equals(Orbwalking.OrbwalkingMode.None) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.misc.stacktear").GetValue<bool>() &&
                    ObjectManager.Player.ManaPercent > Variables.Menu.Item($"{Variables.MainMenuName}.misc.stacktearmana").GetValue<Slider>().Value))
            {
                Variables.Q.Cast(Game.CursorPos);
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteModes(EventArgs args)
        {
            /// <summary>
            /// The No AA while in Combo option.
            /// </summary>
            Variables.Orbwalker.SetAttack(
                Variables.Orbwalker.ActiveMode.Equals(Orbwalking.OrbwalkingMode.Combo) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.misc.aa").GetValue<bool>());

            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ((Obj_AI_Hero)Variables.Orbwalker.GetTarget()).HasBuffOfType(BuffType.Poison) &&

                (Variables.Orbwalker.ActiveMode.Equals(Orbwalking.OrbwalkingMode.Combo) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useecombo").GetValue<bool>()))
            {
                Utility.DelayAction.Add(Variables.Menu.Item($"{Variables.MainMenuName}.esettings.edelay").GetValue<Slider>().Value,
                () =>
                    {
                        Variables.E.CastOnUnit((Obj_AI_Hero)Variables.Orbwalker.GetTarget());
                    }
                ); 
            }

            /// <summary>
            /// The R Combo Logic.
            /// </summary>
            if (Variables.R.IsReady() &&

                (Variables.Orbwalker.ActiveMode.Equals(Orbwalking.OrbwalkingMode.Combo) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.usercombo").GetValue<bool>() &&
                    Targets.RTargets.Count() >= Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userminenemies").GetValue<Slider>().Value))
            {
                Variables.R.Cast(Targets.RTargets.FirstOrDefault().Position);
            }

            /// <summary>
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ((Obj_AI_Hero)Variables.Orbwalker.GetTarget()).IsValidTarget(Variables.Q.Range) &&
                !((Obj_AI_Hero)Variables.Orbwalker.GetTarget()).HasBuffOfType(BuffType.Poison) &&

                (Variables.Orbwalker.ActiveMode.Equals(Orbwalking.OrbwalkingMode.Combo) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>()))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction((Obj_AI_Hero)Variables.Orbwalker.GetTarget()).CastPosition);
                return;
            }

            /// <summary>
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                !Variables.Q.IsReady() &&
                ((Obj_AI_Hero)Variables.Orbwalker.GetTarget()).IsValidTarget(Variables.W.Range) &&
                !((Obj_AI_Hero)Variables.Orbwalker.GetTarget()).HasBuffOfType(BuffType.Poison) &&

                (Variables.Orbwalker.ActiveMode.Equals(Orbwalking.OrbwalkingMode.Combo) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewcombo").GetValue<bool>()))
            {
                Variables.W.Cast(Variables.W.GetPrediction((Obj_AI_Hero)Variables.Orbwalker.GetTarget()).CastPosition);
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteFarm(EventArgs args)
        {
            /// <summary>
            /// The Q Farm Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&

                (Variables.Orbwalker.ActiveMode.Equals(Orbwalking.OrbwalkingMode.LaneClear) &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    (Variables.Q.GetCircularFarmLocation(Targets.Minions, Variables.Q.Width).MinionsHit >= 3 ||
                        Targets.Minions.FirstOrDefault().CharData.BaseSkinName.Contains("SRU_") ||
                        Targets.Minions.FirstOrDefault().CharData.BaseSkinName.Contains("Mini")) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>()))
            {
                Variables.Q.Cast(Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).Position);
            }

            /// <summary>
            /// The W Farm Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                !Variables.Q.IsReady() &&

                (Variables.Orbwalker.ActiveMode.Equals(Orbwalking.OrbwalkingMode.LaneClear) &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                    (Variables.W.GetCircularFarmLocation(Targets.Minions, Variables.W.Width).MinionsHit >= 3 ||
                        Targets.Minions.FirstOrDefault().CharData.BaseSkinName.Contains("SRU_") ||
                        Targets.Minions.FirstOrDefault().CharData.BaseSkinName.Contains("Mini")) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewfarm").GetValue<bool>()))
            {
                Variables.W.Cast(Variables.W.GetLineFarmLocation(Targets.Minions, Variables.W.Width).Position);
            }

            /// <summary>
            /// The E Farm Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                (Targets.Minions.FirstOrDefault().HasBuffOfType(BuffType.Poison) || Variables.Menu.Item($"{Variables.MainMenuName}.misc.lasthitnopoison").GetValue<bool>()) &&
                
                (Variables.E.GetDamage(Targets.Minions.FirstOrDefault()) > Targets.Minions.FirstOrDefault().Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useefarm").GetValue<bool>()))
            {
                Utility.DelayAction.Add(Variables.Menu.Item($"{Variables.MainMenuName}.esettings.edelay").GetValue<Slider>().Value, 
                () =>
                    {
                        Variables.E.CastOnUnit(Targets.Minions.FirstOrDefault());
                    }
                );
            }
        }
    }
}
