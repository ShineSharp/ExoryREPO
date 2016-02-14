using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Caitlyn
{
    using System;
    using ExorAIO.Utilities;
    using Orbwalking = SFXTargetSelector.Orbwalking;

    /// <summary>
    /// The main class.
    /// </summary>
    class Caitlyn
    {
        /// <summary>
        /// Triggers when the champion is loaded.
        /// </summary>
        public void OnLoad()
        {
            Menus.Initialize();
            Methods.Initialize();
            Drawings.Initialize();
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void OnUpdate(EventArgs args)
        {
            Spells.Initialize();

            if (!ObjectManager.Player.IsDead &&
                Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.None)
            {
                Logics.ExecuteR(args);

                if (Targets.Target != null &&
                    Targets.Target.IsValid &&
                    !Targets.Target.IsInvulnerable &&
                    !Bools.IsSpellShielded(Targets.Target))
                {
                    Logics.ExecuteAuto(args);
                }
            }
        }

        /// <summary>
        /// Called when a cast gets executed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void OnDoCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe &&
                Orbwalking.IsAutoAttack(args.SData.Name))
            {
                if (args.Target.IsValid<Obj_AI_Hero>() &&
                    !((Obj_AI_Hero)args.Target).IsInvulnerable &&
                    !Bools.IsSpellShielded(((Obj_AI_Hero)args.Target)))
                {
                    Logics.ExecuteModes(sender, args);
                }
                else if (args.Target.IsValid<Obj_AI_Minion>())
                {
                    Logics.ExecuteFarm(sender, args);
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
                ObjectManager.Player.Distance(gapcloser.End) < Variables.W.Range &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.gp").IsActive())
            {
                Variables.W.Cast(gapcloser.End);
            }

            if (Variables.E.IsReady() &&
                ObjectManager.Player.Distance(gapcloser.End) < 300f &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.gp").IsActive())
            {
                Variables.E.Cast(gapcloser.Sender.Position);
            }
        }
    }
}
