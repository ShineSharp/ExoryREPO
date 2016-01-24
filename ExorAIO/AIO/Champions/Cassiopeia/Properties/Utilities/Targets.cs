namespace ExorAIO.Champions.Cassiopeia
{
    using System.Collections.Generic;
    using LeagueSharp;
    using LeagueSharp.Common;
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
                .GetTarget(Variables.W.Range, LeagueSharp.DamageType.Magical);

        /// <summary>
        /// The minion targets.
        /// </summary>
        public static List<Obj_AI_Base> Minions
        => 
            MinionManager
                .GetMinions(ObjectManager.Player.ServerPosition, Variables.E.Range);

        /// <summary>
        /// The R Range targets.
        /// </summary>
        public static IEnumerable<Obj_AI_Hero> RTargets
        =>
            HeroManager.Enemies
                .FindAll(enemy => 
                    enemy.IsValidTarget(Variables.R.Range) &&
                    enemy.IsFacing(ObjectManager.Player));
    }
}
