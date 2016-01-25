namespace ExorAIO.Champions.DrMundo
{
    using System;
    using LeagueSharp;
    using LeagueSharp.Common;
    using ExorAIO.Utilities;
    using Orbwalking = SFXTargetSelector.Orbwalking;

    /// <summary>
    /// The champion class.
    /// </summary>
    class DrMundo
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
            if (!ObjectManager.Player.IsDead)
			{
				if (Targets.Target != null &&
					Targets.Target.IsValid &&
					Bools.HasNoProtection(Targets.Target))
				{
					Logics.ExecuteAuto(args);
				}

                Logics.ExecuteFarm(args);
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
				Bools.HasNoProtection((Obj_AI_Hero)args.Target) &&
                Orbwalking.IsAutoAttack(args.SData.Name) &&
                Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.None)
            {
				switch (args.Target.Type)
				{
					case GameObjectType.obj_AI_Hero: 
						Logics.ExecuteModes(sender, args);
						break;

					default: 
						break;
				}
            }
        }
    }
}
