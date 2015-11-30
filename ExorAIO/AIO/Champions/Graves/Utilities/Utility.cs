namespace ExorAIO.Champions.Graves
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
            Variables.Q = new Spell(SpellSlot.Q, 950f);
            Variables.W = new Spell(SpellSlot.W, 960f);
            Variables.E = new Spell(SpellSlot.E);
            Variables.R = new Spell(SpellSlot.R, 1000f);

            Variables.Q.SetSkillshot(0.25f, 60f, 2000f, false, SkillshotType.SkillshotLine);
            Variables.W.SetSkillshot(0.35f, 150f, 1650f, false, SkillshotType.SkillshotCircle);
            Variables.R.SetSkillshot(0.25f, 120f, 2100f, false, SkillshotType.SkillshotLine);
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
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqimmobile", "Use Q to Harass Immobile Champions")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqharassfarm", "Use Q in Harass/Farm")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.qmana", "Use Q in Harass/Farm only if Mana >= x%"))
                        .SetValue(new Slider(50, 0, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.QMenu);

                // W Options
                Variables.WMenu = new Menu("W Settings", $"{Variables.MainMenuName}.wsettingsmenu");
                {
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.usewcombo", "Use W in Combo")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.usewks", "Use W to Automatically KillSteal")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.usewharass", "Use W in Harass")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.wmana", "Use W in Harass/Farm only if Mana >= x%"))
                        .SetValue(new Slider(50, 0, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.WMenu);

                // E Options
                Variables.EMenu = new Menu("E Settings", $"{Variables.MainMenuName}.esettingsmenu");
                {
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useereset", "Use E to Reset Ammos + AAs")).SetValue(true);
                }
                Variables.SettingsMenu.AddSubMenu(Variables.EMenu);

                // R Options
                Variables.RMenu = new Menu("R Settings", $"{Variables.MainMenuName}.rsettingsmenu");
                {
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rsettings.userks", "Use R to KillSteal")).SetValue(true);
                }
                Variables.SettingsMenu.AddSubMenu(Variables.RMenu);
            }
            Variables.Menu.AddSubMenu(Variables.SettingsMenu);

            //Drawings Menu
            Variables.DrawingsMenu = new Menu("Drawings Menu", $"{Variables.MainMenuName}.drawingsmenu");
            {
                Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.q", "Show Q Range")).SetValue(true);
                Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.w", "Show W Range")).SetValue(true);
                Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.r", "Show R Range")).SetValue(true);
            }
            Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
        }

        public static void SetMethods()
        {
            Game.OnUpdate += Graves.Game_OnGameUpdate;
            Obj_AI_Base.OnDoCast += Graves.Obj_AI_Base_OnDoCast;
        }
    }

    /// <summary>
    ///    The targets class.
    /// </summary>
    public class Targets
    {
        public static Obj_AI_Hero Target => TargetSelector.GetTarget(Variables.R.Range, TargetSelector.DamageType.Physical);
        public static List<LeagueSharp.Obj_AI_Base> Minions => 
            MinionManager.GetMinions(
                ObjectManager.Player.ServerPosition,
                Variables.Q.Range,
                MinionTypes.All,
                MinionTeam.Enemy,
                MinionOrderTypes.Health
            );
    }

    /// <summary>
    ///    The bools class.
    /// </summary>
    public class Bools
    {
        public static bool IsImmobile(Obj_AI_Hero Target)
        => 
            Target.HasBuffOfType(BuffType.Stun) ||
            Target.HasBuffOfType(BuffType.Snare) ||
            Target.HasBuffOfType(BuffType.Knockup) ||
            Target.HasBuffOfType(BuffType.Charm) ||
            Target.HasBuffOfType(BuffType.Flee) || 
            Target.HasBuffOfType(BuffType.Taunt) ||
            Target.HasBuffOfType(BuffType.Suppression);
    }
}
