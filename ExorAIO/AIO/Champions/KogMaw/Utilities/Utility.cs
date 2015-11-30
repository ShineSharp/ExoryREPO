namespace ExorAIO.Champions.KogMaw
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;
    
    using ExorAIO.Utilities;

    /// <summary>
    ///    The settings class.
    /// </summary>
    public class Settings
    {
        public static void SetSpells()
        {
            Variables.Q = new Spell(SpellSlot.Q, 1100f);
            Variables.W = new Spell(SpellSlot.W, 590f + (30 * ObjectManager.Player.Spellbook.GetSpell(SpellSlot.W).Level));
            Variables.E = new Spell(SpellSlot.E, 1200f);
            Variables.R = new Spell(SpellSlot.R, 1800f);

            Variables.Q.SetSkillshot(0.25f, 70f, 1650f, true, SkillshotType.SkillshotLine);
            Variables.E.SetSkillshot(0.25f, 120f, 1400f, false, SkillshotType.SkillshotLine);
            Variables.R.SetSkillshot(1.2f, 120f, float.MaxValue, false, SkillshotType.SkillshotCircle);
        }

        public static void SetMenu()
        {
            //Settings Menu
            Variables.SettingsMenu = new Menu("Spell Menu", $"{Variables.MainMenuName}.settingsmenu");
            {
                // Q Options
                Variables.QMenu = new Menu("Q Settings", $"{Variables.MainMenuName}.qsettingsmenu");
                {
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqcombo", "Use Q in Combo")).SetValue(true); //
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqharass", "Use Q in Harass")).SetValue(false); //
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqimmobile", "Use Q against Immobile Targets")).SetValue(true); //
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqks", "Use Q to KillSteal")).SetValue(true); //
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.qmana", "Use Q in Harass only if Mana >= x%")) //
                        .SetValue(new Slider(50, 0, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.QMenu);

                // W Options
                Variables.WMenu = new Menu("W Settings", $"{Variables.MainMenuName}.wsettingsmenu");
                {
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.usewcombo", "Use W in Combo")).SetValue(true); //
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.wadvertiser", " --- The W Limiter Is In The Misc. Menu ---")); //
                }
                Variables.SettingsMenu.AddSubMenu(Variables.WMenu);

                // E Options
                Variables.EMenu = new Menu("E Settings", $"{Variables.MainMenuName}.esettingsmenu");
                {
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useecombo", "Use E in Combo")).SetValue(true); // 
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useeharassfarm", "Use E in Harass/Farm")).SetValue(true); //
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useeimmobile", "Use E against Immobile Targets")).SetValue(true); // 
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useeks", "Use E to KillSteal")).SetValue(true); //
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.emana", "Use E in Harass/Farm only if Mana >= x%")) //
                        .SetValue(new Slider(50, 0, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.EMenu);

                // R Options
                Variables.RMenu = new Menu("R Settings", $"{Variables.MainMenuName}.rsettingsmenu");
                {
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rsettings.user", "Use R")).SetValue(true);
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rsettings.userharassfarm", "Use R in Harass/Farm")).SetValue(true);
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rsettings.userks", "Use R to KillSteal")).SetValue(true);
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rsettings.rcombokeepstacks", "Use R in Combo only if Stacks <= x"))
                        .SetValue(new Slider(2, 1, 10));
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rsettings.rfarmkeepstacks", "Use R in Farm only if Stacks <= x"))
                        .SetValue(new Slider(1, 1, 10));
                    {
                        //Ult Whitelist Menu
                        Variables.WhiteListMenu = new Menu("Ultimate Whitelist Menu", $"{Variables.MainMenuName}.rsettings.rwhitelist");
                        {
                            foreach (Obj_AI_Hero champ in HeroManager.Enemies)
                            {
                                Variables.WhiteListMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rsettings.rwhitelist.{champ.ChampionName.ToLower()}", $"Ult Only: {champ.ChampionName}").SetValue(true));
                            }
                        }
                        Variables.RMenu.AddSubMenu(Variables.WhiteListMenu);
                    }
                }
                Variables.SettingsMenu.AddSubMenu(Variables.RMenu);
                
                //Miscellaneous Menu
                Variables.MiscMenu = new Menu("Miscellaneous Menu", $"{Variables.MainMenuName}.miscmenu");
                {
                    Variables.MiscMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.misc.stacktear", "Stack Tear of the Goddess")).SetValue(true);
                    Variables.MiscMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.misc.stacktearmana", "Stack Tear only if Mana > x%"))
                        .SetValue(new Slider(80, 1, 95));
                }
                Variables.Menu.AddSubMenu(Variables.MiscMenu);

                //Drawings Menu
                Variables.DrawingsMenu = new Menu("Drawings Menu", $"{Variables.MainMenuName}.drawingsmenu");
                {
                    Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.q", "Show Q Range")).SetValue(true);
                    Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.w", "Show W Range")).SetValue(true);
                    Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.e", "Show E Range")).SetValue(true);
                    Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.r", "Show R Range")).SetValue(true);
                }
                Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
            }
            Variables.Menu.AddSubMenu(Variables.SettingsMenu);
        }

        public static void SetMethods()
        {
            Game.OnUpdate += KogMaw.Game_OnGameUpdate;
            Obj_AI_Base.OnDoCast += KogMaw.Obj_AI_Base_OnDoCast;
        }
    }

    /// <summary>
    ///    The targets class.
    /// </summary>
    public class Targets
    {
        public static Obj_AI_Hero Target => TargetSelector.GetTarget(Variables.R.Range, TargetSelector.DamageType.Physical);
        public static List<LeagueSharp.Obj_AI_Base> Minions => MinionManager.GetMinions(ObjectManager.Player.ServerPosition, Variables.R.Range, MinionTypes.All);
    }
}
