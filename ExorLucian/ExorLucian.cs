using LeagueSharp;
using LeagueSharp.Common;

namespace ExorLucian
{
    using System;
    using Orbwalking = SFXTargetSelector.Orbwalking;

    /// <summary>
    /// The main class.
    /// </summary>
    class Lucian
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
            if (!ObjectManager.Player.IsDead &&
                Targets.Target != null &&
                Targets.Target.IsValid &&
                Bools.HasNoProtection(Targets.Target))
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
                Bools.HasNoProtection(Targets.Target) &&
                Orbwalking.IsAutoAttack(args.SData.Name) &&
                !args.SData.Name.Equals("lucianpassiveattack"))
            {
				switch (args.Target.Type)
				{
					case GameObjectType.obj_AI_Hero: 
						Logics.ExecuteModes(sender, args);
						break;

					case GameObjectType.obj_AI_Minion: 
						Logics.ExecuteFarm(sender, args);
						break;

					default: 
						break;
				}
            }
        }
    }
}
