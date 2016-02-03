using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Jinx
{
    using System;
    using ExorAIO.Utilities;
    using Orbwalking = SFXTargetSelector.Orbwalking;

    /// <summary>
    /// The main class.
    /// </summary>
    class Jinx
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

            if (Targets.Target != null &&
                Targets.Target.IsValid &&
                !ObjectManager.Player.IsDead)
            {
                Logics.ExecuteAuto(args);
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
                args.Target.IsValid<Obj_AI_Hero>() &&
                Orbwalking.IsAutoAttack(args.SData.Name) &&
                Bools.HasNoProtection((Obj_AI_Hero)args.Target) &&
                Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.None)
            {
                Logics.ExecuteModes(sender, args);
            }
        }

        /// <summary>
        /// Called when a cast gets executed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender != null &&
                sender.IsEnemy &&
                sender.IsValid<Obj_AI_Hero>() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.auto").GetValue<bool>())
            {
                Logics.ExecuteSpellCast(sender, args);
            }
        }

        /// <summary>
        /// The E on Gapcloser Logic.
        /// </summary>
        public static void OnGapcloser(ActiveGapcloser gapcloser)
        {
            if (Variables.E.IsReady() &&
                ObjectManager.Player.Distance(gapcloser.End) <= Variables.E.Range &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.auto").GetValue<bool>())
            {
                Variables.E.Cast(gapcloser.End);
            }
        }
    }
}
