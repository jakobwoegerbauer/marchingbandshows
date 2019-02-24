using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Data
{
    /// <summary>
    /// Specifies an action that should be applied to a group in the current formation
    /// </summary>
    public class GroupAction
    {
        /// <summary>
        /// Delay of the action. The action is started {Delay} steps after the starttime of the current element
        /// </summary>
        public int Delay { get; set; }

        /// <summary>
        /// Duration of the action. The action is executed {Duration} times.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Priority of the action. Actions with higher values are executed before actions with lower values.
        /// </summary>
        public double Priority { get; set; }

        /// <summary>
        /// The positions which the action should be applied to.
        /// If Positions is null the action will be applied to every position in the formation.
        /// </summary>
        public int[] Positions { get; set; }

        /// <summary>
        /// Type of the action. Is used to lookup the correct ActionExecutor.
        /// </summary>
        public string ActionType { get; set; }

        /// <summary>
        /// Array of positions which should perform their actions before the positions of the current action.
        /// </summary>
        public int[] Dependencies { get; set; }

        /// <summary>
        /// Additional parameters for the ActionExecutor specified by the ActionType
        /// </summary>
        public Dictionary<string, object> Parameters { get; set; }
    }
}
