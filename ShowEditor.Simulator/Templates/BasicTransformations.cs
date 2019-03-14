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
                groupActions.AddRange(Curve(duration, (j + 1) * formation.SideMargin, degree, positions: p));
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
                groupActions.Add(GroupActions.FollowDirectFront(i, formation, timeFinishedTurn - 2, duration / 2, delay: 2, followers: row));
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

        public Element Wait(string name, Formation formation, int duration)
        {
            return new Element
            {
                Name = name,
                StartFormation = formation,
                GroupActions = new GroupAction[]
                {
                    new GroupAction
                    {
                        ActionType = ActionManager.DefaultActions.WAIT,
                        Duration = duration
                    }
                }
            };
        }

        public Element BreiteFormation(string name, RowsFormation formation, int stepsPerRow = 2, double sideMarginFactor = 2)
        {
            List<GroupAction> groupActions = new List<GroupAction>();

            for (int i = 0; i < formation.Columns; i++)
            {
                double fromCenter = i - (formation.Columns - 1) / 2.0;
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
                        StartTime = wideFormationForward.Duration,
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

        public Element GrosseWende(string name, RowsFormation formation, int rotDuration = 16, double slowStep = 0.4)
        {
            var actions = new List<GroupAction>();
            var firstRow = formation.GetRow(0);

            int endOfLastRowTurn = rotDuration + (formation.Rows - 1) * (rotDuration / 4);
            double posFrontRowWhenLastTurnEnds = slowStep * (endOfLastRowTurn - rotDuration);
            double posSecondToLastRowWhenLastTurnEnds = posFrontRowWhenLastTurnEnds - (formation.Rows - 2) * formation.Depth;
            int durationAll = endOfLastRowTurn + (int)(posSecondToLastRowWhenLastTurnEnds / (StepSize - slowStep)) - 1;

            int[] frontRow = formation.GetRow(0);
            for (int j = 0; j < formation.Columns; j++)
            {
                int fromCenter = j - (formation.Columns - 1) / 2;
                int[] p = new int[1];
                p[0] = frontRow[j];
                actions.AddRange(Curve(rotDuration, Math.Abs(0.5+2*fromCenter)*formation.SideMargin/2, fromCenter >= 0 ? 180 : -180, positions: p));
            }
            actions.Add(GroupActions.MoveForward(durationAll - rotDuration, rotDuration, slowStep, positions: frontRow));

            int[] rowBefore = firstRow;
            for (int i = 1; i < formation.Rows; i++)
            {
                int[] row = formation.GetRow(i);
                int timeFinishedTurn = i * (rotDuration / 4) + rotDuration;
                actions.Add(GroupActions.FollowDirectFront(i, formation, i * rotDuration / 4 + rotDuration, rotDuration / 4, followers: row));
                actions.Add(GroupActions.MoveUpTo(rowBefore[0], durationAll - timeFinishedTurn, row, delay: timeFinishedTurn, stepsize: StepSize, depth: formation.Depth));
                rowBefore = row;
            }

            return new Element
            {
                Name = name,
                StartFormation = formation,
                GroupActions = actions.ToArray()
            };
        }

        private IEnumerable<GroupAction> Curve(int duration, double radius, double degrees, int delay = 0, double priority = 0, int[] positions = null)
        {
            double eps = 0.000001;
            return new List<GroupAction>
            {
                GroupActions.Rotate(degrees / 2, duration: duration, delay: delay, priority: priority - eps, positions: positions),
                GroupActions.MoveForward(duration, stepsize: radius * PositionHelper.ToRadians(Math.Abs(degrees) / duration), delay: delay, priority: priority, positions: positions),
                GroupActions.Rotate(degrees / 2, duration: duration, delay: delay, priority: priority + eps, positions: positions)
            };
        }
    }
}
