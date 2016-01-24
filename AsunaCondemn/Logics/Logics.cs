namespace AsunaCondemn
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using SharpDX;

    /// <summary>
    /// The logics class.
    /// </summary>
    public class Logics
    {
        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void ExecuteAuto(EventArgs args)
        {
            /// <summary>
            /// The fixed Condem Logic Kappa.
            /// </summary>
            if (Variables.E.IsReady() &&
                Variables.Flash != SpellSlot.Unknown &&
                Variables.Flash.IsReady() &&
                Variables.Menu.Item($"{Variables.MainMenuName}.condem.executecondem").GetValue<KeyBind>().Active)
            {
                foreach (var e in HeroManager.Enemies
                    .Where(
                        c =>
                            c.IsValidTarget(Variables.E.Range) &&
                            ObjectManager.Player.Distance(c) < 425f - ObjectManager.Player.BoundingRadius*2))
                {
                    for (int i = 1; i < 10; i++)
                    {
                        if ((Variables.E.GetPrediction(e).UnitPosition - Vector3.Normalize(e.ServerPosition - ObjectManager.Player.Position) * i * 42).IsWall() &&
                            (Variables.E.GetPrediction(e).UnitPosition - Vector3.Normalize(e.ServerPosition - ObjectManager.Player.Position) * i * 44).IsWall())
                        {
                            Variables.E.CastOnUnit(e);
                            ObjectManager.Player.Spellbook.CastSpell(Variables.Flash, ObjectManager.Player.ServerPosition.Extend(e.ServerPosition, 425f - ObjectManager.Player.BoundingRadius*2));
                        }
                    }
                }
            }
        }
    }
}
