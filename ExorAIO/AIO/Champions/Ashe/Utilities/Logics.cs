namespace ExorAIO.Champions.Ashe
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;
    
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
            /// The Q Combo Logic,
            /// The Q Farm Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ObjectManager.Player.HasBuff("AsheQCastReady") &&

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>()) ||

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>())))
            {
                Variables.Q.Cast();
            }

            /// <summary>
            /// The W Immobile Harass Logic,
            /// The W KillSteal Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&
                Bools.HasNoProtection(Targets.Target) &&

                ((Targets.Target.Health < Variables.W.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewks").GetValue<bool>())) ||

                (Bools.IsImmobile(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewimmobile").GetValue<bool>()))
            {
                Variables.W.CastIfHitchanceEquals(Targets.Target, HitChance.High, false);
            }
            
            /// <summary>
            /// The R Logics.
            /// </summary>
            if (Targets.Target.IsValidTarget(Variables.W.Range+200) &&
                Bools.HasNoProtection(Targets.Target))
            {
                /// <summary>
                /// R KillSteal Logic.
                /// </summary>
                if (Variables.R.IsReady() &&
                    (!Variables.W.IsReady() || !Targets.Target.IsValidTarget(Variables.W.Range)) &&

                    (Targets.Target.Health < Variables.R.GetDamage(Targets.Target) &&
                        Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userks").GetValue<bool>()))
                {
                    Variables.R.Cast(Variables.R.GetPrediction(Targets.Target).UnitPosition);
                }

                /// <summary>
                /// The R Doublelift Mechanic Logic,
                /// The R Normal Combo Logic.
                /// </summary>
                if (Variables.R.IsReady() &&
                    !Targets.Target.IsValidTarget(ObjectManager.Player.AttackRange) &&

                    (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                        Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.usercombo").GetValue<bool>() &&
                        Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.rwhitelist.{Targets.Target.ChampionName.ToLower()}").GetValue<bool>()))
                {
                    if (Variables.E.IsReady() &&
                        Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.usermechanic").GetValue<bool>())
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
                Bools.HasNoProtection((Obj_AI_Hero)args.Target) &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewcombo").GetValue<bool>()))
            {
                Variables.W.CastIfHitchanceEquals((Obj_AI_Hero)args.Target, HitChance.High, false);
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

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                    (Variables.W.GetLineFarmLocation(Targets.Minions, Variables.W.Width).MinionsHit >= 3 ||
                        GameObjects.Jungle.Contains((Obj_AI_Minion)Variables.Orbwalker.GetTarget())) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewfarm").GetValue<bool>()))
            {
                Variables.W.Cast(Variables.W.GetLineFarmLocation(Targets.Minions, Variables.W.Width).Position);
            }
        }
    }
}
