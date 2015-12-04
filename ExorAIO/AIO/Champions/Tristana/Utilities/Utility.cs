namespace ExorAIO.Champions.Tristana
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
            Variables.Q = new Spell(SpellSlot.Q);
            Variables.E = new Spell(SpellSlot.E, ObjectManager.Player.BoundingRadius + (550 + (7 * ObjectManager.Player.Level)));
            Variables.R = new Spell(SpellSlot.R, ObjectManager.Player.BoundingRadius + (550 + (7 * ObjectManager.Player.Level)));
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
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqauto", "Use Smart Q")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqfarm", "Use Q to Farm")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.qmana", "Use Q in Farm only if Mana >= x%"))
                        .SetValue(new Slider(50, 0, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.QMenu);

                /// <summary>
                /// The settings menu for the E spell.
                /// </summary>
                Variables.EMenu = new Menu("E Settings", $"{Variables.MainMenuName}.esettingsmenu");
                {
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useeauto", "Use E Combo")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useefarm", "Use E to Farm")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useebuildings", "Use E against Buildings")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.emana", "Use E in Farm only if Mana >= x%"))
                        .SetValue(new Slider(50, 0, 99));
                    {
                        /// <summary>
                        /// The whitelist menu for the E spell.
                        /// </summary>
                        Variables.WhiteListMenu = new Menu("E Whitelist Menu", $"{Variables.MainMenuName}.esettings.ewhitelist");
                        {
                            foreach (Obj_AI_Hero champ in HeroManager.Enemies)
                            {
                                Variables.WhiteListMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.ewhitelist.{champ.ChampionName.ToLower()}", $"E Only: {champ.ChampionName}").SetValue(true));
                            }
                        }
                        Variables.EMenu.AddSubMenu(Variables.WhiteListMenu);
                    }
                }
                Variables.SettingsMenu.AddSubMenu(Variables.EMenu);

                /// <summary>
                /// The settings menu for the R spell.
                /// </summary>
                Variables.RMenu = new Menu("R Settings", $"{Variables.MainMenuName}.rsettingsmenu");
                {
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rsettings.userks", "Use Smart E & R KillSteal")).SetValue(true);
                }
                Variables.SettingsMenu.AddSubMenu(Variables.RMenu);
            }
            Variables.Menu.AddSubMenu(Variables.SettingsMenu);

            /// <summary>
            /// The drawings menu.
            /// </summary>
            Variables.DrawingsMenu = new Menu("Drawings Menu", $"{Variables.MainMenuName}.drawingsmenu");
            {
                Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.e", "Show E Range")).SetValue(true);
                Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.r", "Show R Range")).SetValue(true);
            }
            Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
        }

        /// <summary>
        /// The methods.
        /// </summary>
        public static void SetMethods()
        {
            Orbwalking.BeforeAttack += Tristana.Orbwalking_BeforeAttack;
            Obj_AI_Base.OnDoCast += Tristana.Obj_AI_Base_OnDoCast;
        }
    }

    /// <summary>
    /// The killsteal class.
    /// </summary>
    public class KillSteal
    {
        public static int Damage(Obj_AI_Hero target)
        =>
            Bools.IsCharged(target) ?
                (int)(Variables.E.GetDamage(target) *
                    ((0.30f * target.GetBuffCount("TristanaECharge")) + 1f) + 
                    Variables.R.GetDamage(target))
            :
                (int)Variables.R.GetDamage(target);
    }

    /// <summary>
    /// The targets class.
    /// </summary>
    public class Targets
    {
        public static IEnumerable<Obj_AI_Minion> EMinions => GameObjects.EnemyMinions
            .Where(
                eminion =>
                    eminion.IsValidTarget(Variables.E.Range) &&
                    GameObjects.EnemyMinions
                        .Count(
                            minions =>
                                minions.Distance(eminion) < 150f) > 2);

        public static Obj_AI_Minion EMinion => Targets.EMinions.FirstOrDefault();
    }
}
