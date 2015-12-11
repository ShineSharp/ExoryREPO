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
        /// Sets the spells.
        /// </summary>
        public static void SetSpells()
        {
            Variables.Q = new Spell(SpellSlot.Q);
            Variables.E = new Spell(SpellSlot.E, ObjectManager.Player.BoundingRadius + (550 + (7 * ObjectManager.Player.Level)));
            Variables.R = new Spell(SpellSlot.R, ObjectManager.Player.BoundingRadius + (550 + (7 * ObjectManager.Player.Level)));
        }

        /// <summary>
        /// Sets the menu.
        /// </summary>
        public static void SetMenu()
        {
            Variables.SettingsMenu = new Menu("Spell Menu", $"{Variables.MainMenuName}.settingsmenu");
            {
                Variables.QMenu = new Menu("Q Settings", $"{Variables.MainMenuName}.qsettingsmenu");
                {
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqauto", "Use Smart Q")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqfarm", "Use Q to Farm")).SetValue(true);
                }
                Variables.SettingsMenu.AddSubMenu(Variables.QMenu);

                Variables.EMenu = new Menu("E Settings", $"{Variables.MainMenuName}.esettingsmenu");
                {
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useeauto", "Use E Combo")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useefarm", "Use E to Farm")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useebuildings", "Use E against Buildings")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.emana", "Use E in Farm only if Mana >= x%"))
                        .SetValue(new Slider(50, 0, 99));
                    {
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

                Variables.RMenu = new Menu("R Settings", $"{Variables.MainMenuName}.rsettingsmenu");
                {
                    Variables.RMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.rsettings.userks", "Use Smart E & R KillSteal")).SetValue(true);
                }
                Variables.SettingsMenu.AddSubMenu(Variables.RMenu);
            }
            Variables.Menu.AddSubMenu(Variables.SettingsMenu);

            Variables.DrawingsMenu = new Menu("Drawings Menu", $"{Variables.MainMenuName}.drawingsmenu");
            {
                Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.e", "Show E Range")).SetValue(true);
                Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.r", "Show R Range")).SetValue(true);
            }
            Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
        }

        /// <summary>
        /// Sets the methods.
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
                (int)(Variables.E.GetDamage(target) * ((0.30f * target.GetBuffCount("TristanaECharge")) + 1f) + Variables.R.GetDamage(target)) :
                (int)Variables.R.GetDamage(target);
    }

    /// <summary>
    /// The targets class.
    /// </summary>
    public class Targets
    {
        /// <summary>
        /// The E spell's minion targets.
        /// </summary>
        public static IEnumerable<Obj_AI_Minion> EMinions
        => 
            GameObjects.EnemyMinions
                .Where(
                    eminion =>
                        eminion.IsValidTarget(Variables.E.Range));
    }
}
