namespace ExorKalista
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using SharpDX;
    using SharpDX.Direct3D9;

    using LeagueSharp;
    using LeagueSharp.Common;

    using Orbwalking = SFXTargetSelector.Orbwalking;
    using TargetSelector = SFXTargetSelector.TargetSelector;

    /// <summary>
    /// The logics class.
    /// </summary>
    public class Logics
    {
        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteAuto(EventArgs args)
        {
            /// <summary>
            /// The Q KillSteal Logic,
            /// The Q Immobile Harass Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&
                Variables.Q.GetPrediction(Targets.Target).Hitchance >= HitChance.High &&

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqautoharass").GetValue<bool>()) ||

                (Targets.Target.Health < Variables.Q.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqks").GetValue<bool>()) ||

                (Bools.IsImmobile(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqimmobile").GetValue<bool>())))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction(Targets.Target).UnitPosition);
            }

            /// <summary>
            /// The E before Dying Logic.
            /// </summary>
            if (Variables.E.IsReady() &&

                (HealthPrediction.GetHealthPrediction(ObjectManager.Player, (int)(1500 + Game.Ping / 2f)) <= 0 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useedie").GetValue<bool>()))
            {
                Variables.E.Cast();
            }
            
            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                !ObjectManager.Player.Spellbook.IsCastingSpell &&
                !ObjectManager.Player.IsDashing() &&

                Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useecombo").GetValue<bool>())
            {
                foreach (var unit in HeroManager.Enemies
                    .Where(
                        h =>
                            Bools.IsPerfectRendTarget(h) &&
                            Bools.IsKillableByRend(h)))
                {
                    Variables.E.Cast();
                }
            }

            /// <summary>
            /// The R Lifesaver Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Variables.SoulBound != null &&
                Variables.SoulBound.CountEnemiesInRange(800) > 0 &&
                ObjectManager.Player.Distance(Variables.SoulBound) <= Variables.R.Range &&

                (HealthPrediction.GetHealthPrediction(Variables.SoulBound, (int)(1500 + Game.Ping / 2f)) <= 0 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.userlifesaver").GetValue<bool>()))
            {
                Variables.R.Cast();
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
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.Q.Range) &&
                Variables.Q.GetPrediction(Targets.Target).Hitchance >= HitChance.High &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>()))
            {
                Variables.Q.Cast(Variables.Q.GetPrediction((Obj_AI_Hero)args.Target).UnitPosition);
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteFarm(EventArgs args)
        {
            /// <summary>
            /// The Q Farm Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Variables.Orbwalker.GetTarget().IsValid<Obj_AI_Minion>() &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>()))
            {
                if (Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).MinionsHit > 2 &&
                    Targets.Minions
                    .Count(
                        m =>
                            m.Health < Variables.Q.GetDamage(m)) > 2)
                {
                    Variables.Q.Cast(Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).Position);
                }
            }

            /// <summary>
            /// The E Farm Logic,
            /// The E Harass Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Variables.Orbwalker.GetTarget().IsValid<Obj_AI_Minion>() &&

                (ObjectManager.Player.ManaPercent > ManaManager.NeededEMana &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useefarm").GetValue<bool>()))
            {
                if (Targets.Minions
                    .Count(
                        x =>
                            Bools.IsPerfectRendTarget(x) &&
                            Bools.IsKillableByRend(x)) >= (Targets.ETarget.Any() ? 1 : 2))
                {
                    Variables.E.Cast();
                }
            }

            /// <summary>
            /// The E against Monsters Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Variables.Orbwalker.GetTarget().IsValid<Obj_AI_Minion>() &&
                
                Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useemonsters").GetValue<bool>())
            {
                foreach (var miniontarget in GameObjects.Jungle
                    .Where(
                        m =>
                            (!m.CharData.BaseSkinName.Contains("Mini") || !m.CharData.BaseSkinName.Contains("Crab")) &&
                            Bools.IsPerfectRendTarget(m) &&
                            Bools.IsKillableByRend(m)))
                {
                    Variables.E.Cast();
                }
            }
        }
    }
}
