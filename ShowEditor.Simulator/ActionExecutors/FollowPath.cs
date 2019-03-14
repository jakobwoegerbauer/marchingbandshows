using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowEditor.Data;
using ShowEditor.Simulator.ExecutionGraph;
using ShowEditor.Simulator.Templates;

namespace ShowEditor.Simulator.ActionExecutors
{
    class FollowPath : IActionExecutor
    {
        public Position ExecuteStep(ActionData data)
        {
            var p = GetDefaultParameters();
            ActionManager.MergeParameters(p, data.ActionParameters);

            int timeDiff = Convert.ToInt32(p["timeDiff"]);
            int minTime = Convert.ToInt32(p["minTime"]);
            int relDepRow = Convert.ToInt32(p["dependantRelativeRow"]);
            int relDepCol = Convert.ToInt32(p["depandantRelativeColumn"]);
            int dependant = Convert.ToInt32(p["dependant"]);

            if (relDepRow != 0 || relDepCol != 0)
                dependant = (data.GetFormation() as RowsFormation).GetRelativePosition(data.CurrentPlayer, relDepRow, relDepCol);

            if (data.LocalTime - timeDiff < minTime)
                return data.GetCurrentPosition();

            return data.GetPosition(dependant, data.LocalTime - timeDiff);
        }

        public Dictionary<string, object> GetDefaultParameters()
        {
            return new Dictionary<string, object>
            {
                { "timeDiff", 0 },
                { "minTime", 0 },
                { "dependantRelativeRow", 0 },
                { "depandantRelativeColumn", 0 },
                { "dependant", 0 }
            };
        }
    }
}
