using LeagueSharp;
using LeagueSharp.Common;

namespace AsunaCondemn
{
    using System.Linq;
    using SharpDX;
    using Color = System.Drawing.Color;
    using SPrediction;

    /// <summary>
    /// The drawings class.
    /// </summary>
    class Drawings
    {
        /// <summary>
        /// Loads the range drawings.
        /// </summary>
        public static void Initialize()
        {
            Drawing.OnDraw += delegate
            {
                /// <summary>
                /// Loads the E drawing.
                /// </summary>
                if (Variables.E.IsReady() &&
                    !ObjectManager.Player.IsDead &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.e").GetValue<bool>())
                {
                    foreach (var e in HeroManager.Enemies
                        .Where(c => c.IsValidTarget(Variables.E.Range)))
                    {
                        Drawing.DrawLine(
                            Drawing.WorldToScreen(e.Position).X,
                            Drawing.WorldToScreen(e.Position).Y,
                            Drawing.WorldToScreen(Variables.E.GetSPrediction(e).UnitPosition.To3D2() - Vector3.Normalize(e.ServerPosition - ObjectManager.Player.Position) * 430).X,
                            Drawing.WorldToScreen(Variables.E.GetSPrediction(e).UnitPosition.To3D2() - Vector3.Normalize(e.ServerPosition - ObjectManager.Player.Position) * 430).Y,
                            1,
                            (Variables.E.GetSPrediction(e).UnitPosition.To3D2() - Vector3.Normalize(e.ServerPosition - ObjectManager.Player.Position) * 430).IsWall() &&
                            (Variables.E.GetSPrediction(e).UnitPosition.To3D2() - Vector3.Normalize(e.ServerPosition - ObjectManager.Player.Position) * 440).IsWall() ?
                                Color.Green :
                                Color.Red
                        );

                        Render.Circle.DrawCircle(
                            ObjectManager.Player.ServerPosition.Extend(e.ServerPosition, 425f),
                            50,
                            ObjectManager.Player.Distance(e) < 425f - ObjectManager.Player.BoundingRadius*2 ?
                                Color.Green :
                                Color.Red,
                            1
                        );
                    }
                }
            };
        }
    }
}
