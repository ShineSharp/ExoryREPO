using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Graves
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
            /// The Q Immobile Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&

                ((Variables.Q.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").IsActive()) ||

                (Bools.IsImmobile(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.immobile").IsActive())))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
                return;
            }

            /// <summary>
            /// The W KillSteal Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&
                Variables.W.GetDamage(Targets.Target) > Targets.Target.Health &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.ks").IsActive())
            {
                Variables.W.Cast(Variables.W.GetPrediction(Targets.Target).CastPosition);
                return;
            }

            /// <summary>
            /// The R KillSteal Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Targets.Target.IsValidTarget(Variables.R.Range) &&
                Variables.R.GetDamage(Targets.Target) > Targets.Target.Health &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.ks").IsActive())
            {
                Variables.R.Cast(Variables.R.GetPrediction(Targets.Target).UnitPosition);
            }
        }

        ///<summary>
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
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").IsActive())
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
                return;
            }   

            /// <summary>
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.W.Range) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.combo").IsActive())
            {
                Variables.W.Cast(Variables.W.GetPrediction(Targets.Target).CastPosition);
                return;
            }

            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.auto").IsActive())
            {
                Variables.E.Cast(Game.CursorPos);
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteFarm(EventArgs args)
        {
            /// <summary>
            /// The Q LaneClear Logic,
            /// The Q JungleClear Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                (Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).MinionsHit >= 3 || Targets.JungleMinions.Any()) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.farm").IsActive())
            {
                if (Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).MinionsHit >= 3)
                {
                    Variables.Q.Cast(Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).Position);
                }
                else if (Targets.JungleMinions.Any())
                {
                    Variables.Q.Cast((Targets.JungleMinions.FirstOrDefault()).Position);
                }
            }
        }
    }
}
