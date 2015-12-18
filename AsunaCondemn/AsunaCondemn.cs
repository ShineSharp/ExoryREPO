namespace AsunaCondemn
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LeagueSharp;
    using LeagueSharp.Common;

    using SharpDX;

    /// <summary>
    /// The main class.
    /// </summary>
    public class Condem
    {
        /// <summary>
        /// Triggers when the champion is loaded.
        /// </summary>
        public static void OnLoad()
        {
            Settings.SetSpells();
            Settings.SetMenu();
            Settings.SetMethods();
        }

        /// <summary>
        /// Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void Game_OnGameUpdate(EventArgs args)
        {
            if (!ObjectManager.Player.IsDead)
            {
                Logics.ExecuteAuto(args);
            }
        }

        /// <summary>
        /// Called when the drawings update themselves.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static void Drawing_OnDraw(EventArgs args)
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
                            c.IsEnemy &&
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
                        ObjectManager.Player.Position.Extend(e.ServerPosition, 425f - (ObjectManager.Player.BoundingRadius + 150f)),
                        50,
                        ObjectManager.Player.Distance(e) < 425f - (ObjectManager.Player.BoundingRadius + 150f) ?
                            System.Drawing.Color.Green :
                            System.Drawing.Color.Red,
                        1
                    );
                }
            }
        }
    }
}
