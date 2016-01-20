namespace ExorLucian
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
        /// Sets the spells.
        /// </summary>
        public static void SetSpells()
        {
            Variables.Q = new Spell(SpellSlot.Q, ObjectManager.Player.BoundingRadius*3 + 500f);
            Variables.W = new Spell(SpellSlot.W, 1000f);
            Variables.E = new Spell(SpellSlot.E, ObjectManager.Player.BoundingRadius + (ObjectManager.Player.AttackRange + 450f));
            Variables.R = new Spell(SpellSlot.R, 1400f);
            
            Variables.Q.SetTargetted(0.25f, float.MaxValue);
            Variables.W.SetSkillshot(0.3f, 80f, 1600f, true, SkillshotType.SkillshotLine);
            Variables.E.SetSkillshot(.25f, 1f, float.MaxValue, false, SkillshotType.SkillshotLine);
        }

        /// <summary>
        /// Sets the menu.
        /// </summary>
        public static void SetMenu()
        {
            Variables.Menu = new Menu("ExorLucian", $"{Variables.MainMenuName}", true);
            {
                Variables.OrbwalkerMenu = new Menu("Orbwalker", $"{Variables.MainMenuName}.orbwalkermenu");
                {
                    Variables.Orbwalker = new Orbwalking.Orbwalker(Variables.OrbwalkerMenu);
                }
                Variables.Menu.AddSubMenu(Variables.OrbwalkerMenu);

                Variables.TargetSelectorMenu = new Menu("[SFX]Target Selector", $"{Variables.MainMenuName}.targetselector");
                {
                    TargetSelector.AddToMenu(Variables.TargetSelectorMenu);
                }
                Variables.Menu.AddSubMenu(Variables.TargetSelectorMenu);

                Variables.SettingsMenu = new Menu("Spell Menu", $"{Variables.MainMenuName}.settingsmenu");
                {
                    Variables.QMenu = new Menu("Q Settings", $"{Variables.MainMenuName}.qsettingsmenu");
                    {
                        Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqcombo", "Use Q Combo")).SetValue(true);
                        Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqautoharass", "Use Q AutoHarass")).SetValue(true);
                        Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqks", "Use Q to KillSteal")).SetValue(true);
                        Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqfarm", "Use Q to Farm")).SetValue(true);
                        {
                            Variables.WhiteListMenu = new Menu("Extended Q Harass Whitelist", $"{Variables.MainMenuName}.qsettings.qwhitelist");
                            {
                                foreach (Obj_AI_Hero champ in HeroManager.Enemies)
                                {
                                    Variables.WhiteListMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.qwhitelist.{champ.ChampionName.ToLower()}", $"Harass: {champ.ChampionName}").SetValue(true));
                                }
                            }
                            Variables.QMenu.AddSubMenu(Variables.WhiteListMenu);
                        }
                        Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.qmana", "Use Q in Harass/Farm only if Mana >= x%"))
                            .SetValue(new Slider(50, 0, 99));
                    }
                    Variables.SettingsMenu.AddSubMenu(Variables.QMenu);

                    Variables.WMenu = new Menu("W Settings", $"{Variables.MainMenuName}.wsettingsmenu");
                    {
                        Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.usewcombo", "Use W in Combo")).SetValue(true);
                        Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.usewks", "Use W to Automatically KillSteal")).SetValue(true);
                        Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.usewfarm", "Use W to Farm")).SetValue(true);
                        Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.wmana", "Use W to Farm only if Mana >= x%"))
                            .SetValue(new Slider(50, 0, 99));
                    }
                    Variables.SettingsMenu.AddSubMenu(Variables.WMenu);

                    Variables.EMenu = new Menu("E Settings", $"{Variables.MainMenuName}.esettingsmenu");
                    {
                        Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useeauto", "Use Smart E Logic")).SetValue(true);
                        Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useejc", "Use E to JungleClear")).SetValue(true);
                        Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.emana", "Use E to JungleClear only if Mana >= x%"))
                            .SetValue(new Slider(50, 0, 99));
                    }
                    Variables.SettingsMenu.AddSubMenu(Variables.EMenu);
                }
                Variables.Menu.AddSubMenu(Variables.SettingsMenu);

                Variables.DrawingsMenu = new Menu("Drawings Menu", $"{Variables.MainMenuName}.drawingsmenu");
                {
                    Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.q", "Show Q Range")).SetValue(false);
                    Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.w", "Show W Range")).SetValue(false);
                    Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.e", "Show E Range")).SetValue(false);
                }
                Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
            }
            Variables.Menu.AddToMainMenu();
        }

        /// <summary>
        /// The methods.
        /// </summary>
        public static void SetMethods()
        {
            Game.OnUpdate += Lucian.Game_OnGameUpdate;
            Obj_AI_Base.OnDoCast += Lucian.Obj_AI_Base_OnDoCast;
        }
    }

    /// <summary>
    /// The targets class.
    /// </summary>
    public class Targets
    {
        /// <summary>
        /// The main hero target.
        /// </summary>
        public static Obj_AI_Hero Target => TargetSelector.GetTarget(Variables.Q.Range + 600f, LeagueSharp.DamageType.Physical);

        /// <summary>
        /// The minions target.
        /// </summary>
        public static List<Obj_AI_Base> Minions
        =>
            MinionManager.GetMinions(
                ObjectManager.Player.ServerPosition,
                Variables.Q.Range,
                MinionTypes.All
            );
    }
}
