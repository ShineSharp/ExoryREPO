using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Vayne
{
    using ExorAIO.Utilities;

    /// <summary>
    /// The killsteal class.
    /// </summary>
    class KillSteal
    {
        /// <summary>
        /// Gets the Killsteal damage.
        /// </summary>
        public static float GetDamage(Obj_AI_Hero target)
        {
            float dmg = 0f;

            if (target.GetBuffCount("vaynesilvereddebuff") == 2)
            {
                dmg += Variables.W.GetDamage(target);
            }
            
            if (Variables.E.IsReady())
            {
                dmg += Variables.E.GetDamage(target);
            }
            
            return dmg;
        }
    }
}
