using ShowEditor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Simulator.ExecutionGraph
{
    internal class IndividualAction
    {
        public int Position { get; set; }
        public string ActionType { get; set; }
        public double Priority { get; set; }
        public int LocalTime { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public Element Element { get; set; }
    }
}
