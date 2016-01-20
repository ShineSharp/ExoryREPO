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
            if (Variables.Q.IsReady())
            {
                var selectedPos =
                    ObjectManager.Player.Distance(new Vector2(11510, 4460)) <
                    ObjectManager.Player.Distance(new Vector2(6667, 8794)) ?
                        new Vector2(11510, 4460) :
                        new Vector2(6667, 8794);

                var walkPos = 
                    ObjectManager.Player.Distance(new Vector2(11510, 4460)) <
                    ObjectManager.Player.Distance(new Vector2(6667, 8794)) ?
                        new Vector2(12050, 4827) :
                        new Vector2(6962, 8952);

                if (Variables.Menu.Item($"{Variables.MainMenuName}.walltumbler.executewalltumble").GetValue<KeyBind>().Active)
                {
                    if (ObjectManager.Player.Distance(walkPos) > 50 || ObjectManager.Player.Distance(walkPos) < 15)
                    {
                        ObjectManager.Player.IssueOrder(GameObjectOrder.MoveTo, walkPos.To3D());
                        return;
                    }
                }

                if (Variables.Menu.Item($"{Variables.MainMenuName}.walltumbler.executewalltumble").GetValue<KeyBind>().Active ||
                    Variables.Menu.Item($"{Variables.MainMenuName}.walltumbler.enableonclickwalltumble").GetValue<bool>())
                {
                    if (ObjectManager.Player.Distance(walkPos) < 10 &&
                        !ObjectManager.Player.IsMoving)
                    {
                        Variables.Q.Cast(selectedPos.To3D());
                    }
                }
            }
        }
    }
}
