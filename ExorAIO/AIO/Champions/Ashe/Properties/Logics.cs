using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Ashe
{
    using System;
    using System.Linq;
    using ExorAIO.Utilities;
    using Orbwalking = SFXTargetSelector.Orbwalking;
    using SPrediction;
    using ItemData = LeagueSharp.Common.Data.ItemData;

    /// <summary>
    /// The logics class.
    /// </summary>
    class Logics
    {
        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteQ(EventArgs args)
        {
            /// <summary>
            /// The Q Combo Logic,
            /// The Q Farm Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                ObjectManager.Player.HasBuff("AsheQCastReady") &&
                ((Items.HasItem(3085) &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear) ||
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.auto").IsActive())
            {
                Variables.Q.Cast();
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteAuto(EventArgs args)
        {
            /// <summary>
            /// The W Immobile Harass Logic,
            /// The W KillSteal Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&

                ((Bools.IsImmobile(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wspell.immobile").IsActive()) ||

                (Targets.Target.Health < Variables.W.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wspell.ks").IsActive())))
            {
                Variables.W.SPredictionCast(Targets.Target, HitChance.High);
            }

            /// <summary>
            /// The E Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
               !ObjectManager.Player.IsWindingUp &&
               !ObjectManager.Player.IsRecalling() &&
                ObjectManager.Player.CountEnemiesInRange(1500) == 0 &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededEMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.None &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.auto").IsActive())
            {
                Variables.E.Cast(
                    Variables.Locations
                        .OrderBy(d => ObjectManager.Player.Distance(d))
                        .FirstOrDefault()
                );
            }

            /// <summary>
            /// The R KillSteal Logic,
            /// The R Doublelift Mechanic Logic,
            /// The R Normal Combo Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Targets.Target.IsValidTarget(Variables.R.Range) &&

                ((Targets.Target.Health < Variables.R.GetDamage(Targets.Target) &&
                    (!Variables.W.IsReady() || !Targets.Target.IsValidTarget(Variables.W.Range)) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.ks").IsActive()) ||

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.combo").IsActive() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.whitelist.{Targets.Target.ChampionName.ToLower()}").IsActive()))))
            {
                if (Variables.E.IsReady() &&
                    Targets.Target.Health > Variables.R.GetDamage(Targets.Target) &&
                    !Targets.Target.IsValidTarget(ObjectManager.Player.AttackRange) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.espell.auto").IsActive())
                {
                    Variables.E.SPredictionCast(Targets.Target, HitChance.High);
                }

                Variables.R.SPredictionCast(Targets.Target, HitChance.High);
            }
        }

        /// <summary>
        /// Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void ExecuteModes(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.W.Range) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.combo").IsActive())
            {
                Variables.W.SPredictionCast((Obj_AI_Hero)args.Target, HitChance.High);
            }
        }

        /// <summary>
        /// Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void ExecuteFarm(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            /// The W LaneClear Logic,
            /// The W JungleClear Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.farm").IsActive())
            {
                if (Variables.W.GetLineFarmLocation(Targets.Minions, Variables.W.Width).MinionsHit >= 3)
                {
                    Variables.W.Cast(Variables.W.GetLineFarmLocation(Targets.Minions, Variables.W.Width).Position);
                }
                else if (Targets.JungleMinions.Any())
                {
                    Variables.W.Cast((Targets.JungleMinions.FirstOrDefault()).Position);
                }
            }
        }
    }
}
