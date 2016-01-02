namespace ExorAIO.Champions.Vayne
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using ExorAIO.Utilities;

    using Orbwalking = SFXTargetSelector.Orbwalking;

    /// <summary>
    /// The main class.
    /// </summary>
    public class Vayne
    {
        /// <summary>
        /// Triggers when the champion is loaded.
        /// </summary>
        public void OnLoad()
        {
            Settings.SetSpells();
            Settings.SetMenu();
            Settings.SetMethods();
            Drawings.Load();
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void Game_OnGameUpdate(EventArgs args)
        {
            if (!ObjectManager.Player.IsDead)
            {
                if (Targets.Target != null &&
                    Targets.Target.IsValid)
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

        /// <summary>
        /// Called on-attack request.
        /// </summary>
        /// <param name="unit">The sender.</param>
        /// <param name="target">The target.</param>
        public static void Orbwalking_OnAttack(AttackableUnit unit, AttackableUnit target)
        {
            if (unit.IsMe &&
                target.IsValid<Obj_AI_Hero>() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.miscsettings.usebetaq").GetValue<bool>())
            {
                Logics.ExecuteBetaModes(unit, target);
            }
        }

        /// <summary>
        /// Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void Obj_AI_Base_OnDoCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe &&
                args.Target.IsValid<Obj_AI_Hero>() &&
                Orbwalking.IsAutoAttack(args.SData.Name) &&
                !Variables.Menu.Item($"{Variables.MainMenuName}.miscsettings.usebetaq").GetValue<bool>())
            {
                Logics.ExecuteModes(sender, args);
            }
        }
    }
}
