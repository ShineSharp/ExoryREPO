using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Kalista
{
    using System.Linq;
    using System.Collections.Generic;
    using ExorAIO.Utilities;
    using TargetSelector = SFXTargetSelector.TargetSelector;

    /// <summary>
    /// The settings class.
    /// </summary>
    class Targets
    {
        /// <summary>
        /// The main hero target.
        /// </summary>
        public static Obj_AI_Hero Target
        =>
            TargetSelector.GetTarget(Variables.Q.Range, LeagueSharp.DamageType.Physical);

        /// <summary>
        /// The hero targets with E stacks on them.
        /// </summary>
        public static IEnumerable<Obj_AI_Hero> HarassableTargets
        =>
            HeroManager.Enemies.Where(h => Bools.IsPerfectRendTarget(h));

        /// <summary>
        /// The minion targets.
        /// </summary>
        public static List<Obj_AI_Base> Minions
        => 
            MinionManager
                .GetMinions(Variables.E.Range);
    }
}
