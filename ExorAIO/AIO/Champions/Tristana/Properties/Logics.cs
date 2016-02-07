using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Tristana
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
            /// The R KillSteal Logic.
            /// </summary>
            if (Variables.R.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(Variables.R.Range) &&
                Targets.Target.Health > Targets.Target.MaxHealth/3 &&
                KillSteal.Damage(Targets.Target) > Targets.Target.Health &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.ks").GetValue<bool>())
            {
                Variables.R.CastOnUnit(Targets.Target);
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteModes(EventArgs args)
        {
            /// <summary>
            /// The Q Combo Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&
                ObjectManager.Player.IsWindingUp &&
                (Targets.Target.HasBuff("TristanaECharge") || !Variables.E.IsReady()) &&

                ((Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.combo").GetValue<bool>()) ||

                (Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.qspell.farm").GetValue<bool>())))
            {
                Variables.Q.Cast();
            }

            /// <summary>
            /// The E Combo Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                Targets.Target.IsValidTarget(Variables.E.Range) &&
                Targets.Target.Health > Targets.Target.MaxHealth/6 &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.combo").GetValue<bool>() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.whitelist.{Targets.Target.ChampionName.ToLower()}").GetValue<bool>())
            {
                Variables.E.CastOnUnit(Targets.Target);
            }
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteFarm(EventArgs args)
        {
            /// <summary>
            /// The E LaneClear Logic,
            /// The E against Towers Logic,
            /// The E JungleClear Logic.
            /// </summary>
            if (Variables.E.IsReady() &&
                !ObjectManager.Player.IsWindingUp &&
                ObjectManager.Player.ManaPercent > ManaManager.NeededEMana &&
                Variables.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear &&
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.farm").GetValue<bool>())
            {
                if (GameObjects.Minions
                    .Count(units =>
                        units.IsValidTarget(Variables.E.Range) &&
                        units.Distance(Targets.Minions?.FirstOrDefault(), false) < 150f) > 2)
                {
                    Variables.E.CastOnUnit(Targets.Minions?.FirstOrDefault());
                }
                else if (Variables.Orbwalker.GetTarget().IsValid<Obj_AI_Turret>())
                {
                    Variables.E.CastOnUnit((Obj_AI_Turret)Variables.Orbwalker.GetTarget());
                }
                else if (Targets.JungleMinions.Any())
                {
                    Variables.E.CastOnUnit(Targets.JungleMinions.FirstOrDefault());
                }
            }
        }
    }
}
