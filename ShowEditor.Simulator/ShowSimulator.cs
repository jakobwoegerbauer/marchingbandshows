using ShowEditor.Data;
using ShowEditor.Simulator.ActionExecutors;
using ShowEditor.Simulator.ExecutionGraph;
using System;

namespace ShowEditor.Simulator
{
    public class ShowSimulator
    {
        public ActionManager ActionManager { get; private set; }
        public Show Show { get; }
        public int Time
        {
            get
            {
                if (executionGraph == null)
                    return 0;
                return executionGraph.Time;
            }
        }

        private Graph executionGraph;

        public ShowSimulator(Show show)
        {
            if (show.Element.StartFormation == null)
                throw new ArgumentException("The StartFormation of the show must not be null");

            ActionManager = new ActionManager();
            Show = show;
        }

        public void Initialize()
        {
            executionGraph = new Graph(Show.Element, ActionManager);
        }

        public void ExecuteStep()
        {
            if (executionGraph == null)
                Initialize();

            executionGraph.Step();
        }

        public Position[] GetPositions()
        {
            return executionGraph?.GetPositions() ?? Show.Element.StartFormation.Positions;
        }
    }
}
