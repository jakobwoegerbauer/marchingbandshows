using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowEditor.Data;
using ShowEditor.Simulator.ExecutionGraph;

namespace ShowEditor.Simulator.ActionExecutors
{
    public class Rotate : IActionExecutor
    {
        public Position ExecuteStep(ActionData data)
        {
            double rotation = -90;
            if (data.ActionParameters != null)
            {
                if (data.ActionParameters.TryGetValue("rotation", out object ds))
                    rotation = (ds as double?) ?? rotation;
            }
            var pos = data.GetCurrentPosition();
            pos.Rotation -= rotation;
            return pos;
        }
    }
}
