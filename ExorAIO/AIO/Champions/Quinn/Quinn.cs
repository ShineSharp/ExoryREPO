using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Quinn
{
    using System;
    using ExorAIO.Utilities;
    using Orbwalking = SFXTargetSelector.Orbwalking;

    /// <summary>
    /// The champion class.
    /// </summary>
    class Quinn
    {
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
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void OnUpdate(EventArgs args)
        {
            if (!ObjectManager.Player.IsDead)
            {
                Variables.Orbwalker.SetAttack(!Variables.R.Instance.Name.Equals("quinnrfinale"));

                /// <summary>
                /// The R Logic.
                /// </summary>
                if (Variables.R.IsReady() &&
                    ObjectManager.Player.InFountain() &&
                    Variables.R.Instance.Name.Equals("QuinnR"))
                {
                    Variables.R.Cast();
                }

                if (Targets.Target != null &&
                    Targets.Target.IsValid &&
                    Bools.HasNoProtection(Targets.Target) &&
                    Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.None)
                {
                    if ((Targets.Target.IsValidTarget(500f) ||
                        Targets.Target.CountEnemiesInRange(1000f) > 1) &&
                        Variables.R.Instance.Name.Equals("quinnrfinale"))
                    {
                        Variables.R.Cast();
                    }
                    
                    Logics.ExecuteAuto(args);
                }
            }
        }

        /// <summary>
        /// Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void OnDoCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe &&
                Orbwalking.IsAutoAttack(args.SData.Name) &&
                Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.None)
            {
                if (args.Target.IsValid<Obj_AI_Hero>() &&
                    Bools.HasNoProtection((Obj_AI_Hero)args.Target))
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
        /// Called on interruptable spell.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Interrupter2.InterruptableTargetEventArgs"/> instance containing the event data.</param>
        public static void OnInterruptableTarget(Obj_AI_Hero sender, Interrupter2.InterruptableTargetEventArgs args)
        {
            if (Variables.E.IsReady() &&
                sender.IsValidTarget(Variables.E.Range) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.ir").GetValue<bool>())
            {
                Variables.E.CastOnUnit(sender);
            }
        }

        /// <summary>
        /// Called on gapclosing spell.
        /// </summary>
        /// <param name="gapcloser">The <see cref="ActiveGapcloser"/> instance containing the event data.</param>
        public static void OnEnemyGapcloser(ActiveGapcloser gapcloser)
        {
            if (Variables.E.IsReady() &&
                gapcloser.Sender.IsValidTarget(Variables.E.Range) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.gp").GetValue<bool>())
            {
                Variables.E.CastOnUnit(gapcloser.Sender);
            }
        }
    }
}
