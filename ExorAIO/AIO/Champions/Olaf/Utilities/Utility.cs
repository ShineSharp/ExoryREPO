namespace ExorAIO.Champions.Olaf
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using Orbwalking = SFXTargetSelector.Orbwalking;
    using TargetSelector = SFXTargetSelector.TargetSelector;

    using ExorAIO.Utilities;

    /// <summary>
    /// The settings class.
    /// </summary>
    public class Settings
    {
        public static void SetSpells()
        {
            Variables.Q = new Spell(SpellSlot.Q, 1000f);
            Variables.W = new Spell(SpellSlot.W);
            Variables.E = new Spell(SpellSlot.E, 325f);
            Variables.R = new Spell(SpellSlot.R);
            
            Variables.Q.SetSkillshot(0.25f, 80f, 1550f, false, SkillshotType.SkillshotLine);
        }

        public static void SetMenu()
        {
            //Settings Menu
            Variables.SettingsMenu = new Menu("Spell Menu", $"{Variables.MainMenuName}.settingsmenu");
            {
                // Q Options
                Variables.QMenu = new Menu("Q Settings", $"{Variables.MainMenuName}.qsettingsmenu");
                {
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqcombo", "Use Q in Combo")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqfarm", "Use Q to Farm")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqks", "Use Q to Automatically KillSteal")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqautoharass", "Use Q AutoHarass")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqmana", "Use Q to Farm/Harass if Mana >= x"))
                        .SetValue(new Slider(50, 10, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.QMenu);

                // W Options
                Variables.WMenu = new Menu("W Settings", $"{Variables.MainMenuName}.wsettingsmenu");
                {
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.usewcombo", "Use W in Combo")).SetValue(true);
                }
                Variables.SettingsMenu.AddSubMenu(Variables.WMenu);

                // E Options
                Variables.EMenu = new Menu("E Settings", $"{Variables.MainMenuName}.esettingsmenu");
                {
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useecombo", "Use E in Combo")).SetValue(true);
                }
                Variables.SettingsMenu.AddSubMenu(Variables.EMenu);

                // R Options
                Variables.RMenu = new Menu("R Settings", $"{Variables.MainMenuName}.rsettingsmenu");
                {
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rsettings.useranticc", "Use Smart R Anti-HardCC")).SetValue(true);
                }
                Variables.SettingsMenu.AddSubMenu(Variables.RMenu);

                // Miscellaneous Options
                Variables.MiscMenu = new Menu("Misc. Settings", $"{Variables.MainMenuName}.miscsettingsmenu");
                {
                    Variables.MiscMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.miscsettings.useresetters", "Use Smart Tiamat/Ravenous/Titanic")).SetValue(true);
                }
                Variables.SettingsMenu.AddSubMenu(Variables.MiscMenu);
            }
            Variables.Menu.AddSubMenu(Variables.SettingsMenu);

            //Drawings Menu
            Variables.DrawingsMenu = new Menu("Drawings Menu", $"{Variables.MainMenuName}.drawingsmenu");
            {
                Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.q", "Show Q Range")).SetValue(true);
                Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.e", "Show E Range")).SetValue(true);
            }
            Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
        }

        public static void SetMethods()
        {
            Game.OnUpdate += Olaf.Game_OnGameUpdate;
            Obj_AI_Base.OnDoCast += Olaf.Obj_AI_Base_OnDoCast;
        }
    }

    /// <summary>
    /// The targets class.
    /// </summary>
    public class Targets
    {
        public static Obj_AI_Hero Target => TargetSelector.GetTarget(Variables.Q.Range, LeagueSharp.DamageType.Physical);
        public static List<LeagueSharp.Obj_AI_Base> Minions => 
            MinionManager.GetMinions(
                ObjectManager.Player.ServerPosition,
                Variables.Q.Range,
                MinionTypes.All,
                MinionTeam.Enemy,
                MinionOrderTypes.Health
            );
    }
}
