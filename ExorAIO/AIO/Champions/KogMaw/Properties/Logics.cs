using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.KogMaw
{
    using System;
    using System.Linq;
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
            /// The Q KillSteal Logic,
            /// The Q Immobile Harass Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&
                Variables.Q.GetPrediction(Targets.Target).Hitchance >= HitChance.High &&

                ((Targets.Target.Health < Variables.Q.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").GetValue<bool>()) ||

                (Bools.IsImmobile(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.immobile").GetValue<bool>())))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
            }

            /// <summary>
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.combo").GetValue<bool>())
            {
                Variables.W.Cast();
            }

            /// <summary>
            /// The E KillSteal Logic,
            /// The E Immobile Harass Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Targets.Target.IsValidTarget(Variables.E.Range) &&

                ((Targets.Target.Health < Variables.E.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.espell.ks").GetValue<bool>()) ||

                (Bools.IsImmobile(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.espell.immobile").GetValue<bool>())))
            {
                Variables.E.Cast(Variables.E.GetPrediction(Targets.Target).UnitPosition);
            }

            /// <summary>
            /// The R Combo Logic,
            /// The R KillSteal Logic,
            /// The R Farm Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Targets.Target.HealthPercent < 50 &&
                !ObjectManager.Player.IsWindingUp &&

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.combo").GetValue<bool>() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.stacks").GetValue<Slider>().Value >=
                        ObjectManager.Player.GetBuffCount("kogmawlivingartillerycost")) ||

                (Targets.Target.Health < Variables.R.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.ks").GetValue<bool>())))
            {
                Variables.R.Cast(Variables.R.GetPrediction(Targets.Target).CastPosition);
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
                Targets.Target.IsValidTarget(Variables.Q.Range) &&
                Variables.Q.GetPrediction(Targets.Target).Hitchance >= HitChance.High &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").GetValue<bool>())
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
                return;
            }

            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").GetValue<bool>())
            {
                Variables.E.Cast(Variables.E.GetPrediction(Targets.Target).UnitPosition);
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
            /// The E Farm Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededEMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                (Variables.E.GetLineFarmLocation(Targets.Minions, Variables.E.Width).MinionsHit >= 3 ||
                    GameObjects.Jungle.Contains((Obj_AI_Minion)args.Target)) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.farm").GetValue<bool>())
            {
                Variables.E.Cast(((Obj_AI_Minion)args.Target).Position);
            }
        }
    }    
}
