using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Jinx
{
    using System.Linq;
    using System.Collections.Generic;
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
                .GetTarget(Variables.W.Range + 200f, LeagueSharp.DamageType.Physical);
    }
}
