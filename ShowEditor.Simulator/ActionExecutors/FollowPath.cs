using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowEditor.Data;
using ShowEditor.Simulator.ExecutionGraph;

namespace ShowEditor.Simulator.ActionExecutors
{
    class FollowPath : IActionExecutor
    {
        public Position ExecuteStep(ActionData data)
        {
            int timeDiff = 0;
            int minTime = 0;
            int dependant = 0;

            if (data.ActionParameters.TryGetValue("timeDiff", out object td))
                timeDiff = Convert.ToInt32(td);
            if (data.ActionParameters.TryGetValue("minTime", out object mt))
                minTime = Convert.ToInt32(mt);
            if (data.ActionParameters.TryGetValue("dependant", out object d))
                dependant = Convert.ToInt32(d);

            if (data.LocalTime - timeDiff < minTime)
                return data.GetCurrentPosition();

            return data.GetPosition(dependant, data.LocalTime - timeDiff);
        }
    }
}
