namespace ExorAIO.Champions.Cassiopeia
{
    using System;
    using System.Linq;
    using LeagueSharp;
    using LeagueSharp.Common;
    using ExorAIO.Utilities;
    using Orbwalking = SFXTargetSelector.Orbwalking;

    /// <summary>
    /// The logics class.
    /// </summary>
    class Logics
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
                ObjectManager.Player.CountEnemiesInRange(1500) == 0 &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.None &&
                Variables.Menu.Item($"{Variables.MainMenuName}.misc.stacktear").GetValue<bool>() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededTearMana)
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
            if (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                !Variables.Menu.Item($"{Variables.MainMenuName}.misc.aacombo").GetValue<bool>())
            {
                Variables.Orbwalker.SetAttack(false);
            }

            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ((Obj_AI_Hero)Variables.Orbwalker.GetTarget()).HasBuffOfType(BuffType.Poison) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").GetValue<bool>())
            {
                Utility.DelayAction.Add(Variables.Menu.Item($"{Variables.MainMenuName}.espell.edelay").GetValue<Slider>().Value,
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
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.combo").GetValue<bool>() &&
                Targets.RTargets.Count() >= Variables.Menu.Item($"{Variables.MainMenuName}.rspell.enemies").GetValue<Slider>().Value)
            {
                Variables.R.Cast((Targets.RTargets.FirstOrDefault()).Position);
            }

            /// <summary>
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ((Obj_AI_Hero)Variables.Orbwalker.GetTarget()).IsValidTarget(Variables.Q.Range) &&
                !((Obj_AI_Hero)Variables.Orbwalker.GetTarget()).HasBuffOfType(BuffType.Poison) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").GetValue<bool>())
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
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.combo").GetValue<bool>())
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
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                (Variables.Q.GetCircularFarmLocation(Targets.Minions, Variables.Q.Width).MinionsHit >= 3 ||
                    GameObjects.Jungle.Contains((Obj_AI_Minion)Variables.Orbwalker.GetTarget())) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>())
            {
                Variables.Q.Cast(Variables.Q.GetCircularFarmLocation(Targets.Minions, Variables.Q.Width).Position);
            }

            /// <summary>
            /// The W Farm Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                !Variables.Q.IsReady() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                (Variables.W.GetCircularFarmLocation(Targets.Minions, Variables.W.Width).MinionsHit >= 3 ||
                    GameObjects.Jungle.Contains((Obj_AI_Minion)Variables.Orbwalker.GetTarget())) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewfarm").GetValue<bool>())
            {
                Variables.W.Cast(Variables.W.GetCircularFarmLocation(Targets.Minions, Variables.W.Width).Position);
            }

            /// <summary>
            /// The E Farm Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                !Variables.Q.IsReady() &&
                !Variables.W.IsReady() &&
                (((Obj_AI_Minion)Variables.Orbwalker.GetTarget()).HasBuffOfType(BuffType.Poison) || 
                    Variables.Menu.Item($"{Variables.MainMenuName}.misc.lasthitnopoison").GetValue<bool>()) &&

                (Variables.E.GetDamage(((Obj_AI_Minion)Variables.Orbwalker.GetTarget())) > ((Obj_AI_Minion)Variables.Orbwalker.GetTarget()).Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.espell.farm").GetValue<bool>()))
            {
                Utility.DelayAction.Add(Variables.Menu.Item($"{Variables.MainMenuName}.espell.delay").GetValue<Slider>().Value, 
                () =>
                    {
                        Variables.E.CastOnUnit(((Obj_AI_Minion)Variables.Orbwalker.GetTarget()));
                    }
                );
            }
        }
    }
}
