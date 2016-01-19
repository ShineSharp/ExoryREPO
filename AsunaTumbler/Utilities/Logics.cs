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
            /// The WallTumble Logic.
            /// </summary>
            
            //Ty Asuna
            if (Environment.TickCount - Variables.LastMoveC < 80)
            {
                return;
            }
            Variables.LastMoveC = Environment.TickCount;

            if (Variables.Q.IsReady())
            {
                var selectedPos =
                    ObjectManager.Player.Distance(new Vector2(11510, 4470)) <
                    ObjectManager.Player.Distance(new Vector2(6667, 8794)) ?
                        new Vector2(11510, 4470) :
                        new Vector2(6667, 8794);

                var walkPos = 
                    ObjectManager.Player.Distance(new Vector2(11510, 4470)) <
                    ObjectManager.Player.Distance(new Vector2(6667, 8794)) ?
                        new Vector2(12050, 4828) :
                        new Vector2(6962, 8952);

                if (Variables.Menu.Item($"{Variables.MainMenuName}.walltumbler.executewalltumble").GetValue<KeyBind>().Active)
                {
                    if (ObjectManager.Player.Distance(walkPos) > 50)
                    {
                        ObjectManager.Player.IssueOrder(GameObjectOrder.MoveTo, walkPos.To3D());
                        return;
                    }
                }

                if (Variables.Menu.Item($"{Variables.MainMenuName}.walltumbler.executewalltumble").GetValue<KeyBind>().Active ||
                    Variables.Menu.Item($"{Variables.MainMenuName}.walltumbler.enableonclickwalltumble").GetValue<bool>())
                {
                    if (ObjectManager.Player.Distance(walkPos) < 5)
                    {
                        Variables.Q.Cast(selectedPos.To3D());
                        ObjectManager.Player.IssueOrder(GameObjectOrder.MoveTo, selectedPos.To3D());
                    }
                }
            }
        }
    }
}
