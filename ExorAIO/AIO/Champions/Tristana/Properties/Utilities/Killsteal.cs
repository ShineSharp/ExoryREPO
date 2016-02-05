using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Tristana
{
    using ExorAIO.Utilities;

    /// <summary>
    /// The killsteal class.
    /// </summary>
    class KillSteal
    {
        public static float Damage(Obj_AI_Hero target)
        {
            float dmg = 0f;

            if (target.HasBuff("TristanaECharge"))
            {
                dmg += Variables.E.GetDamage(target);
            }
            
            if (Variables.R.IsReady())
            {
                dmg += Variables.R.GetDamage(target);
            }
            
            return dmg;
        }
    }
}
