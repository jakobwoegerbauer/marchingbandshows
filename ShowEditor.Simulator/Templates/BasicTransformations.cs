using ShowEditor.Data;
using ShowEditor.Simulator.ActionExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Simulator.Templates
{
    public class BasicElements
    {
        public double StepSize { get; set; }

        public BasicElements(double stepSize)
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
            int durationAll = endOfLastRowTurn + (int)(posSecondToLastRowWhenLastTurnEnds / (StepSize - slowStep)) - 1;

            int[] frontRow = formation.GetRow(0);
            for (int j = 0; j < formation.Columns; j++)
            {
                int inwardIndex = toRight ? (formation.Columns - j - 1) : j;
                int[] p = new int[1];
                p[0] = frontRow[inwardIndex];
                groupActions.Add(GroupActions.MoveForward(duration, stepsize: (j + 1.0) * PositionHelper.ToRadians(Math.Abs(degree) / duration) * formation.SideMargin, positions: p));
                groupActions.Add(GroupActions.Rotate(degree * duration / (duration + 1), duration: duration, priority: 10, positions: p));
                groupActions.Add(GroupActions.Rotate(degree / (duration + 1), delay: duration - 1, duration: 1, priority: -10, positions: p));
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
                    groupActions.Add(GroupActions.Follow(rowBefore[j], timeFinishedTurn-2, duration / 2, delay: 2, followers: p));
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

        public Element BreiteFormation(string name, RowsFormation formation, int stepsPerRow = 2, double sideMarginFactor = 2)
        {
            List<GroupAction> groupActions = new List<GroupAction>();

            for(int i = 0; i < formation.Columns; i++)
            {
                double fromCenter = i - (formation.Columns-1) / 2.0;
                groupActions.Add(new GroupAction
                {
                    ActionType = ActionManager.DefaultActions.MOVE_FORWARD,
                    Duration = (int)Math.Abs(stepsPerRow * fromCenter),
                    Positions = formation.GetColumn(i),
                    Parameters = new Dictionary<string, object>
                    {
                        { "stepsize", formation.SideMargin*(sideMarginFactor-1) / stepsPerRow },
                        { "direction",  fromCenter >= 0 ? 90 : -90 }
                    }                    
                });
            }
            formation.SideMargin *= sideMarginFactor;
            return new Element
            {
                Name = name,
                StartFormation = formation,
                GroupActions = groupActions.ToArray()
            };
        }

        public Element GrosseWendeComplete(string name, RowsFormation formation, int timeAfterWideFormation = 2)
        {
            var wideFormation = BreiteFormation(name + "_1", formation);
            var wideFormationForward = MoveForward(name + "_2", formation, wideFormation.Duration + timeAfterWideFormation);
            var wende = GrosseWende(name + "_3", formation);
            var slimFormation = BreiteFormation(name + "_4", formation, sideMarginFactor: 0.5);
            var slimFormationForward = MoveForward(name + "_2", formation, slimFormation.Duration);

            return new Element
            {
                Name = name,
                StartFormation = formation,
                SubElements = new SubElement[]
                {
                    new SubElement
                    {
                        Element = wideFormation
                    },
                    new SubElement
                    {
                        Element = wideFormationForward
                    },
                    new SubElement
                    {
                        StartTime = wideFormation.Duration,
                        Element = wende
                    },
                    new SubElement
                    {
                        StartTime = wideFormationForward.Duration + wende.Duration,
                        Element = slimFormation
                    },
                    new SubElement
                    {
                        StartTime = wideFormationForward.Duration + wende.Duration,
                        Element = slimFormationForward
                    }
                }
            };
        }

        public Element GrosseWende(string name, RowsFormation formation)
        {
            var actions = new List<GroupAction>();



            return new Element
            {
                Name = name,
                StartFormation = formation,
                GroupActions = actions.ToArray()
            };
        }
    }
}
