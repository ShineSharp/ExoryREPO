using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Darius
{
    using ExorAIO.Utilities;

    /// <summary>
    /// The killsteal class.
    /// </summary>
    class KillSteal
    {
        public static float GetRDamage(Obj_AI_Hero target)
        =>
            (float)(Variables.R.GetDamage(target) * (1 + 0.20 * target.GetBuffCount("dariushemo")));
    }
}
