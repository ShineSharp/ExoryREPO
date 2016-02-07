using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Renekton
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
                .GetTarget(Variables.E.Range + Orbwalking.GetRealAutoAttackRange(null), LeagueSharp.DamageType.Physical);

        /// <summary>
        /// The minion targets.
        /// </summary>      
        public static List<Obj_AI_Base> Minions
        => 
            MinionManager
                .GetMinions(ObjectManager.Player.ServerPosition, Variables.Q.Range);

        /// <summary>
        /// The jungle minions targets.
        /// </summary>
        public static List<Obj_AI_Base> JungleMinions
        => 
            MinionManager
                .GetMinions(ObjectManager.Player.ServerPosition, Variables.Q.Range, MinionTypes.All, MinionTeam.Neutral, MinionOrderTypes.MaxHealth);
    }
}