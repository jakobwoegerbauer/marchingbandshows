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
            double direction = 0;

            if (data.ActionParameters.TryGetValue("stepsize", out object ds))
                stepsize = Convert.ToDouble(ds);
            if (data.ActionParameters.TryGetValue("direction", out object dir))
                direction = Convert.ToDouble(dir);

            return PositionHelper.Forward(data.GetCurrentPosition(), stepsize, direction);
        }
    }
}
