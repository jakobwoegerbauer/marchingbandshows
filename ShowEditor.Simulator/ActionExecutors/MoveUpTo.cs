using System;
using System.Collections.Generic;
using ShowEditor.Data;
using ShowEditor.Simulator.ExecutionGraph;

namespace ShowEditor.Simulator.ActionExecutors
{
    public class MoveUpTo : IActionExecutor
    {
        public Position ExecuteStep(ActionData data)
        {
            double depth = 1;
            double stepsize = 1;
            int dependant = 0;

            if (data.ActionParameters.TryGetValue("depth", out object d))
                depth = Convert.ToDouble(d);
            if (data.ActionParameters.TryGetValue("stepsize", out object s))
                stepsize = Convert.ToDouble(s);
            if (data.ActionParameters.TryGetValue("dependant", out object dep))
                dependant = Convert.ToInt32(dep);

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
    }
}
