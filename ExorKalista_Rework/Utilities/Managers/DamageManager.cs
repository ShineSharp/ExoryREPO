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
            /*
            float healthDebuffer = 0f;
            

            /// <summary>
            /// Gets the reduction from the exhaust spell.
            /// </summary>
            /// <returns>
            /// You deal 40% of you total damage while exhausted.
            /// </returns>
            if (ObjectManager.Player.HasBuff("summonerexhaust"))
            {
                RendDamage *= 0.6f;
            }

            /// <summary>
            /// Gets the reduction from the baron nashor.
            /// </summary>
            /// <returns>
            /// You deal 50% reduced damage to Baron Nashor.
            /// </returns>
            if (target.CharData.BaseSkinName.Equals("SRU_Baron") &&
                ObjectManager.Player.HasBuff("barontarget"))
            {
                RendDamage *= 0.5f;
            }

            /// <summary>
            /// Gets the reduction from the dragon.
            /// </summary>
            /// <returns>
            /// The Dragon receives 7% reduced damage per stack.
            /// </returns>
            if (target.CharData.BaseSkinName.Equals("SRU_Dragon"))
            {
                RendDamage *= 1f - (ObjectManager.Player.GetBuffCount("s5test_dragonslayerbuff") * 0.07f);
            }

            if (target is Obj_AI_Hero)
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
            */
            //return (float)((RendDamage - target.PhysicalShield) - healthDebuffer);
            return (float)(RendDamage - (target.PhysicalShield + target.HPRegenRate));
        }
    }
}