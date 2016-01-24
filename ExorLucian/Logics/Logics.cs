namespace ExorLucian
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using SharpDX;

    using Orbwalking = SFXTargetSelector.Orbwalking;

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
                if (!Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)) &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqautoharass").GetValue<bool>())
                {
                    foreach (var tgminion in from target in ObjectManager.Player.GetEnemiesInRange(Variables.Q.Range + 600f)
                        where
                            Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.qwhitelist.{target.ChampionName.ToLower()}").GetValue<bool>()
                        select
                            Variables.Q.GetCollision(ObjectManager.Player.Position.To2D(),new List<Vector2> {target.Position.To2D()})
                                .FirstOrDefault(
                                    minion =>
                                        minion.IsValidTarget(Variables.Q.Range))

                        into tgminion
                        where tgminion != null
                        select tgminion)
                    {
                        Variables.Q.CastOnUnit(tgminion);
                    }
                }

                /// <summary>
                /// The Q KillSteal Logic.
                /// </summary>
                if (Targets.Target.IsValidTarget(Variables.Q.Range) &&

                    (Variables.Q.GetDamage(Targets.Target) > Targets.Target.Health &&
                        Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqks").GetValue<bool>()))
                {
                    Variables.Q.CastOnUnit(Targets.Target);
                }
            }

            /// <summary>
            /// The W KillSteal Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                (!Variables.Q.IsReady() || !Targets.Target.IsValidTarget(Variables.Q.Range)) &&
                Targets.Target.IsValidTarget(Variables.W.Range) &&
                Variables.W.GetPrediction(Targets.Target).Hitchance >= HitChance.High &&

                (Variables.W.GetDamage(Targets.Target) > Targets.Target.Health &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewks").GetValue<bool>()))
            {
                Variables.W.Cast(Variables.W.GetPrediction(Targets.Target).UnitPosition);
            }
            
            /// <summary>
            /// The E Gap Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                (Targets.Target.IsValidTarget(Variables.E.Range) && !Targets.Target.IsValidTarget(Variables.Q.Range)) &&
                ObjectManager.Player.CountEnemiesInRange(1500) < 2 &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeauto").GetValue<bool>()))
            {
                Variables.E.Cast(Game.CursorPos);
            }

            /// <summary>
            /// The Semi-Automatic R Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                ((Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.usersemiauto").GetValue<KeyBind>().Active && !ObjectManager.Player.HasBuff("LucianR")) ||
                (!Variables.Menu.Item($"{Variables.MainMenuName}.rsettings.usersemiauto").GetValue<KeyBind>().Active && ObjectManager.Player.HasBuff("LucianR"))))
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
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.E.Range) &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useeauto").GetValue<bool>()))
            {
                Variables.E.Cast(Game.CursorPos);
                return;
            }

            /// <summary>
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.Q.Range) &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqcombo").GetValue<bool>()))
            {
                Variables.Q.CastOnUnit((Obj_AI_Hero)args.Target);
                return;
            }

            /// <summary>
            /// The W Combo Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ((Obj_AI_Hero)args.Target).IsValidTarget(Variables.W.Range) &&

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewcombo").GetValue<bool>()))
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
                GameObjects.Jungle.Contains((Obj_AI_Minion)Variables.Orbwalker.GetTarget()) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.esettings.useejc").GetValue<bool>())
            {
                Variables.E.Cast(Game.CursorPos);
                return;
            }

            /// <summary>
            /// The Q Farm Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                (Variables.Q.GetLineFarmLocation(Targets.Minions, 60).MinionsHit >= 3 ||
                    GameObjects.Jungle.Contains((Obj_AI_Minion)Variables.Orbwalker.GetTarget())) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.qsettings.useqfarm").GetValue<bool>())
            {
                Variables.Q.CastOnUnit((Obj_AI_Minion)Variables.Orbwalker.GetTarget());
                return;
            }

            /// <summary>
            /// The W Farm Logic.
            /// </summary>
            if (Variables.W.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededWMana &&
                (Variables.W.GetCircularFarmLocation(Targets.Minions, Variables.W.Width).MinionsHit >= 2 ||
                    GameObjects.Jungle.Contains((Obj_AI_Minion)Variables.Orbwalker.GetTarget())) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.wsettings.usewfarm").GetValue<bool>())
            {
                Variables.W.Cast(((Obj_AI_Minion)Variables.Orbwalker.GetTarget()).Position);
            }
        }
    }
}
