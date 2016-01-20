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
            /// The Soulbound declaration.
            /// </summary>
            Variables.SoulBound = HeroManager.Allies
                .Find(
                    h =>
                        h.Buffs
                .Any(
                    b =>
                        b.Caster.IsMe &&
                        b.Name.Contains("kalistacoopstrikeally")));

            /// <summary>
            /// The Target preference.
            /// </summary>
            if (TargetSelector.Weights.GetItem("low-health") != null)
            {
                TargetSelector.Weights.GetItem("low-health").ValueFunction = hero => hero.Health - Variables.GetPerfectRendDamage(hero);
                TargetSelector.Weights.GetItem("low-health").Tooltip = "Low Health (Health < Rend Damage) = Higher Weight";
                TargetSelector.Weights.Register(
                    new TargetSelector.Weights.Item(
                        "w-stack", "W Stack", 10, false, hero => hero.HasBuff("kalistacoopstrikemarkally") ? 1 : 0,
                        "Has W Debuff = Higher Weight"));
            }

            /// <summary>
            /// The Q KillSteal Logic,
            /// The Q Immobile Harass Logic.
            /// </summary>
            if (Targets.Target != null &&
                Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&
                Variables.Q.GetPrediction(Targets.Target).Hitchance >= HitChance.High &&

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqautoharass").GetValue<bool>()) ||

<<<<<<< HEAD:ExorKalista/Logics/Logics.cs
                (Targets.Target.Health < Variables.Q.GetDamage(Targets.Target) &&
=======
                (Targets.Target.Health <= ObjectManager.Player.CalcDamage(Targets.Target, LeagueSharp.Common.Damage.DamageType.Physical, Variables.Q.GetDamage(Targets.Target)) &&
>>>>>>> parent of 90bc5d9... ExorKalista: 6.1.0.1 - Rework.:ExorKalista/Utilities/Logics/Logics.cs
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

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useecombo").GetValue<bool>()))
            {
                foreach (var unit in HeroManager.Enemies
                    .Where(
                        h =>
                            Bools.IsPerfectRendTarget(h) &&
                            Bools.IsKillableRendTarget(h)))
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
                
                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>()))
            {
                if (Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).MinionsHit > 2 &&
                    Targets.Minions
                    .Count(
<<<<<<< HEAD:ExorKalista/Logics/Logics.cs
                        m =>
                            m.Health < Variables.Q.GetDamage(m)) > 2)
=======
                        m => 
                            m != null &&
                            m.IsValidTarget(Variables.Q.Range) &&
                            m.Health < ObjectManager.Player.CalcDamage(m, LeagueSharp.Common.Damage.DamageType.Physical, Variables.Q.GetDamage(m))) > 2)
>>>>>>> parent of 90bc5d9... ExorKalista: 6.1.0.1 - Rework.:ExorKalista/Utilities/Logics/Logics.cs
                {
                    Variables.Q.Cast(Variables.Q.GetLineFarmLocation(Targets.Minions, Variables.Q.Width).Position);
                }
            }

            /// <summary>
            /// The E Farm Logic,
            /// The E Harass Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
<<<<<<< HEAD:ExorKalista/Logics/Logics.cs
                !ObjectManager.Player.IsDashing() &&
                !ObjectManager.Player.Spellbook.IsCastingSpell &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededEMana)
            {
                if (Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useefarm").GetValue<bool>() ||
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useemonsters").GetValue<bool>() ||
                    (Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeharass").GetValue<bool>() &&
                        Variables.Menu.Item($"{Variables.MainMenuName}.esettings.ewhitelist.{Targets.ETarget.FirstOrDefault().ChampionName.ToLower()}").GetValue<bool>()))
=======
                ObjectManager.Player.ManaPercent > ManaManager.NeededEMana &&
                Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useefarm").GetValue<bool>())
            {
                if (Targets.Minions
                    .Count(
                        x =>
                            Bools.IsPerfectRendTarget(x) &&
                            Bools.IsKillableRendTarget(x)) >= (Targets.ETarget.Any() ? 1 : 2))
                {
                    Variables.E.Cast();
                }
            }

            /// <summary>
            /// The E against Monsters Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useemonsters").GetValue<bool>())
            {
                foreach (var miniontarget in GameObjects.Jungle
                    .Where(
                        m =>
                            !m.CharData.BaseSkinName.Contains("Mini") &&
                            Bools.IsPerfectRendTarget(m) &&
                            Bools.IsKillableRendTarget(m)))
>>>>>>> parent of 90bc5d9... ExorKalista: 6.1.0.1 - Rework.:ExorKalista/Utilities/Logics/Logics.cs
                {
                    if (ObjectManager.Get<Obj_AI_Minion>()
                        .Count(
                            x =>
                                Bools.IsPerfectRendTarget(x) &&
                                Bools.IsKillableByRend(x)) >=
                                    (Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useefarm").GetValue<bool>() &&
                                    !GameObjects.Jungle.Contains((Obj_AI_Minion)Variables.Orbwalker.GetTarget()) ? 2 : 1))
                    {
                        Variables.E.Cast();
                    }
                }
            }
        }
    }
}
