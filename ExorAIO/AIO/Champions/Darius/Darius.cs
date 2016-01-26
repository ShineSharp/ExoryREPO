using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Darius
{
    using System;
    using ExorAIO.Utilities;
    using Orbwalking = SFXTargetSelector.Orbwalking;

    /// <summary>
    /// The main class.
    /// </summary>
    class Darius
    {
        /// <summary>
        /// Triggers when the champion is loaded.
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
            if (Targets.Target != null &&
				Targets.Target.IsValid &&
                !ObjectManager.Player.IsDead &&
				Bools.HasNoProtection(Targets.Target))
            {
                Logics.ExecuteAuto(args);
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
			    Bools.HasNoProtection((Obj_AI_Hero)args.Target) &&
                Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.None)
            {
				if (args.Target.Type.Equals(GameObjectType.obj_AI_Hero))
                {
					Logics.ExecuteModes(sender, args);
                }
                else if (args.Target.Type.Equals(GameObjectType.obj_AI_Minion))
                {
					Logics.ExecuteFarm(sender, args);
                }
            }
        }
    }
}
