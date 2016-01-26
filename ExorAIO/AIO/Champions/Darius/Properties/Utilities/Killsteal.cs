using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Champions.Darius
{
    /// <summary>
    /// The killsteal class.
    /// </summary>
    class KillSteal
    {
        public static float GetRDamage(Obj_AI_Hero target)
        =>
            (float)
                (100 * ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).Level +
				(20 * ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).Level +
				(0.15 * ObjectManager.Player.FlatPhysicalDamageMod)) * target.GetBuffCount("dariushemo"));
    }
}
