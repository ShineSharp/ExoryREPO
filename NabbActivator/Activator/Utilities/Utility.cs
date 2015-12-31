namespace NabbActivator
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    /// <summary>
    /// The settings class.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Sets the spells.
        /// </summary>
        public static void SetSpells()
        {
            Variables.W = new Spell(SpellSlot.W);
        }

        /// <summary>
        /// Sets the menu.
        /// </summary>
        public static void SetMenu()
        {
            Variables.Menu = new Menu("NabbActivator", $"{Variables.MainMenuName}", true);
            {
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.offensives", "Enable Offensive Items")).SetValue(true);
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.defensives", "Enable Defensive Items")).SetValue(true);
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.spells", "Enable Spells")).SetValue(true);
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.cleansers", "Enable Cleansers")).SetValue(true);
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.consumables", "Enable Potions")).SetValue(true);
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.resetters", "Enable Tiamat/Hydra/Titanic")).SetValue(true);
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.randomizer", "Enable NabbHumanizer")).SetValue(true);
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.separator", ""));
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.warn1", "!(Must be the same as your Combo key)!"));
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.combo_button", "Combo")).SetValue(new KeyBind(32, KeyBindType.Press));
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.warn2", "!(Must be the same as your Combo key)!"));
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.separator2", ""));
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.warn3", "!(Must be the same as your LaneClear key)!"));
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.laneclear_button", "LaneClear")).SetValue(new KeyBind(86, KeyBindType.Press));
                Variables.Menu.AddItem(new MenuItem($"{Variables.MainMenuName}.warn4", "!(Must be the same as your LaneClear key)!"));
                Variables.SliderMenu = new Menu("Consumables Options", $"{Variables.MainMenuName}.consumables.options");
                {
                    Variables.SliderMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.consumables.on_health_percent", "Use Health Potions if Health < x%").SetValue(new Slider(50, 0, 100)));
                    Variables.SliderMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.consumables.on_mana_percent", "Use Mana Potions if Mana < x%").SetValue(new Slider(50, 0, 100)));
                }
                Variables.Menu.AddSubMenu(Variables.SliderMenu);
            }
            Variables.Menu.AddToMainMenu();
        }

        /// <summary>
        /// Sets the methods.
        /// </summary>
        public static void SetMethods()
        {
            Game.OnUpdate += Activator.Game_OnGameUpdate;
            Obj_AI_Base.OnDoCast += Activator.Obj_AI_Base_OnDoCast;
        }
    }

    /// <summary>
    /// The targets.
    /// </summary>
    public class Targets
    {
        /// <summary>
        /// The main hero target.
        /// </summary>
        public static Obj_AI_Hero Target => TargetSelector.GetTarget(850f, TargetSelector.DamageType.Physical);

        /// <summary>
        /// The minion targets.
        /// </summary>
        public static Obj_AI_Minion banTarget
        =>
            ObjectManager.Get<Obj_AI_Minion>()
                .Where(
                    x =>
                        x.IsValidTarget(1200f, false) &&
                        x.CharData.BaseSkinName.Contains("siege"))
                .FirstOrDefault();
    }
}
