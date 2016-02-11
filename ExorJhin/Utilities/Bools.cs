using LeagueSharp;
using LeagueSharp.Common;

namespace ExorJhin
{
    using System;
    using SharpDX;

    /// <summary>
    /// The bools class.
    /// </summary>
    class Bools
    {
        /// <summary>
        /// Gets a value indicating whether the target has protection or not.
        /// </summary>
        /// <value>
        /// <c>true</c> if the has no protection.; otherwise, <c>false</c>.
        /// </value> 
        public static bool IsSpellShielded(Obj_AI_Hero unit)
        =>
            unit.HasBuffOfType(BuffType.SpellShield) ||
            unit.HasBuffOfType(BuffType.SpellImmunity) ||
            Utils.TickCount - unit.LastCastedSpellT() < 300 &&
            (
                unit.LastCastedSpellName().Equals("SivirE") ||
                unit.LastCastedSpellName().Equals("BlackShield") ||
                unit.LastCastedSpellName().Equals("NocturneShit")
            );

        /// <summary>
        /// Gets a value indicating whether a determined champion can move or not.
        /// </summary>
        /// <value>
        /// <c>true</c> if the target can't move.; otherwise, <c>false</c>.
        /// </value>
        public static bool IsImmobile(Obj_AI_Hero target)
        => 
            target.IsChannelingImportantSpell() ||
            target.HasBuffOfType(BuffType.Stun) ||
            target.HasBuffOfType(BuffType.Flee) ||
            target.HasBuffOfType(BuffType.Snare) ||
            target.HasBuffOfType(BuffType.Taunt) ||
            target.HasBuffOfType(BuffType.Charm) ||
            target.HasBuffOfType(BuffType.Knockup) ||
            target.HasBuffOfType(BuffType.Suppression);

        /// <summary>
        /// Gets a value indicating whether a determined champion is inside R Cone.
        /// </summary>
        /// <value>
        /// <c>true</c> if the target is inside R Cone.; otherwise, <c>false</c>.
        /// </value>
        public static bool IsInsideRCone(Obj_AI_Hero target)
        =>
            (target.Position.To2D() - ObjectManager.Player.Position.To2D())
                .Distance(new Vector2(), true) < Variables.R.Range * Variables.R.Range &&

                (ObjectManager.Player.Position.Extend(target.Position, Variables.R.Range).To2D() - ObjectManager.Player.Position.To2D())
                    .Rotated(-(70f * (float)Math.PI / 180) / 2)
                    .CrossProduct(target.Position.To2D() - ObjectManager.Player.Position.To2D()) > 0 &&

            (target.Position.To2D() - ObjectManager.Player.Position.To2D())
                .CrossProduct((ObjectManager.Player.Position.Extend(target.Position, Variables.R.Range).To2D() - ObjectManager.Player.Position.To2D())
                .Rotated(-(70f * (float)Math.PI / 180) / 2).Rotated(70f * (float)Math.PI / 180)) > 0;
    }
}
