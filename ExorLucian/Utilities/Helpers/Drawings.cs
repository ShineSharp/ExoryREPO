using LeagueSharp;
using LeagueSharp.Common;

namespace ExorLucian
{
    using Color = System.Drawing.Color;

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
                /// Loads the Q drawing.
                /// </summary>
                if (Variables.Q.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.q").IsActive())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.Q.Range, Color.Green, 1);
                }

                if (Variables.Q.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.qe").IsActive())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.Q.Range + 600f, Color.LightGreen, 1);
                }

                /// <summary>
                /// Loads the W drawing.
                /// </summary>
                if (Variables.W.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.w").IsActive())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.W.Range, Color.Purple, 1);
                }

                /// <summary>
                /// Loads the E drawing.
                /// </summary>
                if (Variables.E.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.e").IsActive())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.E.Range, Color.Cyan, 1);
                }
            };
        }
    }
}
