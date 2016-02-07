using LeagueSharp;
using LeagueSharp.Common;

namespace NabbActivator
{
    using System;
    using System.Linq;

    /// <summary>
    /// The main class.
    /// </summary>
    class Activator
    {
        /// <summary>
        /// Called when the game loads itself.
        /// </summary>
        public static void OnLoad()
        {
            Menus.Initialize();
            ISpells.Initialize();
            Methods.Initialize();
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void OnUpdate(EventArgs args)
        {
            if (ObjectManager.Player.IsDead)
            {
                return;
            }

            /// <summary>
            /// Load the Defesive items.
            /// </summary>
            if (Variables.Menu.Item($"{Variables.MainMenuName}.defensives").GetValue<bool>()) 
            {
                Defensives.Execute(args);
            }

            /// <summary>
            /// Load the Spells.
            /// </summary>
            if (Variables.Menu.Item($"{Variables.MainMenuName}.spells").GetValue<bool>())
            {
                Spells.Execute(args);
            }

            /// <summary>
            /// Load the Cleanser items.
            /// </summary>
            if (Variables.Menu.Item($"{Variables.MainMenuName}.cleansers").GetValue<bool>())
            {
                Cleansers.Execute(args);
                SCleansers.Execute(args);
            }

            /// <summary>
            /// Load the Consumable items.
            /// </summary>
            if (Variables.Menu.Item($"{Variables.MainMenuName}.consumables").GetValue<bool>())
            {
                Consumables.Execute(args);
            }

            /// <summary>
            /// Load the Offensive items.
            /// </summary>
            if (Variables.Menu.Item($"{Variables.MainMenuName}.offensives").GetValue<bool>() &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.combo_button").GetValue<KeyBind>().Active ||
                    Variables.Menu.Item($"{Variables.MainMenuName}.laneclear_button").GetValue<KeyBind>().Active))
            {
                Offensives.Execute(args);
            }
        }

        /// <summary>
        /// Called when a cast has been executed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void OnDoCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// Load the Ohmwrecker logic.
            /// </summary>
            if (sender != null &&
                args.Target != null &&
                args.Target.IsAlly &&
                sender.IsValid<Obj_AI_Turret>() &&
                args.Target.IsValid<Obj_AI_Hero>() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.defensives").GetValue<bool>())
            {
                Ohmwrecker.Execute(sender, args);
            }

            /// <summary>
            /// Load the Offensive items.
            /// </summary>
            if (sender.IsMe &&
			    Orbwalking.IsAutoAttack(args.SData.Name) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.resetters").GetValue<bool>() &&
                (Variables.Menu.Item($"{Variables.MainMenuName}.combo_button").GetValue<KeyBind>().Active ||
                    Variables.Menu.Item($"{Variables.MainMenuName}.laneclear_button").GetValue<KeyBind>().Active))
            {
                Resetters.Execute(sender, args);
            }
        }
    }
}
