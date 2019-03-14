using System;
using System.Collections.Generic;
using ShowEditor.Data;
using ShowEditor.Simulator.ExecutionGraph;
using ShowEditor.Simulator.Templates;

namespace ShowEditor.Simulator.ActionExecutors
{
    public class MoveUpTo : IActionExecutor
    {
        public Position ExecuteStep(ActionData data)
        {
            var p = GetDefaultParameters();
            ActionManager.MergeParameters(p, data.ActionParameters);

            double depth = Convert.ToDouble(p["depth"]);
            double stepsize = Convert.ToDouble(p["stepsize"]);
            int relDepRow = Convert.ToInt32(p["dependantRelativeRow"]);
            int relDepCol = Convert.ToInt32(p["depandantRelativeColumn"]);
            int dependant = Convert.ToInt32(p["dependant"]);

            if (relDepRow != 0 || relDepCol != 0)
                dependant = (data.GetFormation() as RowsFormation).GetRelativePosition(data.CurrentPlayer, relDepRow, relDepCol);

            Position depPos = data.GetPosition(dependant, data.LocalTime);
            double alpha = data.GetCurrentPosition().Rotation - PositionHelper.ToDegrees(
                Math.Atan((depPos.Y - data.GetCurrentPosition().Y) / (depPos.X - data.GetCurrentPosition().X)));
            double hyp = PositionHelper.GetDistance(data.GetCurrentPosition(), depPos);
            double x = Math.Abs(hyp * Math.Cos(PositionHelper.ToRadians(alpha))) - depth;

            if (x <= stepsize)
            {
                return PositionHelper.Forward(data.GetCurrentPosition(), x);
            }
            return PositionHelper.Forward(data.GetCurrentPosition(), stepsize);
        }

        public Dictionary<string, object> GetDefaultParameters()
        {
            return new Dictionary<string, object>
            {
                { "depth", 1 },
                { "stepsize", 1 },
                { "dependantRelativeRow", 0 },
                { "depandantRelativeColumn", 0 },
                { "dependant", 0 }
            };
        }
    }
}
