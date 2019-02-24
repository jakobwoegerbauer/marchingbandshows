using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShowEditor.Data;
using ShowEditor.Simulator.ExecutionGraph;

namespace ShowEditor.Test
{
    [TestClass]
    public class TestExecutionGraph
    {
        [TestMethod]
        public void TestCollectEdges()
        {

            /*
             * 01       2 3                 012
             * 23   ->      local indizes:  345
             * 45       4 5                 678
             * 
             * local:  8 follows 2
             * global: 5 follows 3
             * edges should be: 3->5
             * 
             * second dependency should be ignored because of the time offset.
             */

            /*
            Element show = new Element
            {
                Name = "My Show",
                StartFormation = new Formation(new Position[] {
                    new Position(0,0,0),
                    new Position(0,1,1),
                    new Position(1,0,2),
                    new Position(1,1,3),
                    new Position(2,0,4),
                    new Position(2,1,5)
                })
            };

            show.SubElements = new SubElement[]
            {
                new SubElement
                {
                    StartTime = 0,
                    Transformation = new Element
                    {
                        Name = "3x3 Square",
                        StartFormation = new Formation(new Position[]
                            {
                            new Position(0,0,0),
                            new Position(0,1,1),
                            new Position(0,2,2),
                            new Position(1,0,3),
                            new Position(1,1,4),
                            new Position(1,2,5),
                            new Position(2,0,6),
                            new Position(2,1,7),
                            new Position(2,2,8)
                        }),
                        GroupActions = new GroupAction[]
                        {
                            new GroupAction
                            {
                                Positions = new int[]{8},
                                ActionType = "Follow",
                                Dependency =
                                new MovementDependency
                                {
                                    Position = 2,
                                    Concurrent = true
                                },
                                Duration = 10
                            },
                            new GroupAction
                            {
                                Positions = new int[]{0, 2},
                                ActionType = "MoveForward",
                                Duration = 10
                            }
                        }
                    },
                    PositionMapping = new int[]
                    {
                        -1, -1, 0, 2, 6, 8
                    }
                }
            };

            string s = show.ToJSON();

            Graph g = new Graph(show, new Simulator.ActionExecutors.ActionManager());
            while (true)
            {
                g.CalculateStep();
                var positions = g.GetPositions();
                g.ExecuteStep();
            }*/
        }
    }
}
