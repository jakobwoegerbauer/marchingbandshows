using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowEditor.Data;
using ShowEditor.Simulator.ExecutionGraph;

namespace ShowEditor.Simulator.ActionExecutors
{
    class MoveForward : IActionExecutor
    {
        public Position ExecuteStep(ActionData data)
        {
            var p = GetDefaultParameters();
            ActionManager.MergeParameters(p, data.ActionParameters);
            double stepsize = Convert.ToDouble(p["stepsize"]);
            double direction = Convert.ToDouble(p["direction"]);

            return PositionHelper.Forward(data.GetCurrentPosition(), stepsize, direction);
        }

        public Dictionary<string, object> GetDefaultParameters()
        {
            return new Dictionary<string, object>
            {
                { "stepsize", 1 },
                { "direction", 0 }
            };
        }
    }
}
