using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Simulator.ActionExecutors
{
    public class ActionManager
    {
        private Dictionary<string, IActionExecutor> actionExecutors;

        public ActionManager()
        {
            actionExecutors = new Dictionary<string, IActionExecutor>
            {
                { DefaultActions.WAIT, new Wait() },
                { DefaultActions.COPY_MOVEMENT, new CopyMovement() },
                { DefaultActions.FOLLOW_PATH, new FollowPath() },
                { DefaultActions.MOVE_FORWARD, new MoveForward() },
                { DefaultActions.ROTATE, new Rotate() },
                { DefaultActions.MOVE_UP_TO, new MoveUpTo() }
            };
        }

        internal IActionExecutor GetActionExecutor(string actionType)
        {
            if (!actionExecutors.TryGetValue(actionType, out IActionExecutor executor))
            {
                throw new InvalidOperationException($"Unknown ActionType '{actionType}'.");
            }
            return executor;
        }

        public void AddExecutor(string actionType, IActionExecutor actionExecutor)
        {
            actionExecutors.Add(actionType, actionExecutor);
        }

        public static void MergeParameters(Dictionary<string, object> parameters, Dictionary<string, object> overrideParams)
        {
            foreach(var kv in overrideParams)
            {
                parameters[kv.Key] = kv.Value;
            }
        }

        public static class DefaultActions
        {
            public static readonly string WAIT = "Wait";
            public static readonly string MOVE_FORWARD = "MoveForward";
            public static readonly string COPY_MOVEMENT = "CopyMovement";
            public static readonly string FOLLOW_PATH = "FollowPath";
            public static readonly string ROTATE = "Rotate";
            public static readonly string MOVE_UP_TO = "MoveUpTo";
        }
    }
}
