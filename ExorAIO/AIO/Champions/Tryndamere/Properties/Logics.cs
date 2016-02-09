using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Tryndamere
{
    using System;
    using System.Linq;
    using ExorAIO.Utilities;
    using Orbwalking = SFXTargetSelector.Orbwalking;
    using SPrediction;

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
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.auto").IsActive())
            {
                Variables.W.Cast();
            }

            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                !Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)) &&
                (!Targets.Target.UnderTurret() || Variables.E.GetDamage(Targets.Target) > Targets.Target.Health) &&
                ObjectManager.Player.Distance(Variables.E.GetSPrediction(Targets.Target).CastPosition.To3D()) < Variables.E.Range - 50 &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").IsActive())
            {
                Variables.E.Cast(Variables.E.GetSPrediction(Targets.Target).CastPosition.To3D());
            }

            /// <summary>
            /// The Q Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                !Variables.E.IsReady() &&
                ObjectManager.Player.ManaPercent >= 50 &&
                !ObjectManager.Player.IsFacing(Targets.Target) &&
                HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/2 &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.auto").IsActive())
            {
                Variables.Q.Cast();
                return;
            }

            /// <summary>
            /// The R Lifesaver Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                !Variables.Q.IsReady() &&
                ObjectManager.Player.CountEnemiesInRange(700f) > 0 &&
                HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(250 + Game.Ping / 2f)) <= ObjectManager.Player.MaxHealth/4 &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.lifesaver").IsActive())
            {
                Variables.R.Cast();
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
            /// The E LaneClear Logic,
            /// The E JungleClear Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ObjectManager.Player.CountEnemiesInRange(800f) == 0 &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.E.GetLineFarmLocation(Targets.Minions, Variables.E.Width).MinionsHit >= 3 &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.farm").IsActive())
            {
                if (Variables.E.GetLineFarmLocation(Targets.Minions, Variables.E.Width).MinionsHit >= 3)
                {
                    Variables.E.Cast(Variables.E.GetLineFarmLocation(Targets.Minions, Variables.E.Width).Position);
                }
                else if (Targets.JungleMinions.Any())
                {
                    Variables.E.Cast((Targets.JungleMinions.FirstOrDefault()).Position);
                }
            }
        }
    }
}
