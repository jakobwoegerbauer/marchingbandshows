using ShowEditor.Data;
using ShowEditor.Simulator.ActionExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Simulator.Templates
{
    public class GroupActions
    {
        public static GroupAction MoveForward(int duration, int delay = 0, double stepsize = 1, double priority = 0, int[] positions = null)
        {
            return new GroupAction
            {
                ActionType = ActionManager.DefaultActions.MOVE_FORWARD,
                Duration = duration,
                Delay = delay,
                Positions = positions,
                Priority = priority,
                Parameters = new Dictionary<string, object>
                {
                    { "stepsize", stepsize }
                },
            };
        }

        public static GroupAction Turn(bool toRight = false, int delay = 0, int[] positions = null)
        {
            return Rotate(toRight ? 90.0 : -90.0, delay, positions);
        }

        public static GroupAction Rotate(double degrees, int delay = 0, int[] positions = null, int duration = 1, double priority = 0)
        {
            return new GroupAction
            {
                ActionType = ActionManager.DefaultActions.ROTATE,
                Duration = duration,
                Delay = delay,
                Priority = priority,
                Positions = positions,
                Parameters = new Dictionary<string, object>
                    {
                        { "rotation", degrees / duration }
                    }
            };
        }

        public static GroupAction Follow(int position, int duration, int timeDiff, int delay = 0, int[] followers = null)
        {
            var g = new GroupAction
            {
                ActionType = ActionManager.DefaultActions.FOLLOW_PATH,
                Duration = duration,
                Delay = delay,
                Positions = followers,
                Parameters = new Dictionary<string, object>
                {
                    { "timeDiff", timeDiff },
                    { "dependant", position }
                }
            };
            if (timeDiff == 0)
                g.Dependencies = new int[] { position };
            return g;
        }

        public static GroupAction MoveUpTo(int position, int duration, int[] followers, int delay = 0, double stepsize = 1, double depth = 1)
        {
            return new GroupAction
            {
                ActionType = ActionManager.DefaultActions.MOVE_UP_TO,
                Duration = duration,
                Delay = delay,
                Positions = followers,
                Dependencies = new int[] { position },
                Parameters = new Dictionary<string, object>
                {
                    {"depth", depth },
                    {"stepsize", stepsize },
                    {"dependant", position }
                }
            };
        }
    }
}
