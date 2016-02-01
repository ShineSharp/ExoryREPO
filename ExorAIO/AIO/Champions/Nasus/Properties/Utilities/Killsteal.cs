using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Nasus
{
    using ExorAIO.Utilities;

    /// <summary>
    /// The killsteal class.
    /// </summary>
    class KillSteal
    {
        public static float GetQDamage(Obj_AI_Base target)
        =>
            (float)(Variables.Q.GetDamage(target) + target.GetBuffCount(""));
    }
}
