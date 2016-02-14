using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Vayne
{
    using System;
    using System.Linq;
    using ExorAIO.Utilities;
    using Orbwalking = SFXTargetSelector.Orbwalking;
    using TargetSelector = SFXTargetSelector.TargetSelector;

    /// <summary>
    /// The champion class.
    /// </summary>
    class Vayne
    {
        /// <summary>
        /// Called when the game loads itself.
        /// </summary>
        public void OnLoad()
        {
            Menus.Initialize();
            Spells.Initialize();
            Methods.Initialize();
            Drawings.Initialize();
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void OnUpdate(EventArgs args)
        {
            if (!ObjectManager.Player.IsDead &&  
                Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.None)
            {
                /// <summary>
                /// The Focus Logic (W Stacks).
                /// </summary>
                foreach (var tg in HeroManager.Enemies
                    .Where(h =>
                        h.HasBuff("vaynesilvereddebuff") &&
                        h.IsValidTarget(ObjectManager.Player.AttackRange)))
                {
                    TargetSelector.Selected.Target = tg ?? null;
                    Variables.Orbwalker.ForceTarget(tg ?? null);
                }

                if (Targets.Target != null &&
                    Targets.Target.IsValid &&
                    !Targets.Target.IsInvulnerable &&
                    !Bools.IsSpellShielded(Targets.Target))
                {
                    Logics.ExecuteAuto(args);
                }
            }
        }

        /// <summary>
        /// Called on-attack request.
        /// </summary>
        /// <param name="unit">The sender.</param>
        /// <param name="target">The target.</param>
        public static void OnAttack(AttackableUnit unit, AttackableUnit target)
        {
            if (unit.IsMe &&
                target.IsValid<Obj_AI_Hero>() &&
                !((Obj_AI_Hero)target).IsInvulnerable &&
                !Bools.IsSpellShielded((Obj_AI_Hero)target) &&
                Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.None &&
                Variables.Menu.Item($"{Variables.MainMenuName}.misc.resets").IsActive())
            {
                Logics.ExecuteBetaModes(unit, target);
            }
        }

        /// <summary>
        /// Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void OnDoCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe &&
                Orbwalking.IsAutoAttack(args.SData.Name) &&
                Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.None)
            {
                if (args.Target.IsValid<Obj_AI_Hero>() &&
                    !((Obj_AI_Hero)args.Target).IsInvulnerable &&
                    !Bools.IsSpellShielded((Obj_AI_Hero)args.Target) &&
                    !Variables.Menu.Item($"{Variables.MainMenuName}.misc.resets").IsActive())
                {
                    Logics.ExecuteModes(sender, args);
                }
                else if (args.Target.IsValid<Obj_AI_Minion>())
                {
                    Logics.ExecuteFarm(sender, args);
                }
            }
        }

        /// <summary>
        /// Called on interruptable spell.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Interrupter2.InterruptableTargetEventArgs"/> instance containing the event data.</param>
        public static void OnInterruptableTarget(Obj_AI_Hero sender, Interrupter2.InterruptableTargetEventArgs args)
        {
            if (Variables.E.IsReady() &&
                !Bools.IsSpellShielded(sender) &&
                sender.IsValidTarget(Variables.E.Range) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.ir").IsActive())
            {
                Variables.E.CastOnUnit(sender);
            }
        }

        /// <summary>
        /// Called on gapclosing spell.
        /// </summary>
        /// <param name="gapcloser">The <see cref="ActiveGapcloser"/> instance containing the event data.</param>
        public static void OnEnemyGapcloser(ActiveGapcloser gapcloser)
        {
            if (Variables.E.IsReady() &&
                !Bools.IsSpellShielded(gapcloser.Sender) &&
                ObjectManager.Player.Distance(gapcloser.End) <= Variables.E.Range &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.gp").IsActive())
            {
                Variables.E.CastOnUnit(gapcloser.Sender);
            }
        }
    }
}
