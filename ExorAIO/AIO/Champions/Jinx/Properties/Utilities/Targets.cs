using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Jinx
{
    using ExorAIO.Utilities;
    using TargetSelector = SFXTargetSelector.TargetSelector;

    /// <summary>
    /// The targets class.
    /// </summary>
    class Targets
    {
        /// <summary>
        /// The main hero target.
        /// </summary>
        public static Obj_AI_Hero Target
        =>
            TargetSelector
                .GetTarget(Variables.W.Range, LeagueSharp.DamageType.Physical);
    }
}
