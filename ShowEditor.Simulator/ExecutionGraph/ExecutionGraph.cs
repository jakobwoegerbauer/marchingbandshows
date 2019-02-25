using ShowEditor.Data;
using ShowEditor.Simulator.ActionExecutors;
using ShowEditor.Simulator.Templates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShowEditor.Simulator.ExecutionGraph
{
    public class Graph
    {
        private ActionManager actionManager;
        private Element show;
        private readonly PositionHistory[] nodes;
        private List<IndividualAction>[] individualActions;
        private List<Edge> edges;
        private int[] nodeOrder;
        private bool calculated;
        private Dictionary<Element, int[]> reversePositionMappings;

        public int Time { get; private set; }

        public Graph(Element show, ActionManager actionManager)
        {
            this.show = show;
            this.actionManager = actionManager;
            var formation = show.StartFormation;
            nodes = new PositionHistory[formation.Size];
            individualActions = new List<IndividualAction>[formation.Size];
            reversePositionMappings = new Dictionary<Element, int[]>();

            for (int i = 0; i < formation.Size; i++)
            {
                nodes[i] = new PositionHistory(formation.Positions[i]);
                individualActions[i] = new List<IndividualAction>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transformation"></param>
        /// <param name="reversePositionMapping">reverse position mapping. 
        /// Maps the position index from the subTransformation to the main transformation index</param>
        /// <param name="localTime">time since the start of the current transformation</param>
        /// <returns></returns>
        private List<Edge> CollectEdges(Element transformation, int[] reversePositionMapping, int localTime)
        {
            if (!reversePositionMappings.ContainsKey(transformation))
            {
                int[] r = new int[reversePositionMapping.Length];
                Array.Copy(reversePositionMapping, r, r.Length);
                reversePositionMappings.Add(transformation, r);
            }
            List<Edge> edges = new List<Edge>();
            if (transformation.Duration < localTime)
                return edges;

            if (transformation.GroupActions != null)
            {
                foreach (var action in transformation.GroupActions)
                {
                    if (action.Delay > localTime || localTime >= action.Delay + action.Duration)
                        continue;
                    int[] positions = action.Positions ?? Combination.Range(0, transformation.StartFormation.Size - 1);
                    for (int i = 0; i < positions.Length; i++)
                    {
                        if (reversePositionMapping[positions[i]] != -1)
                        {
                            if (action.Dependencies != null)
                            {
                                foreach(int dep in action.Dependencies)
                                {
                                    edges.Add(new Edge(
                                        reversePositionMapping[dep],
                                        reversePositionMapping[positions[i]]
                                    ));
                                }                                
                            }

                            SetAction(new IndividualAction
                            {
                                Position = reversePositionMapping[positions[i]],
                                ActionType = action.ActionType,
                                Priority = action.Priority,
                                Parameters = action.Parameters,
                                LocalTime = localTime+1,
                                Element = transformation
                            }, reversePositionMapping[positions[i]]);
                        }
                    }
                }
            }

            if (transformation.SubElements != null)
            {
                foreach (var subTransformation in transformation.SubElements)
                {
                    if (localTime < subTransformation.StartTime
                        || localTime >= subTransformation.StartTime + subTransformation.Element.Duration)
                        continue;

                    int[] mapping = new int[subTransformation.Element.StartFormation.Size];
                    for (int i = 0; i < subTransformation.Element.StartFormation.Size; i++)
                    {
                        mapping[i] = -1;
                    }

                    int[] subMapping = subTransformation.PositionMapping ?? Combination.Range(0, transformation.StartFormation.Size - 1);
                    for (int i = 0; i < subMapping.Length; i++)
                    {
                        if (subMapping[i] != -1)
                        {
                            mapping[subMapping[i]] = i;
                        }
                    }
                    edges.AddRange(CollectEdges(subTransformation.Element, mapping, localTime - subTransformation.StartTime));
                }
            }
            return edges;
        }

        private void SetAction(IndividualAction action, int globalPosition)
        {
            individualActions[globalPosition].Add(action);
        }

        public void Step()
        {
            CalculateStep();
            ExecuteStep();
        }

        public void CalculateStep()
        {
            if (calculated)
                return;

            int[] mapping = new int[show.StartFormation.Size];
            for (int i = 0; i < mapping.Length; i++)
            {
                mapping[i] = i;
            }
            edges = CollectEdges(show, mapping, Time);
            nodeOrder = new TopologicalSort(edges.ToArray(), nodes.Length).Sort();
            calculated = true;
        }

        public void ExecuteStep()
        {
            if (!calculated)
            {
                throw new InvalidOperationException("CalculateStep() has to be called before.");
            }
            calculated = false;
            Time++;
            for (int i = 0; i < nodeOrder.Length; i++)
            {
                var n = nodeOrder[i];
                Position p = nodes[n].GetPosition(Time);
                foreach (var action in individualActions[n].OrderByDescending(a => a.Priority))
                {
                    var executor = actionManager.GetActionExecutor(action.ActionType);
                    p = executor.ExecuteStep(new ActionData(Time, action.LocalTime, nodes, reversePositionMappings[action.Element], n, action.Element, action.Parameters));
                    nodes[n].AddPosition(p, Time);
                }
                if (individualActions[n].Count == 0)
                {
                    nodes[n].AddPosition(nodes[n].GetPosition(Time), Time + 1);
                }
                individualActions[n].Clear();
            }
        }

        public List<Edge> GetEdges()
        {
            return edges;
        }

        public Position[] GetPositions()
        {
            return nodes.Select(n => n.GetPosition(Time)).ToArray();
        }
    }
}