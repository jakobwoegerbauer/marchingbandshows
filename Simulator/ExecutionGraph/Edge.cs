using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Simulator.ExecutionGraph
{
    public class Edge
    {
        public int From { get; set; }
        public int To { get; set; }

        public Edge(int from, int to)
        {
            From = from;
            To = to;
        }
    }
}
