using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Olaf
{
    using System;
    using ExorAIO.Utilities;
    using Orbwalking = SFXTargetSelector.Orbwalking;

    /// <summary>
    /// The champion class.
    /// </summary>
    class Olaf
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
                if (Targets.Target != null &&
                    Targets.Target.IsValid &&
                    Bools.HasNoProtection(Targets.Target) &&
                    Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.None)
                {
                    Logics.ExecuteAuto(args);
                }

                if (Variables.Orbwalker.GetTarget() != null &&
                    Variables.Orbwalker.GetTarget().IsValid)
                {
                    Logics.ExecuteFarm(args);
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
            }
        }
    }
}
