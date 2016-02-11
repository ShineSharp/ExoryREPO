using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Jhin
{
    using ExorAIO.Utilities;

    /// <summary>
    /// The killsteal class.
    /// </summary>
    class KillSteal
    {
        public static double GetQDamage(Obj_AI_Base target)
        =>
            ObjectManager.Player
                .CalcDamage(
                    target,
                    Damage.DamageType.Physical,
                    35 + Variables.Q.Level * 25 + 0.4 * ObjectManager.Player.FlatPhysicalDamageMod);

        public static double GetWDamage(Obj_AI_Base target)
        =>
            ObjectManager.Player
                .CalcDamage(
                    target,
                    Damage.DamageType.Physical,
                    55 + Variables.W.Level * 35 + 0.7 * ObjectManager.Player.FlatPhysicalDamageMod);

        public static double GetRDamage(Obj_AI_Base target)
        =>
            ObjectManager.Player
                .CalcDamage(
                    target,
                    Damage.DamageType.Physical,
                    ( -25 + 75 * Variables.R.Level + 0.2 * ObjectManager.Player.FlatPhysicalDamageMod) * (1 + (100 - target.HealthPercent) * 0.02));
    }
}
