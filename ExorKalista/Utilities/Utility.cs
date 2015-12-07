namespace ExorKalista
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using Orbwalking = SFXTargetSelector.Orbwalking;
    using TargetSelector = SFXTargetSelector.TargetSelector;

    /// <summary>
    /// The settings class.
    /// </summary>
    public class Settings
    {    
        /// <summary>
        /// The spells.
        /// </summary>
        public static void SetSpells()
        {
            Variables.Q = new Spell(SpellSlot.Q, 1150f);
            Variables.W = new Spell(SpellSlot.W, 5000f);
            Variables.E = new Spell(SpellSlot.E, ObjectManager.Player.BoundingRadius + 950f);
            Variables.R = new Spell(SpellSlot.R, 1200f);

            Variables.Q.SetSkillshot(0.25f, 40f, 2100f, true, SkillshotType.SkillshotLine);
        }
        
        /// <summary>
        /// The menu builder.
        /// </summary>
        public static void SetMenu()
        {
            /// <summary>
            /// The main menu.
            /// </summary>
            Variables.Menu = new Menu("ExorKalista", $"{Variables.MainMenuName}", true);
            {
                /// <summary>
                /// The ExOrbwalker menu.
                /// </summary>
                Variables.OrbwalkerMenu = new Menu("Orbwalker", $"{Variables.MainMenuName}.orbwalkermenu");
                {
                    Variables.Orbwalker = new Orbwalking.Orbwalker(Variables.OrbwalkerMenu);
                }
                Variables.Menu.AddSubMenu(Variables.OrbwalkerMenu);
                
                /// <summary>
                /// The SFX TargetSelector Menu menu.
                /// </summary>
                Variables.TargetSelectorMenu = new Menu("[SFX]Target Selector", $"{Variables.MainMenuName}.targetselector");
                {
                    TargetSelector.AddToMenu(Variables.TargetSelectorMenu);
                }
                Variables.Menu.AddSubMenu(Variables.TargetSelectorMenu);

                /// <summary>
                /// The settings menu.
                /// </summary>
                Variables.SettingsMenu = new Menu("Spell Menu", $"{Variables.MainMenuName}.settingsmenu");
                {
                    /// <summary>
                    /// The menu for the Q spell.
                    /// </summary>
                    Variables.QMenu = new Menu("Q Settings", $"{Variables.MainMenuName}.qsettingsmenu");
                    {
                        Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqcombo", "Use Q in Combo")).SetValue(true);
                        Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqks", "Use Q to Automatically KillSteal")).SetValue(true);
                        Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqimmobile", "Use Q against Immobile Enemies")).SetValue(true);
                        Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqfarm", "Use Q to Farm")).SetValue(true);
                        Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqmana", "Use Q to Farm if Mana >= %"))
                            .SetValue(new Slider(50, 0, 99));
                    }
                    Variables.SettingsMenu.AddSubMenu(Variables.QMenu);

                    /// <summary>
                    /// The menu for the W spell.
                    /// </summary>
                    Variables.WMenu = new Menu("W Settings", $"{Variables.MainMenuName}.wsettingsmenu");
                    {
                        Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.usewauto", "Use Smart W to Dragon & Baron")).SetValue(true);
                        Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.usewmana", "Use Smart W if Mana >= %"))
                            .SetValue(new Slider(50, 0, 99));
                    }
                    Variables.SettingsMenu.AddSubMenu(Variables.WMenu);

                    /// <summary>
                    /// The menu for the E spell.
                    /// </summary>
                    Variables.EMenu = new Menu("E Settings", $"{Variables.MainMenuName}.esettingsmenu");
                    {
                        Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useecombo", "Automatically Execute Enemies with E")).SetValue(true);
                        Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useemonsters", "Automatically Execute Monsters with E")).SetValue(true);
                        Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useeharass", "Automatically use E to Harass (Minions -> E)")).SetValue(true);
                        Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useedie", "Use E just before Dying")).SetValue(true);
                        Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useefarm", "Use E to Farm")).SetValue(true);
                    }
                    Variables.SettingsMenu.AddSubMenu(Variables.EMenu);

                    /// <summary>
                    /// The menu for the R spell.
                    /// </summary>
                    Variables.RMenu = new Menu("R Settings", $"{Variables.MainMenuName}.rsettingsmenu");
                    {
                        Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rsettings.userlifesaver", "Use R LifeSaver")).SetValue(true);
                    }
                    Variables.SettingsMenu.AddSubMenu(Variables.RMenu);
                }
                Variables.Menu.AddSubMenu(Variables.SettingsMenu);

                /// <summary>
                /// The menu for the drawings.
                /// </summary>
                Variables.DrawingsMenu = new Menu("Drawings Menu", $"{Variables.MainMenuName}.drawingsmenu");
                {
                    Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.q", "Show Q Range")).SetValue(true);
                    Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.e", "Show E Range")).SetValue(true);
                    Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.edmg", "Show E Damage")).SetValue(true);
                    Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.r", "Show R Range")).SetValue(true);
                    Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.dragon_stacks_debug", "Show Stacks")).SetValue(true);
                    Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.dragon_damage_debug", "Show Damage")).SetValue(true);
                }
                Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
            }
            Variables.Menu.AddToMainMenu();
        }
        
        /// <summary>
        /// The loaded methods.
        /// </summary>
        public static void SetMethods()
        {
            Game.OnUpdate += Kalista.Game_OnGameUpdate;
            Obj_AI_Base.OnDoCast += Kalista.Obj_AI_Base_OnDoCast;
            Orbwalking.OnNonKillableMinion += Kalista.OnNonKillableMinion;
        }
    }

    /// <summary>
    /// The targets class.
    /// </summary>
    public class Targets
    {
        public static Obj_AI_Hero Target => TargetSelector.GetTarget(Variables.Q.Range, LeagueSharp.DamageType.Physical);
        public static List<Obj_AI_Base> Minions => MinionManager.GetMinions(Variables.E.Range, MinionTypes.All, MinionTeam.Enemy);
    }
}
