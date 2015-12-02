namespace ExorAIO.Champions.Vayne
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
        /// <summary>
        /// The spells.
        /// </summary>
        public static void SetSpells()
        {
            Variables.Q = new Spell(SpellSlot.Q, 300f);
            Variables.E = new Spell(SpellSlot.E, 650f);
            
            Variables.E.SetTargetted(0.25f, 2200f);
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
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqcombo", "Use Q in Combo")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqks", "Use Q to Automatically KillSteal")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.useqfarm", "Use Q LastHit Helper")).SetValue(true);
                    Variables.QMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.qsettings.qmana", "Use Q in Harass/Farm only if Mana >= x%"))
                        .SetValue(new Slider(50, 0, 99));
                }
                Variables.SettingsMenu.AddSubMenu(Variables.QMenu);

                /// <summary>
                /// The settings menu for the E spell.
                /// </summary>
                Variables.EMenu = new Menu("E Settings", $"{Variables.MainMenuName}.esettingsmenu");
                {
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useeauto", "Use E")).SetValue(true);
                    Variables.EMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.esettings.useeks", "Use E to Automatically KillSteal")).SetValue(false);
                    {
                        /// <summary>
                        /// The whitelist menu for the E spell.
                        /// </summary>
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

            /// <summary>
            /// The settings menu for the miscellaneous settings.
            /// </summary>
            Variables.MiscMenu = new Menu("Miscellaneous Menu", $"{Variables.MainMenuName}.miscsettingsmenu");
            {
                Variables.MiscMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.miscsettings.noaastealth", "Use Don't AA when Stealthed Logic")).SetValue(false);
            }
            Variables.SettingsMenu.AddSubMenu(Variables.MiscMenu);

            /// <summary>
            /// The drawings menu.
            /// </summary>
            Variables.DrawingsMenu = new Menu("Drawings Menu", $"{Variables.MainMenuName}.drawingsmenu");
            {
                Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.q", "Show Q Range")).SetValue(true);
                Variables.DrawingsMenu.AddItem(new MenuItem($"{Variables.MainMenuName}.drawings.e", "Show E Range")).SetValue(true);
            }
            Variables.Menu.AddSubMenu(Variables.DrawingsMenu);
        }

        /// <summary>
        /// The methods.
        /// </summary>
        public static void SetMethods()
        {
            Game.OnUpdate += Vayne.Game_OnGameUpdate;
            Obj_AI_Base.OnDoCast += Vayne.Obj_AI_Base_OnDoCast;
        }
    }

    /// <summary>
    ///    The targets class.
    /// </summary>
    public class Targets
    {
        public static Obj_AI_Hero Target => TargetSelector.GetTarget(Orbwalking.GetRealAutoAttackRange(null) + 240f, TargetSelector.DamageType.Physical);
        public static IEnumerable<Obj_AI_Base> FarmMinions
        =>
            MinionManager.GetMinions(Orbwalking.GetRealAutoAttackRange(null) + Variables.Q.Range)
                .Where(
                    minions =>
                        minions.Health < ObjectManager.Player.GetAutoAttackDamage(minions));

        public static Obj_AI_Base FarmMinion 
        => 
            Targets.FarmMinions.FirstOrDefault();
    }

    /// <summary>
    ///    The killsteal class.
    /// </summary>
    public class KillSteal
    {
        public static int Damage()
        => 
            Bools.Has2WStacks(Targets.Target) ?
                (int)(ObjectManager.Player.GetSpellDamage(Targets.Target, SpellSlot.E) + ObjectManager.Player.GetSpellDamage(Targets.Target, SpellSlot.W))
                :
                (int)(ObjectManager.Player.GetSpellDamage(Targets.Target, SpellSlot.E));
    }
}
