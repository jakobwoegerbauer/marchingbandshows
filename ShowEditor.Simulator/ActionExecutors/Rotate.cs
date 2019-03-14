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
            var p = GetDefaultParameters();
            ActionManager.MergeParameters(p, data.ActionParameters);
            double rotation = Convert.ToDouble(p["rotation"]);

            var pos = data.GetCurrentPosition();
            pos.Rotation -= rotation;
            return pos;
        }

        public Dictionary<string, object> GetDefaultParameters()
        {
            return new Dictionary<string, object>
            {
                { "rotation", -90 },
            };
        }
    }
}
