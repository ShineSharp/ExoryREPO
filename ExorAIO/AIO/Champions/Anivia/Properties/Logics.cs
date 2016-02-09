using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Anivia
{
    using System;
    using System.Linq;
    using ExorAIO.Utilities;
    using Orbwalking = SFXTargetSelector.Orbwalking;
    using SPrediction;

    /// <summary>
    /// The logics class.
    /// </summary>
    class Logics
    {
        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteManager(EventArgs args)
        {
            /// <summary>
            /// The Q Missile Manager.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Anivia.QMissile != null &&
                ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Q).ToggleState != 1 &&
                
                ((Targets.QMinions.Count() >= 2 &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear) ||

                (Anivia.QMissile.Position.CountEnemiesInRange(100f) > 0 &&
                    Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.LaneClear)))
            {
                Variables.Q.Cast();
            }

            /// <summary>
            /// The R Stacking Manager.
            /// </summary>
            if (ObjectManager.Player.InFountain() &&
                Bools.HasTear(ObjectManager.Player) &&
                ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).ToggleState == 1 &&
                Variables.Menu.Item($"{Variables.MainMenuName}.misc.tear").IsActive())
            {
                Variables.R.Cast(Game.CursorPos);
            }

            /// <summary>
            /// The R Missile Manager.
            /// </summary>
            if (Variables.R.IsReady() &&
                Anivia.RMissile != null &&
                !ObjectManager.Player.InFountain() &&
                ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).ToggleState != 1 &&

                ((Targets.RMinions.Count() < 2 &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear) ||

                (Anivia.RMissile.Position.CountEnemiesInRange(Variables.R.Width) < 1 &&
                    Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.LaneClear)))
            {
                Variables.R.Cast();
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteAuto(EventArgs args)
        {
            /// <summary>
            /// The Wall Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                ObjectManager.Player
                    .Distance(Targets.Target.ServerPosition
                    .Extend(Targets.Target.ServerPosition, ObjectManager.Player.Distance(Targets.Target) + Targets.Target.BoundingRadius)) < Variables.W.Range &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.combo").IsActive() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.whitelist.{Targets.Target.ChampionName.ToLower()}").IsActive())
            {
                Variables.W.Cast(ObjectManager.Player.ServerPosition.Extend(Targets.Target.ServerPosition, ObjectManager.Player.Distance(Targets.Target) + 10f));
                return;
            }

            /// <summary>
            /// The E Combo Logic,
            /// The E KillSteal Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Targets.Target.IsValidTarget(Variables.E.Range) &&

                ((Variables.E.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.espell.ks").IsActive()) ||

                (Targets.Target.HasBuffOfType(BuffType.Slow) &&
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).ToggleState != 1 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").IsActive())))
            {
                Variables.E.CastOnUnit(Targets.Target);
                return;
            }

            /// <summary>
            /// The R KillSteal Logic,
            /// The R Combo Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Targets.Target.IsValidTarget(Variables.R.Range) &&

                ((Targets.Target.Health < Variables.R.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.ks").IsActive()) ||

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.combo").IsActive())))
            {
                Variables.R.SPredictionCast(Targets.Target, HitChance.VeryHigh);
                return;
            }

            /// <summary>
            /// The Q Combo Logic.
            /// The Q KillSteal.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&
                ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Q).ToggleState == 1 &&
                ObjectManager.Player.Distance(Variables.Q.GetSPrediction(Targets.Target).UnitPosition.To3D()) < Variables.Q.Range - 100 &&

                ((Variables.Q.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").IsActive()) ||

                ((ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).Level < 1 ?
                    Variables.W.IsReady() && Variables.E.IsReady():
                    !Variables.W.IsReady() && !Variables.E.IsReady()) &&
                    
                    Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").IsActive())))
            {
                Variables.Q.SPredictionCast(Targets.Target, HitChance.VeryHigh);
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteFarm(EventArgs args)
        {
            /// <summary>
            /// The R LaneClear Logic,
            /// The R JungleClear Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededRMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).ToggleState == 1 &&
                (Variables.R.GetCircularFarmLocation(Targets.Minions, Variables.R.Width).MinionsHit >= 4 || Targets.JungleMinions.Any()) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.farm").IsActive())
            {
                Variables.R.Cast(Variables.R.GetCircularFarmLocation(Targets.Minions, Variables.R.Width).Position);
                return;
            }

            /// <summary>
            /// The Q LaneClear Logic,
            /// The Q JungleClear Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Q).ToggleState == 1 &&
                (Variables.W.GetLineFarmLocation(Targets.Minions, Variables.W.Width).MinionsHit >= 3 || Targets.JungleMinions.Any()) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.farm").IsActive())
            {
                Variables.Q.Cast(Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.W.Width).Position);
            }
        }
    }
}
