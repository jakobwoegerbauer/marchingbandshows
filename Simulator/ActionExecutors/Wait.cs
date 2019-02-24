﻿using System.Collections.Generic;
using ShowEditor.Data;
using ShowEditor.Simulator.ExecutionGraph;

namespace ShowEditor.Simulator.ActionExecutors
{
    public class Wait : IActionExecutor
    {
        public Position ExecuteStep(ActionData data)
        {
            return data.GetCurrentPosition();
        }
    }
}
