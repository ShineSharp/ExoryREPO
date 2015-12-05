namespace ExorAIO.Champions.Jinx
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
        /// <summary>
        /// The spells.
        /// </summary>
        public static void SetSpells()
        {
            Variables.Q = new Spell(SpellSlot.Q, ObjectManager.Player.BoundingRadius + 600f + (25f * (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Q).Level + 1)));
            Variables.W = new Spell(SpellSlot.W, 1500f);
            Variables.E = new Spell(SpellSlot.E, 900f);
            Variables.R = new Spell(SpellSlot.R, 4000f);
            
            Variables.W.SetSkillshot(0.6f, 75f, 3300f, true, SkillshotType.SkillshotLine);
            Variables.E.SetSkillshot(1.2f, 1f, 1750f, false, SkillshotType.SkillshotCircle);
            Variables.R.SetSkillshot(0.7f, 140f, 1500f, false, SkillshotType.SkillshotLine);
        }

        /// <summary>
        /// The menu.
        /// </summary>
        public static void SetMenu()
        {
            /// <summary>
            /// The settings menu.
            /// </summary>
            Variables.SettingsMenu = new Menu("Spell Menu", $"{Variables.MainMenuName}.settingsmenu");
            {
                /// <summary>
                /// The settings menu for the Q spell.
                /// </summary>
                Variables.QMenu = new Menu("Q Settings", $"{Variables.MainMenuName}.qsettingsmenu");
                {
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqauto", "Use Smart Q Switching in Combo")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqfarm", "Use Q to Farm")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.qmana", "Use Q in Harass/Farm only if Mana >= x%"))
                        .SetValue(new Slider(50, 0, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.QMenu);

                /// <summary>
                /// The settings menu for the W spell.
                /// </summary>
                Variables.WMenu = new Menu("W Settings", $"{Variables.MainMenuName}.wsettingsmenu");
                {
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.usewcombo", "Use W in Combo")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.usewks", "Use W to Automatically KillSteal")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.usewimmobile", "Use W to Harass Immobile")).SetValue(true);
                    Variables.WMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.wsettings.wmana", "Use W to AutoHarass if Mana >= x"))
                        .SetValue(new Slider(50, 10, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.WMenu);

                /// <summary>
                /// The settings menu for the E spell.
                /// </summary>
                Variables.EMenu = new Menu("E Settings", $"{Variables.MainMenuName}.esettingsmenu");
                {
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useeauto", "Use Smart E Logic")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useeimmobile", "Use E against Immobile Targets")).SetValue(false);
                }
                Variables.SettingsMenu.AddSubMenu(Variables.EMenu);

                /// <summary>
                /// The settings menu for the R spell.
                /// </summary>
                Variables.RMenu = new Menu("R Settings", $"{Variables.MainMenuName}.rsettingsmenu");
                {
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rsettings.userks", "Use R to KillSteal")).SetValue(true);
                }
                Variables.SettingsMenu.AddSubMenu(Variables.RMenu);
            }
            Variables.Menu.AddSubMenu(Variables.SettingsMenu);

            /// <summary>
            /// The drawings menu.
            /// </summary>
            Variables.DrawingsMenu = new Menu("Drawings Menu", $"{Variables.MainMenuName}.drawingsmenu");
            {
                Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.q", "Show Q Range")).SetValue(true);
                Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.w", "Show W Range")).SetValue(true);
                Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.e", "Show E Range")).SetValue(true);
            }
            Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
        }

        /// <summary>
        /// The methods.
        /// </summary>
        public static void SetMethods()
        {
            Game.OnUpdate += Jinx.Game_OnGameUpdate;
            Obj_AI_Base.OnProcessSpellCast += Jinx.OnProcessSpellCast;
            AntiGapcloser.OnEnemyGapcloser += Jinx.OnGapcloser;
            Obj_AI_Base.OnDoCast += Jinx.Obj_AI_Base_OnDoCast;
        }
    }

    /// <summary>
    /// The targets class.
    /// </summary>
    public class Targets
    {
        public static Obj_AI_Hero Target => TargetSelector.GetTarget(Variables.W.Range + 200f, LeagueSharp.DamageType.Physical);

        public static IEnumerable<Obj_AI_Minion> QMinions => GameObjects.EnemyMinions
            .Where(
                qminion =>
                    qminion.IsValidTarget(Orbwalking.GetRealAutoAttackRange(null) + Variables.Q.Range) &&
                    GameObjects.EnemyMinions
                        .Count(
                            minions =>
                                minions.Distance(qminion) < 225f) > 2);

        public static Obj_AI_Minion QMinion => Targets.QMinions.FirstOrDefault();
    }
}
