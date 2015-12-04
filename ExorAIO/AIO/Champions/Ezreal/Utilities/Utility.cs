namespace ExorAIO.Champions.Ezreal
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;
    
    using ExorAIO.Utilities;

    /// <summary>
    /// The settings class.
    /// </summary>
    public class Settings
    {
        public static void SetSpells()
        {
            Variables.Q = new Spell(SpellSlot.Q, 1150f);
            Variables.W = new Spell(SpellSlot.W, 1000f);
            Variables.E = new Spell(SpellSlot.E, ObjectManager.Player.BoundingRadius + 500f);
            Variables.R = new Spell(SpellSlot.R, 3000f);

            Variables.Q.SetSkillshot(0.25f, 60f, 2000f, true, SkillshotType.SkillshotLine);
            Variables.W.SetSkillshot(0.25f, 80f, 1600f, false, SkillshotType.SkillshotLine);
            Variables.R.SetSkillshot(1.0f, 160f, 2000f, false, SkillshotType.SkillshotLine);
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
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqks", "Use Q to Automatically KillSteal")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqfarm", "Use Q to Farm")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqimmobile", "Use Q to Harass Immobile")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqautoharass", "Use Q to AutoHarass")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.qmana", "Use Q to AutoHarass if Mana >= x"))
                        .SetValue(new Slider(50, 10, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.QMenu);

                // W Options
                Variables.WMenu = new Menu("W Settings", $"{Variables.MainMenuName}.wsettingsmenu");
                {
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.usewcombo", "Use W in Combo")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.usewks", "Use W to Automatically KillSteal")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.usewimmobile", "Use W to Harass Immobile")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.usewautoharass", "Use W to AutoHarass")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.wmana", "Use W to AutoHarass if Mana >= x"))
                        .SetValue(new Slider(50, 10, 99));
                    
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
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rsettings.usercombo", "Use R in Combo")).SetValue(true);
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rsettings.userks", "Use R to KillSteal")).SetValue(true);
                    {
                        //Ult Whitelist Menu
                        Variables.WhiteListMenu = new Menu("Ultimate Whitelist Menu", $"{Variables.MainMenuName}.rsettings.rwhitelist");
                        {
                            foreach (Obj_AI_Hero champ in HeroManager.Enemies)
                            {
                                Variables.WhiteListMenu.AddItem(
                                    new MenuItem($"{Variables.MainMenuName}.rsettings.rwhitelist.{champ.ChampionName.ToLower()}",
                                        $"Ult Only: {champ.ChampionName}").SetValue(true));
                            }
                        }
                        Variables.RMenu.AddSubMenu(Variables.WhiteListMenu);
                    }
                }
                Variables.SettingsMenu.AddSubMenu(Variables.RMenu);

                // Miscellaneous Options
                Variables.MiscMenu = new Menu("Misc. Settings", $"{Variables.MainMenuName}.miscsettingsmenu");
                {
                    Variables.MiscMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.misc.stacktear", "Stack Tear of the Goddess")).SetValue(true);
                    Variables.MiscMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.misc.stacktearmana", "Stack Tear only if Mana > x%"))
                        .SetValue(new Slider(80, 1, 95));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.MiscMenu);
            }
            Variables.Menu.AddSubMenu(Variables.SettingsMenu);

            //Drawings Menu
            Variables.DrawingsMenu = new Menu("Drawings Menu", $"{Variables.MainMenuName}.drawingsmenu");
            {
                Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.q", "Show Q Range")).SetValue(true);
                Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.w", "Show W Range")).SetValue(true);
                Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.e", "Show E Range")).SetValue(true);
            }
            Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
        }

        public static void SetMethods()
        {
            Game.OnUpdate += Ezreal.Game_OnGameUpdate;
            Obj_AI_Base.OnDoCast += Ezreal.Obj_AI_Base_OnDoCast;
        }
    }

    /// <summary>
    /// The targets class.
    /// </summary>
    public class Targets
    {
        public static Obj_AI_Hero Target => TargetSelector.GetTarget(Variables.W.Range, TargetSelector.DamageType.Physical);
        public static Obj_AI_Base FarmMinion
        => 
            MinionManager.GetMinions(ObjectManager.Player.ServerPosition, Variables.Q.Range, MinionTypes.All, MinionTeam.Enemy, MinionOrderTypes.Health)
                .Where(
                    m =>
                        m.Health < Variables.Q.GetDamage(m))
                .FirstOrDefault();
    }
}
