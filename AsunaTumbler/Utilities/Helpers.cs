namespace AsunaTumbler
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Color = System.Drawing.Color;

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
                /// Loads the WallTumble Positions drawings.
                /// </summary>
                if (Variables.Menu.Item($"{Variables.MainMenuName}.drawings.show").GetValue<bool>())
                {
                    Render.Circle.DrawCircle(new Vector2(12050, 4827).To3D(), 65f, Color.AliceBlue);
                    Render.Circle.DrawCircle(new Vector2(6962, 8952).To3D(), 65f, Color.AliceBlue);
                }
            };
        }
    }
}
