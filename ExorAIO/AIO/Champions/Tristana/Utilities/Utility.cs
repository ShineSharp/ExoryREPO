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
            Variables.E = new Spell(SpellSlot.E, ObjectManager.Player.BoundingRadius + 543f + (7 * ObjectManager.Player.Level));
            Variables.R = new Spell(SpellSlot.R, ObjectManager.Player.BoundingRadius + 543f + (7 * ObjectManager.Player.Level));
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
                Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.e", "Show E Range")).SetValue(false);
                Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.r", "Show R Range")).SetValue(false);
            }
            Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
        }

        /// <summary>
        /// Sets the methods.
        /// </summary>
        public static void SetMethods()
        {
            Game.OnUpdate += Tristana.Game_OnGameUpdate;
        }
    }

    /// <summary>
    /// The killsteal class.
    /// </summary>
    public class KillSteal
    {
        public static float GetRealEDamage(Obj_AI_Hero target)
        {
            return 
                (float)
                    (ObjectManager.Player.CalcDamage(target, LeagueSharp.Common.Damage.DamageType.Physical, Variables.E.GetDamage(target)) +
                    (18 + (3 * ObjectManager.Player.Spellbook.GetSpell(SpellSlot.E).Level)) +
                    ((0.15 + (0.045 * ObjectManager.Player.Spellbook.GetSpell(SpellSlot.E).Level)) * ObjectManager.Player.FlatPhysicalDamageMod) +
                    0.15 * ObjectManager.Player.AbilityPower());
        }

        public static float Damage(Obj_AI_Hero target)
        {
            float dmg = 0f;

            if (Bools.IsCharged(target))
            {
                dmg += GetRealEDamage(target);
            }
            
            if (Variables.R.IsReady())
            {
                dmg += Variables.R.GetDamage(target);
            }
            
            return dmg;
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
        public static Obj_AI_Hero Target => TargetSelector.GetTarget(Orbwalking.GetRealAutoAttackRange(null), LeagueSharp.DamageType.Physical);

        /// <summary>
        /// The charged target.
        /// </summary>
        public static Obj_AI_Base ETarget
        =>
            ObjectManager.Get<Obj_AI_Base>()
                .Where(
                    unit =>
                        Bools.IsCharged(unit)).FirstOrDefault();

        /// <summary>
        /// The minions target.
        /// </summary>
        public static List<Obj_AI_Base> Minions
        => 
            MinionManager.GetMinions(
                ObjectManager.Player.ServerPosition,
                Orbwalking.GetRealAutoAttackRange(null),
                MinionTypes.All
            );
    }
}
