using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Utilities
{
    using System.Linq;
    using Color = System.Drawing.Color;

    /// <summary>
    /// The drawings class.
    /// </summary>
    class Drawings
    {
        /// <summary>
        /// Loads the drawings.
        /// </summary>
        public static void Initialize()
        {
            Drawing.OnDraw += delegate
            {
                /// <summary>
                /// Loads the Q drawing,
                /// Loads the Q Spots drawing.
                /// </summary>
                if (Variables.Q != null)
                {
                    if (Variables.Q.IsReady() &&
                        Variables.Menu.Item($"{Variables.MainMenuName}.drawings.q") != null &&
                        Variables.Menu.Item($"{Variables.MainMenuName}.drawings.q").GetValue<bool>())
                    {
                        Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.Q.Range, Color.Green, 1);
                    }

                    if (Variables.Menu.Item($"{Variables.MainMenuName}.drawings.qs") != null &&
                        Variables.Menu.Item($"{Variables.MainMenuName}.drawings.qs").GetValue<bool>())
                    {
                        foreach (GameObject gameobject in GameObjects.AllGameObjects
                            .Where(x => x.Name.Equals("Draven_Base_Q_reticle_self.troy")))
                        {
                            Render.Circle.DrawCircle(gameobject.Position, 120f, Color.Green, 1);
                        }
                    }
                }

                /// <summary>
                /// Loads the W drawing.
                /// </summary>
                if (Variables.W != null &&
                    Variables.W.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.w") != null &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.w").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.W.Range, Color.Purple, 1);
                }

                /// <summary>
                /// Loads the E drawing.
                /// </summary>
                if (Variables.E != null &&
                    Variables.E.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.e") != null &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.e").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.E.Range, Color.Cyan, 1);
                }

                /// <summary>
                /// Loads the R drawing.
                /// </summary>
                if (Variables.R != null &&
                    Variables.R.IsReady() &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.r") != null &&
                    Variables.Menu.Item($"{Variables.MainMenuName}.drawings.r").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Variables.R.Range, Color.Red, 1);
                }
            };
        }
    }
}
