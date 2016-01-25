namespace ExorAIO.Champions.Ashe
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
        public static void ExecuteAuto(EventArgs args)
        {
            /// <summary>
            /// The Q Combo Logic,
            /// The Q Farm Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ObjectManager.Player.HasBuff("AsheQCastReady") &&

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").GetValue<bool>()) ||

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.farm").GetValue<bool>())))
            {
                Variables.Q.Cast();
            }

            /// <summary>
            /// The W Immobile Harass Logic,
            /// The W KillSteal Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&
                Variables.W.GetPrediction(Targets.Target).Hitchance >= HitChance.High &&

                ((Targets.Target.Health < Variables.W.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wspell.ks").GetValue<bool>())) ||

                (Bools.IsImmobile(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wspell.harass").GetValue<bool>()))
            {
                Variables.W.Cast(Variables.W.GetPrediction(Targets.Target).UnitPosition);
            }
            
            /// <summary>
            /// The R Logics.
            /// </summary>
            if (Variables.R.IsReady() &&
                Targets.Target.IsValidTarget(Variables.W.Range+200) &&
                !Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)))
            {
                /// <summary>
                /// R KillSteal Logic.
                /// </summary>
                if ((!Variables.W.IsReady() ||
                    !Targets.Target.IsValidTarget(Variables.W.Range)) &&
                    Targets.Target.Health < Variables.R.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.ks").GetValue<bool>())
                {
                    Variables.R.Cast(Variables.R.GetPrediction(Targets.Target).UnitPosition);
                }

                /// <summary>
                /// The R Doublelift Mechanic Logic,
                /// The R Normal Combo Logic.
                /// </summary>
                if ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.combo").GetValue<bool>() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.whitelist.{Targets.Target.ChampionName.ToLower()}").GetValue<bool>()))
                {
                    if (Variables.E.IsReady() &&
                        Variables.Menu.Item($"{Variables.MainMenuName}.misc.ermechanic").GetValue<bool>())
                    {
                        Variables.E.Cast(Variables.E.GetPrediction(Targets.Target).UnitPosition);
                    }

                    Variables.R.Cast(Variables.R.GetPrediction(Targets.Target).UnitPosition);
                }
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
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Variables.W.GetPrediction(Targets.Target).Hitchance >= HitChance.Medium &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.combo").GetValue<bool>())
            {
                Variables.W.Cast(Variables.W.GetPrediction(Targets.Target).UnitPosition);
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
            /// The W Farm Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                (Variables.W.GetLineFarmLocation(Targets.Minions, Variables.W.Width).MinionsHit >= 3 ||
                    GameObjects.Jungle.Contains((Obj_AI_Minion)Variables.Orbwalker.GetTarget())) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.farm").GetValue<bool>())
            {
                Variables.W.Cast(Variables.W.GetLineFarmLocation(Targets.Minions, Variables.W.Width).Position);
            }
        }
    }
}
