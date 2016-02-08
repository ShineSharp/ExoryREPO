using LeagueSharp;
using LeagueSharp.Common;

namespace ExorLucian
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using SharpDX;
    using Orbwalking = SFXTargetSelector.Orbwalking;

    /// <summary>
    /// The logics class.
    /// </summary>
    class Logics
    {
        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteAuto(EventArgs args)
        {
            /// <summary>
            /// The R Orbwalking.
            /// </summary>
            if (ObjectManager.Player.HasBuff("LucianR"))
            {
                ObjectManager.Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
            }

            /// <summary>
            /// The Q Logics.
            /// </summary>
            if (Variables.Q.IsReady())
            {
                /// <summary>
                /// The Q AutoHarass Logic.
                /// </summary>
                if (!Targets.Target.IsValidTarget(Variables.Q.Range + 50f) &&
                    Targets.Target.IsValidTarget(Variables.Q.Range + 600f) &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Mixed ||
                        Targets.Target.Health < Variables.Q.GetDamage(Targets.Target)) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.harass").IsActive())
                {
                    foreach (var tgminion in from target in ObjectManager.Player.GetEnemiesInRange(Variables.Q.Range + 600f)
                        where Variables.Menu.Item($"{Variables.MainMenuName}.qspell.whitelist.{target.ChampionName.ToLower()}").IsActive()
                        select Variables.Q.GetCollision(ObjectManager.Player.Position.To2D(),new List<Vector2> {target.Position.To2D()})
                            .FirstOrDefault(minion => minion.IsValidTarget(Variables.Q.Range))

                        into tgminion
                        where tgminion != null
                        select tgminion)
                    {
                        Variables.Q.CastOnUnit(tgminion);
                        return;
                    }
                }

                /// <summary>
                /// The Q KillSteal Logic.
                /// </summary>
                if (Targets.Target.IsValidTarget(Variables.Q.Range) &&
                    Variables.Q.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").IsActive())
                {
                    Variables.Q.CastOnUnit(Targets.Target);
                }
            }

            /// <summary>
            /// The W KillSteal Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&
                Variables.W.GetDamage(Targets.Target) > Targets.Target.Health &&
                Variables.W.GetPrediction(Targets.Target).Hitchance >= HitChance.High &&
                (!Variables.Q.IsReady() || !Targets.Target.IsValidTarget(Variables.Q.Range)) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.ks").IsActive())
            {
                Variables.W.Cast(Variables.W.GetPrediction(Targets.Target).UnitPosition);
            }
            
            /// <summary>
            /// The E Gap Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Targets.Target.IsValidTarget(Variables.E.Range) &&
                !Targets.Target.IsValidTarget(Variables.Q.Range) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                (!ObjectManager.Player.UnderTurret() ||
                    Targets.Target.Health < ObjectManager.Player.GetAutoAttackDamage(Targets.Target)) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.auto").IsActive())
            {
                Variables.E.Cast(Game.CursorPos);
            }

            /// <summary>
            /// The Semi-Automatic R Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.boolrsa").IsActive() &&
                ((Variables.Menu.Item($"{Variables.MainMenuName}.rspell.keyrsa").GetValue<KeyBind>().Active && !ObjectManager.Player.HasBuff("LucianR")) ||
                (!Variables.Menu.Item($"{Variables.MainMenuName}.rspell.keyrsa").GetValue<KeyBind>().Active && ObjectManager.Player.HasBuff("LucianR"))))
            {
                Variables.R.Cast(Variables.R.GetPrediction(Targets.Target).UnitPosition);
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
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)) &&
                (!ObjectManager.Player.UnderTurret() ||
                    ((Obj_AI_Hero)args.Target).Health < ObjectManager.Player.GetAutoAttackDamage((Obj_AI_Hero)args.Target)) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.auto").IsActive())
            {
                if (ObjectManager.Player.CountEnemiesInRange(1500) >= 2 ||
                    ObjectManager.Player.Distance(Game.CursorPos) < Orbwalking.GetRealAutoAttackRange(null))
                {
                    Variables.E.Cast(ObjectManager.Player.Position.Extend(Game.CursorPos, 50));
                }
                else if (ObjectManager.Player.Distance(Game.CursorPos) > Orbwalking.GetRealAutoAttackRange(null))
                {
                    Variables.E.Cast(Game.CursorPos);
                }
                return;
            }

            /// <summary>
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.Q.Range) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").IsActive())
            {
                Variables.Q.CastOnUnit((Obj_AI_Hero)args.Target);
                return;
            }

            /// <summary>
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.W.Range) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.combo").IsActive())
            {
                Variables.W.Cast(Variables.W.GetPrediction((Obj_AI_Hero)args.Target).UnitPosition);
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
            /// The E JungleClear Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededEMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Targets.JungleMinions.Any() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.jcfarm").IsActive())
            {
                Variables.E.Cast(Game.CursorPos);
                return;
            }

            /// <summary>
            /// The Q Farm Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                (Variables.Q.GetLineFarmLocation(Targets.Minions, 60f).MinionsHit >= 2 || Targets.JungleMinions.Any()) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.farm").IsActive())
            {
                Variables.Q.CastOnUnit((Obj_AI_Minion)Variables.Orbwalker.GetTarget());
                return;
            }

            /// <summary>
            /// The W Farm Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                (Variables.W.GetCircularFarmLocation(Targets.Minions, Variables.W.Width).MinionsHit >= 1 || Targets.JungleMinions.Any()) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.farm").IsActive())
            {
                if (Variables.W.GetCircularFarmLocation(Targets.Minions, Variables.W.Width).MinionsHit >= 2)
                {
                    Variables.W.Cast(Variables.W.GetCircularFarmLocation(Targets.Minions, Variables.W.Width).Position);
                }
                else if (Targets.JungleMinions.Any())
                {
                    Variables.W.Cast((Targets.JungleMinions.FirstOrDefault()).Position);
                }
            }
        }
    }
}
