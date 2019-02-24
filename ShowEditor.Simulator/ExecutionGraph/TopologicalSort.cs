using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Simulator.ExecutionGraph
{
    internal class TopologicalSort
    {
        private IEnumerable<Edge> edges;
        private List<int> orderedNodes;
        private readonly bool[] markedPermanently;
        private readonly bool[] markedTemporarily;

        public TopologicalSort(IEnumerable<Edge> edges, int nodeCount)
        {
            this.edges = edges;
            markedPermanently = new bool[nodeCount];
            markedTemporarily = new bool[nodeCount];
            orderedNodes = new List<int>();
        }

        public int[] Sort()
        {
            for(int i = 0; i < markedPermanently.Length; i++)
            {
                if (!markedTemporarily[i] && !markedPermanently[i])
                    Sort(i);
            }

            return orderedNodes.ToArray();
        }

        private void Sort(int n)
        {
            if (markedPermanently[n])
                return;
            if (markedTemporarily[n])
                throw new ArgumentException("The dependency graph contains cycles.");

            markedTemporarily[n] = true;
            foreach(var edge in edges.Where(e => e.From == n))
            {
                Sort(edge.To);
            }
            markedPermanently[n] = true;
            orderedNodes.Insert(0, n);
        }
    }
}
