using ShowEditor.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShowEditor.Simulator.ExecutionGraph
{
    public class PositionHistory
    {
        private readonly List<Position> positionHistory;

        public PositionHistory(Position position)
        {
            positionHistory = new List<Position>
            {
                position.Copy()
            };
        }

        internal void AddPosition(Position position, int time)
        {
            if (time < positionHistory.Count - 1)
                positionHistory.RemoveRange(time, positionHistory.Count - time);

            while (positionHistory.Count <= time)
            {
                positionHistory.Add(positionHistory.Last());
            }
            positionHistory[time] = position;
        }

        public Position GetPosition(int time)
        {
            if (time < 0)
                time = 0;
            if (time >= positionHistory.Count)
                time = positionHistory.Count - 1;

            return positionHistory[time].Copy();
        }
    }
}
