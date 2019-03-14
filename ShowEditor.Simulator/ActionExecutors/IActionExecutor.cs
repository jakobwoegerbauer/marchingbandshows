using ShowEditor.Data;
using ShowEditor.Simulator.ExecutionGraph;
using System.Collections.Generic;

namespace ShowEditor.Simulator.ActionExecutors
{
    public interface IActionExecutor
    {
        /// <summary>
        /// Executes a simulation step action.
        /// </summary>
        Position ExecuteStep(ActionData actionData);

        Dictionary<string, object> GetDefaultParameters();
    }
}
