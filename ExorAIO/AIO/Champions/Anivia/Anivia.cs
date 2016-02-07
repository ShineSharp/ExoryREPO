using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Anivia
{
    using System;
    using ExorAIO.Utilities;
    using Orbwalking = SFXTargetSelector.Orbwalking;

    /// <summary>
    /// The champion class.
    /// </summary>
    class Anivia
    {
        public static GameObject QMissile;
        public static GameObject RMissile;

        /// <summary>
        /// Called when the game loads itself.
        /// </summary>
        public void OnLoad()
        {
            Menus.Initialize();
            Spells.Initialize();
            Methods.Initialize();
            Drawings.Initialize();
        }

        /// <summary>
        /// Called when an object gets created by the game.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void OnCreate(GameObject obj, EventArgs args)
        {
            if (obj.IsValid)
            {
                if (obj.Name.Equals("cryo_FlashFrost_Player_mis.troy"))
                {
                    QMissile = obj;
                }
                
                if (obj.Name.Contains("cryo_storm"))
                {
                    RMissile = obj;
                }
            }
        }

        /// <summary>
        /// Called when an object gets deleted by the game.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void OnDelete(GameObject obj, EventArgs args)
        {
            if (obj.IsValid)
            {
                if (obj.Name.Equals("cryo_FlashFrost_Player_mis.troy"))
                {
                    QMissile = null;
                }
                
                if (obj.Name.Contains("cryo_storm"))
                {
                    RMissile = null;
                }
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void OnUpdate(EventArgs args)
        {
            if (!ObjectManager.Player.IsDead)
            {
                Logics.ExecuteManager(args);

                if (Targets.Target != null &&
                    Targets.Target.IsValid &&
                    !ObjectManager.Player.IsDead &&
                    Bools.HasNoProtection(Targets.Target) &&
                    Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.None)
                {
                    Logics.ExecuteAuto(args);
                }

                if ((Obj_AI_Minion)Variables.Orbwalker.GetTarget() != null &&
                    ((Obj_AI_Minion)Variables.Orbwalker.GetTarget()).IsValid)
                {
                    Logics.ExecuteFarm(args);
                }
            }
        }

        /// <summary>
        /// Called on interruptable spell.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Interrupter2.InterruptableTargetEventArgs"/> instance containing the event data.</param>
        public static void OnInterruptableTarget(Obj_AI_Hero sender, Interrupter2.InterruptableTargetEventArgs args)
        {
            if (Variables.W.IsReady() &&
                ObjectManager.Player.Distance(sender) < Variables.W.Range + sender.BoundingRadius &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.ir").GetValue<bool>())
            {
                Variables.W.Cast(ObjectManager.Player.ServerPosition.Extend(Targets.Target.ServerPosition, ObjectManager.Player.Distance(Targets.Target) + Targets.Target.BoundingRadius));
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
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.gp").GetValue<bool>())
            {
                Variables.W.Cast(gapcloser.End);
            }
        }
    }
}
