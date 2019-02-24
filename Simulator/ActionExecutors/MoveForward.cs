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
            double stepsize = 1;
            if (data.ActionParameters != null)
            {
                if (data.ActionParameters.TryGetValue("stepsize", out object ds))
                    stepsize = (ds as double?) ?? stepsize;
            }

            return PositionHelper.Forward(data.GetCurrentPosition(), stepsize);
        }
    }
}
