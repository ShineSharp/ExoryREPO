using LeagueSharp;
using LeagueSharp.Common;

namespace ExorRyze
{
    using System;
    using Orbwalking = SFXTargetSelector.Orbwalking;

    /// <summary>
    /// The main class.
    /// </summary>
    class Ryze
    {
        /// <summary>
        /// Triggers when the champion is loaded.
        /// </summary>
        public static void OnLoad()
        {
            Menus.Initialize();
            Spells.Initialize();
            Methods.Initialize();
            Drawings.Initialize();
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void OnUpdate(EventArgs args)
        {
            if (!ObjectManager.Player.IsDead)
            {
                if (!ObjectManager.Player.IsRecalling() &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.None)
                {
                    Logics.ExecuteStacking(args);
                    Logics.ExecuteTearStacking(args);
                }
                
                if (Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.None)
                {
                    if (Targets.Target != null &&
                        Targets.Target.IsValid &&
                        !Targets.Target.IsInvulnerable &&
                        !Bools.IsSpellShielded(Targets.Target))
                    {
                        Logics.ExecuteAuto(args);
                    }

                    if (Variables.Orbwalker.GetTarget() != null &&
                        Variables.Orbwalker.GetTarget().IsValid<Obj_AI_Minion>())
                    {
                        Logics.ExecuteFarm(args);
                    }
                }
            }
        }

        /// <summary>
        /// Called on gapclosing spell.
        /// </summary>
        /// <param name="gapcloser">The <see cref="ActiveGapcloser"/> instance containing the event data.</param>
        public static void OnEnemyGapcloser(ActiveGapcloser gapcloser)
        {
            if (Variables.W.IsReady() &&
                gapcloser.Sender.IsValidTarget(Variables.W.Range) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.gp").IsActive())
            {
                Variables.W.CastOnUnit(gapcloser.Sender);
            }
        }
    }
}
