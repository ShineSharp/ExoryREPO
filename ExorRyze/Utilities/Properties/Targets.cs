using LeagueSharp;
using LeagueSharp.Common;

namespace ExorRyze
{
    using System.Collections.Generic;
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
                .GetTarget(Variables.Q.Range, LeagueSharp.DamageType.Magical);

        /// <summary>
        /// The minions target.
        /// </summary>
        public static List<Obj_AI_Base> Minions
        =>
            MinionManager
                .GetMinions(ObjectManager.Player.ServerPosition, Variables.Q.Range);
    }
}
