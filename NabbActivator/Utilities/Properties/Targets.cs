using LeagueSharp;
using LeagueSharp.Common;

namespace NabbActivator
{
    using System.Linq;

    /// <summary>
    /// The targets.
    /// </summary>
    class Targets
    {
        /// <summary>
        /// The main enemy target.
        /// </summary>
        public static Obj_AI_Hero Target
        =>
            TargetSelector.GetTarget(1250f, TargetSelector.DamageType.Physical);

        /// <summary>
        /// The main ally target.
        /// </summary>
        public static Obj_AI_Hero Ally
        =>
            HeroManager.Allies
                .Find(a =>
                    !a.IsMe &&
                    Bools.HasNoProtection(a) &&
                    a.IsValidTarget(850f, false));

        /// <summary>
        /// The main minion target.
        /// </summary>
        public static Obj_AI_Base Minion
        =>
            MinionManager
                .GetMinions(ObjectManager.Player.ServerPosition, 1200f, MinionTypes.Ranged, MinionTeam.Ally, MinionOrderTypes.MaxHealth)
                .First();
    }
}
