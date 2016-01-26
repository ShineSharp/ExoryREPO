namespace ExorKalista
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    using LeagueSharp;
    using LeagueSharp.SDK.Core.Wrappers.Damages;

    /// <summary>
    /// The Damage class.
    /// </summary>
    class DamageManager
    {
        /// <summary>
        /// Gets the perfect damage reduction from sources.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>
        /// The damage dealt against all the sources.
        /// </returns>
        public static float GetPerfectRendDamage(Obj_AI_Base target)
        {
            float RendDamage = 
                (float)ObjectManager.Player.GetSpellDamage(target, SpellSlot.E) +
                (float)ObjectManager.Player.GetSpellDamage(target, SpellSlot.E, Damage.DamageStage.Buff);

            float healthDebuffer = 0f;

            if (target.Type.Equals(GameObjectType.obj_AI_Hero))
            {
                /// <summary>
                /// Gets the predicted reduction from Blitzcrank Shield.
                /// </summary>
                if (((Obj_AI_Hero)target).ChampionName.Equals("Blitzcrank") &&
                    !((Obj_AI_Hero)target).HasBuff("BlitzcrankManaBarrierCD"))
                {
                    healthDebuffer += target.Mana / 2;
                }
            }

            return (float)(RendDamage - (target.PhysicalShield + target.HPRegenRate) - healthDebuffer);
        }
    }
}
