namespace AsunaCondemn
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using SharpDX;
    
    /// <summary>
    /// The drawings class.
    /// </summary>
    public class Drawings
    {
        /// <summary>
        /// Loads the range drawings.
        /// </summary>
        public static void Load()
        {
            Drawing.OnDraw += delegate
            {
                /// <summary>
                /// Loads the E drawing.
                /// </summary>
                if (!ObjectManager.Player.IsDead &&
                    Variables.E.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.e").GetValue<bool>())
                {
                    foreach (var e in HeroManager.Enemies
                        .Where(
                            c =>
                                c.IsValidTarget(Variables.E.Range)))
                    {
                        Drawing.DrawLine(
                            Drawing.WorldToScreen(e.Position).X,
                            Drawing.WorldToScreen(e.Position).Y,
                            Drawing.WorldToScreen(Variables.E.GetPrediction(e).UnitPosition - Vector3.Normalize(e.ServerPosition - ObjectManager.Player.Position) * 420).X,
                            Drawing.WorldToScreen(Variables.E.GetPrediction(e).UnitPosition - Vector3.Normalize(e.ServerPosition - ObjectManager.Player.Position) * 420).Y,
                            1,
                            (Variables.E.GetPrediction(e).UnitPosition - Vector3.Normalize(e.ServerPosition - ObjectManager.Player.Position) * 420).IsWall() ?
                                System.Drawing.Color.Green :
                                System.Drawing.Color.Red
                        );

                        Render.Circle.DrawCircle(
                            ObjectManager.Player.Position.Extend(e.ServerPosition, 425f),
                            50,
                            ObjectManager.Player.Distance(e) < 425f - (ObjectManager.Player.BoundingRadius + 100f) ?
                                System.Drawing.Color.Green :
                                System.Drawing.Color.Red,
                            1
                        );
                    }
                }
            };
        }
    }
}
