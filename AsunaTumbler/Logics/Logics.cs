namespace AsunaTumbler
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
            /// The Movement Request Humanization, ty Asuna.
            /// </summary>
            if (Environment.TickCount - Variables.LastMoveC < 80)
            {
                return;
            }
            Variables.LastMoveC = Environment.TickCount;

            /// <summary>
            /// The WallTumble Logic.
            /// </summary>
            if (Variables.Q.IsReady() &&

                (Variables.Menu.Item($"{Variables.MainMenuName}.walltumbler.executewalltumble").GetValue<KeyBind>().Active ||
                    Variables.Menu.Item($"{Variables.MainMenuName}.walltumbler.enableonclickwalltumble").GetValue<bool>()))
            {
                if (ObjectManager.Player.Distance(new Vector2(6667, 8794)) >=
                    ObjectManager.Player.Distance(new Vector2(11514, 4462)))
                {

                    if (ObjectManager.Player.Position.X < 12000 ||
                        ObjectManager.Player.Position.X > 12070 ||
                        ObjectManager.Player.Position.Y < 4800 ||
                        ObjectManager.Player.Position.Y > 4872)
                    {
                        ObjectManager.Player.IssueOrder(GameObjectOrder.MoveTo, new Vector2(12050, 4827).To3D());
                    }
                    else
                    {
                        ObjectManager.Player.IssueOrder(GameObjectOrder.MoveTo, new Vector2(12050, 4827).To3D());
                        LeagueSharp.Common.Utility.DelayAction.Add((int)(106 + Game.Ping / 2f),
                            () =>
                            {
                                Variables.Q.Cast(new Vector2(11514, 4462));
                            }
                        );
                    }
                }
                else
                {
                    if (ObjectManager.Player.Position.X < 6908 ||
                        ObjectManager.Player.Position.X > 6978 ||
                        ObjectManager.Player.Position.Y < 8917 ||
                        ObjectManager.Player.Position.Y > 8989)
                    {
                        ObjectManager.Player.IssueOrder(GameObjectOrder.MoveTo, new Vector2(6962, 8952).To3D());
                    }
                    else
                    {
                        ObjectManager.Player.IssueOrder(GameObjectOrder.MoveTo, new Vector2(6962, 8952).To3D());
                        LeagueSharp.Common.Utility.DelayAction.Add((int)(106 + Game.Ping / 2f),
                            () =>
                            {
                                Variables.Q.Cast(new Vector2(6667, 8794));

                            }
                        );
                    }
                }
            }
        }
    }
}
