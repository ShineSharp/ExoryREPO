using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Akali
{
    using System;
    using System.Linq;
    using ExorAIO.Utilities;
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
            /// The Q Combo Logic,
            /// The Q KillSteal Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").GetValue<bool>()) ||

                (Targets.Target.Health < Variables.Q.GetDamage(Targets.Target) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.ks").GetValue<bool>()) ||

                (!Targets.Target.UnderTurret() &&
                    ObjectManager.Player.ManaPercent > ManaManager.NeededQMana &&
                    Variables.Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.harass").GetValue<bool>())))
            {
                Variables.Q.CastOnUnit(Targets.Target);
            }

            /// <summary>
            /// The R Combo Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                Targets.Target.IsValidTarget(Variables.Q.Range) &&

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.combo").GetValue<bool>() &&
                    (!Targets.Target.UnderTurret() || !Variables.Menu.Item($"{Variables.MainMenuName}.misc.safecheck").GetValue<bool>()) &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.whitelist.{Targets.Target.ChampionName.ToLower()}").GetValue<bool>()) ||

                (Targets.Target.Health < Variables.R.GetDamage(Targets.Target) * 2 &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.rspell.ks").GetValue<bool>())))
            {
                Variables.R.CastOnUnit(Targets.Target);
            }

            /// <summary>
            /// The E KillSteal Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                Targets.Target.IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)) &&
                Targets.Target.Health < Variables.E.GetDamage(Targets.Target) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.ks").GetValue<bool>())
            {
                Variables.E.Cast();
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
                ((Obj_AI_Hero)args.Target).IsValidTarget(Orbwalking.GetRealAutoAttackRange(Targets.Target)) &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").GetValue<bool>())
            {
                Variables.E.Cast();
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
            /// The E Farm Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededEMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                (Targets.Minions.Count() >= 3 ||
                    GameObjects.Jungle.Contains((Obj_AI_Minion)args.Target)) &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.farm").GetValue<bool>())
            {
                Variables.E.Cast();
            }
        }
    }
}
