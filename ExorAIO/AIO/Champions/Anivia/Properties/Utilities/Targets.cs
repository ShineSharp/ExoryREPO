using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Anivia
{
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
                .GetTarget(Variables.R.Range, LeagueSharp.DamageType.Physical);

        /// <summary>
        /// The minion targets.
        /// </summary>
        public static List<Obj_AI_Base> Minions
        => 
            MinionManager
                .GetMinions(ObjectManager.Player.ServerPosition, Variables.W.Range);

        /// <summary>
        /// The minions hit by R.
        /// </summary>
        public static List<Obj_AI_Base> RMinions
        => 
            MinionManager
                .GetMinions(Anivia.RMissile.Position, Variables.R.Width);
    }
}
