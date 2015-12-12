namespace ExorAIO.Champions.Vayne
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
            Variables.Q = new Spell(SpellSlot.Q, ObjectManager.Player.BoundingRadius + (ObjectManager.Player.AttackRange + 300f));
            Variables.W = new Spell(SpellSlot.W);
            Variables.E = new Spell(SpellSlot.E, ObjectManager.Player.BoundingRadius + 600f);
            
            Variables.E.SetTargetted(0.25f, 1200f);
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
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqcombo", "Use Q in Combo")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqfarm", "Use Q LastHit Helper")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqks", "Use Q to Automatically KillSteal")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.qmana", "Use Q in Harass/Farm only if Mana >= x%"))
                        .SetValue(new Slider(50, 0, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.QMenu);

                Variables.EMenu = new Menu("E Settings", $"{Variables.MainMenuName}.esettingsmenu");
                {
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useeauto", "Use E")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useeks", "Use E to Automatically KillSteal")).SetValue(false);
                    {
                        Variables.WhiteListMenu = new Menu("Condemn Whitelist Menu", $"{Variables.MainMenuName}.esettings.ewhitelist");
                        {
                            foreach (Obj_AI_Hero champ in HeroManager.Enemies)
                            {
                                Variables.WhiteListMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.ewhitelist.{champ.ChampionName.ToLower()}", $"Condemn: {champ.ChampionName}").SetValue(true));
                            }
                        }
                        Variables.EMenu.AddSubMenu(Variables.WhiteListMenu);
                    }
                }
                Variables.SettingsMenu.AddSubMenu(Variables.EMenu);
            }
            Variables.Menu.AddSubMenu(Variables.SettingsMenu);

            Variables.MiscMenu = new Menu("Miscellaneous Menu", $"{Variables.MainMenuName}.miscsettingsmenu");
            {
                Variables.MiscMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.miscsettings.noaastealth", "Use Don't AA when Stealthed Logic")).SetValue(false);
            }
            Variables.SettingsMenu.AddSubMenu(Variables.MiscMenu);

            Variables.DrawingsMenu = new Menu("Drawings Menu", $"{Variables.MainMenuName}.drawingsmenu");
            {
                Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.q", "Show Q Range")).SetValue(true);
                Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.e", "Show E Range")).SetValue(true);
            }
            Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
        }

        /// <summary>
        /// Sets the methods.
        /// </summary>
        public static void SetMethods()
        {
            Game.OnUpdate += Vayne.Game_OnGameUpdate;
            Obj_AI_Base.OnDoCast += Vayne.Obj_AI_Base_OnDoCast;
        }
    }

    /// <summary>
    /// The killsteal class.
    /// </summary>
    public class KillSteal
    {
        /// <summary>
        /// Gets the Killsteal damage.
        /// </summary>
        public static float GetDamage(Obj_AI_Hero target)
        {
            float dmg = 0f;

            if (Bools.Has2WStacks(target))
            {
                dmg += Variables.W.GetDamage(target);
            }
            
            if (Variables.E.IsReady())
            {
                dmg += Variables.E.GetDamage(target);
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
        public static Obj_AI_Hero Target => TargetSelector.GetTarget(Variables.Q.Range, LeagueSharp.DamageType.Physical);

        /// <summary>
        /// The minion targets.
        /// </summary>       
        public static IOrderedEnumerable<Obj_AI_Base> FarmMinions
        =>
            MinionManager.GetMinions(ObjectManager.Player.ServerPosition, Variables.Q.Range, MinionTypes.All)
                .Where(
                    m =>
                        m.Health < ObjectManager.Player.GetAutoAttackDamage(m) + Variables.Q.GetDamage(m))
                .OrderBy(
                    m =>
                        m.HealthPercent);
    }
}
