using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Evelynn
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
                .GetTarget(Variables.Q.Range, LeagueSharp.DamageType.Physical);

        /// <summary>
        /// The minions target.
        /// </summary>
        public static List<Obj_AI_Base> Minions
        => 
            MinionManager
                .GetMinions(ObjectManager.Player.ServerPosition, Variables.E.Range);

        /// <summary>
        /// The jungle minions targets.
        /// </summary>
        public static List<Obj_AI_Base> JungleMinions
        => 
            MinionManager
                .GetMinions(ObjectManager.Player.ServerPosition, Variables.E.Range, MinionTypes.All, MinionTeam.Neutral, MinionOrderTypes.MaxHealth);

        /// <summary>
        /// The R Range targets.
        /// </summary>
        public static IEnumerable<Obj_AI_Hero> RTargets
        =>
            HeroManager.Enemies
                .FindAll(enemy => 
                    enemy.IsValidTarget(Variables.R.Range));
    }
}
