namespace ExorAIO.Champions.Lux
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using SharpDX;

    using ExorAIO.Utilities;

    using Orbwalking = SFXTargetSelector.Orbwalking;
    using TargetSelector = SFXTargetSelector.TargetSelector;

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
            /// The Target preference.
            /// </summary>
            if (TargetSelector.Weights.GetItem("low-health") != null)
            {
                TargetSelector.Weights.Register(
                    new TargetSelector.Weights.Item(
                        "p-stack", "Passive Stack", 10, false, hero => hero.HasBuff("luxilluminatingfraulein") ? 1 : 0,
                        "Has Passive Stack = Higher Weight"));
            }

            /// <summary>
            /// The Automatic E Management.
            /// </summary>
            if (Variables.E.IsReady() &&
                !Variables.R.IsReady() &&
                Targets.Target.IsValidTarget(Variables.E.Range) &&
                Bools.CanUseE() &&
                Targets.Target.HasBuffOfType(BuffType.Slow) &&
                (!Targets.Target.HasBuff("luxilluminatingfraulein") || Targets.Target.Health < Variables.E.GetDamage(Targets.Target)))
            {
                Variables.E.Cast();
            }

            /// <summary>
            /// The R KillSteal Logic,
            /// The R Combo Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                !Variables.E.IsReady() &&
                !Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.R.Range) &&
                !(Targets.Target.Health < Variables.E.GetDamage(Targets.Target) && Bools.CanUseE()) &&

                ((Targets.Target.Health < Variables.R.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userks").GetValue<bool>()) ||

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Bools.IsImmobile(Targets.Target) &&
                    Targets.Target.HasBuff("luxilluminatingfraulein") &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.usercombo").GetValue<bool>())))
            {
                Variables.R.Cast(Variables.R.GetPrediction(Targets.Target).UnitPosition);
                return;
            }

            /// <summary>
            /// The Q KillSteal Logic,
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&

                ((Targets.Target.Health < Variables.Q.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqks").GetValue<bool>()) ||

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>())))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
                return;
            }

            /// <summary>
            /// The Smart W Logic.
            /// </summary>
            if (Variables.W.IsReady() &&

                (HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250f + Game.Ping / 2f)) <= ObjectManager.Player.Health - 200 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usesmartw").GetValue<bool>()))
            {
                Variables.W.Cast(Game.CursorPos);
            }

            /// <summary>
            /// The E KillSteal Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                !Bools.CanUseE() &&
                !Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.E.Range) &&
            
                (Targets.Target.Health < Variables.E.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeks").GetValue<bool>()))
            {
                Variables.E.Cast(Variables.E.GetPrediction(Targets.Target).CastPosition);
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
            /// The Q Combo Logic Pt.2.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&

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
                Targets.Target.IsValidTarget(Variables.E.Range) &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useecombo").GetValue<bool>()))
            {
                Variables.E.Cast(Variables.E.GetPrediction(Targets.Target).CastPosition);
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

                (ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                    (Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).MinionsHit == 2 ||
                        ((Obj_AI_Minion)Variables.Orbwalker.GetTarget()).CharData.BaseSkinName.Contains("SRU_") ||
                        ((Obj_AI_Minion)Variables.Orbwalker.GetTarget()).CharData.BaseSkinName.Contains("Mini")) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>()))
            {
                Variables.Q.Cast(((Obj_AI_Minion)Variables.Orbwalker.GetTarget()).Position);
            }

            /// <summary>
            /// The E Farm Logic.
            /// </summary>
            if (Variables.E.IsReady() &&

                (ObjectManager.Player.ManaPercent > ManaManager.NeededEMana &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                    (Variables.E.GetCircularFarmLocation(Targets.Minions, Variables.E.Width).MinionsHit >= 2 ||
                        ((Obj_AI_Minion)Variables.Orbwalker.GetTarget()).CharData.BaseSkinName.Contains("SRU_") ||
                        ((Obj_AI_Minion)Variables.Orbwalker.GetTarget()).CharData.BaseSkinName.Contains("Mini")) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useefarm").GetValue<bool>()))
            {
                Variables.E.Cast(Variables.E.GetCircularFarmLocation(Targets.Minions, Variables.E.Width).Position);
            }
        }
    }
}
