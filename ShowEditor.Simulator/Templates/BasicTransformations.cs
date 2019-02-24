using ShowEditor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Simulator.Templates
{
    public class BasicTransformations
    {
        public double StepSize { get; set; }

        public BasicTransformations(double stepSize)
        {
            StepSize = stepSize;
        }

        public Element MoveForward(string name, Formation formation, int duration)
        {
            return new Element
            {
                Name = name,
                StartFormation = formation,
                GroupActions = new GroupAction[]
                {
                    GroupActions.MoveForward(duration, stepsize: StepSize)
                }
            };
        }

        public Element Schwenkung(string name, RowsFormation formation, bool toRight = false, int duration = 8, double slowStep = 0.4)
        {
            List<GroupAction> groupActions = new List<GroupAction>();
            double degree = toRight ? -90.0 : 90.0;
            int endOfLastRowTurn = (duration / 2) * (formation.Rows + 1);
            double posFrontRowWhenLastTurnEnds = slowStep * (endOfLastRowTurn - duration);
            double posSecondToLastRowWhenLastTurnEnds = posFrontRowWhenLastTurnEnds - (formation.Rows - 2) * formation.Depth;
            int durationAll = endOfLastRowTurn + (int)(posSecondToLastRowWhenLastTurnEnds / (StepSize - slowStep))-1;

            int[] frontRow = formation.GetRow(0);
            for (int j = 0; j < formation.Columns; j++)
            {
                int inwardIndex = toRight ? (formation.Columns - j - 1) : j;
                int[] p = new int[1];
                p[0] = frontRow[inwardIndex];
                groupActions.Add(GroupActions.MoveForward(duration, stepsize: (j + 1.0) * PositionHelper.ToRadians(Math.Abs(degree) / duration), positions: p));
                groupActions.Add(GroupActions.Rotate(degree*duration/(duration+1), duration: duration, priority: 10, positions: p));
                groupActions.Add(GroupActions.Rotate(degree/(duration+1), delay: duration-1, duration: 1, priority: -10, positions: p));
            }
            groupActions.Add(GroupActions.MoveForward(durationAll - duration, delay: duration, stepsize: slowStep, positions: frontRow));

            int[] rowBefore = frontRow;

            List<int> allExceptFirstRow = new List<int>(Combination.Range(0, formation.Size - 1));
            allExceptFirstRow.RemoveAll(x => frontRow.Contains(x));
            groupActions.Add(GroupActions.MoveForward(1, delay: duration / 2, stepsize: StepSize, positions: allExceptFirstRow.ToArray()));

            for (int i = 1; i < formation.Rows; i++)
            {
                int[] row = formation.GetRow(i);
                int timeFinishedTurn = (i + 1) * (duration / 2) + duration / 2;
                for (int j = 0; j < formation.Columns; j++)
                {
                    int[] p = new int[1];
                    p[0] = row[j];
                    groupActions.Add(GroupActions.Follow(rowBefore[j], timeFinishedTurn, duration / 2+1, delay: 0, followers: p));
                }
                groupActions.Add(GroupActions.MoveUpTo(rowBefore[0], durationAll - timeFinishedTurn, row, delay: timeFinishedTurn, stepsize: StepSize, depth: formation.Depth));
                rowBefore = row;
            }

            return new Element
            {
                Name = name,
                StartFormation = formation,
                GroupActions = groupActions.ToArray(),
            };
        }
    }
}
