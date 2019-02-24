using ShowEditor.Data;
using ShowEditor.Simulator;
using ShowEditor.Simulator.Templates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShowEditor.ConsoleTestEditor
{
    class Program
    {
        static void Main(string[] args)
        {/*
            var formation = new RowsFormation(6, 4);

            int[][] groups = new int[][]
            {
                new int[]{0,1,4,5,8,9},
                new int[]{2,3,6,7,10,11},
                new int[]{12,13,16,17,20,21},
                new int[]{14,15,18,19,22,23}
            };

            Element show = new Element
            {
                Name = "My first show!",
                StartFormation = formation,
                GroupActions = new GroupAction[]
                {
                    GroupActions.MoveForward(4),
                    GroupActions.Turn(delay: 4, positions: Combination.Range(0, 11)),
                    GroupActions.Turn(delay: 4, toRight: true, positions: Combination.Range(12, 23)),
                    GroupActions.MoveForward(4, delay: 4),
                    GroupActions.Turn(delay: 8, positions: groups[0]),
                    GroupActions.Turn(delay: 8, toRight: true, positions: groups[1]),
                    GroupActions.Turn(delay: 8, toRight: true, positions: groups[2]),
                    GroupActions.Turn(delay: 8, positions: groups[3]),
                    GroupActions.MoveForward(4, delay: 8),
                    GroupActions.Turn(delay: 12, positions: groups[0]),
                    GroupActions.Turn(delay: 12, toRight: true, positions: groups[1]),
                    GroupActions.Turn(delay: 12, toRight: true, positions: groups[2]),
                    GroupActions.Turn(delay: 12, positions: groups[3]),
                    GroupActions.MoveForward(4, delay: 12),
                    GroupActions.Turn(delay: 16, positions: groups[0]),
                    GroupActions.Turn(delay: 16, toRight: true, positions: groups[1]),
                    GroupActions.Turn(delay: 16, toRight: true, positions: groups[2]),
                    GroupActions.Turn(delay: 16, positions: groups[3]),
                    GroupActions.MoveForward(4, delay: 16),
                }
            };

            Element show2 = new Element
            {
                Name = "2nd",
                StartFormation = formation,
                GroupActions = new GroupAction[]
                {
                    GroupActions.MoveForward(5)
                },
                SubElements = new SubElement[]
                {
                    new SubElement
                    {
                        StartTime = 10,
                        Transformation = show
                    }
                }
            };

            Show show4 = new Show(new BasicTransformations(1).Schwenkung("kl. Wende", formation));

            var x = show4.ToJSON();

            var s = Show.FromJSON(x, formationGenerators);

            ShowSimulator simulator = new ShowSimulator(s);
            PrintPositions(simulator.GetPositions(), simulator.Time);
            Console.ReadLine();
            while (true)
            {
                simulator.ExecuteStep();
                PrintPositions(simulator.GetPositions(), simulator.Time);
                Console.ReadLine();
            }*/
        }

        private static void PrintPositions(Position[] positions, int time)
        {
            for (int y = -6; y < 8; y++)
            {
                for (int x = 0; x < Console.WindowWidth; x++)
                {
                    int i = Array.FindIndex(positions, p => (int)p.X == x && (int)p.Y == y);
                    Console.Write(O(i));
                }
            }
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine("time: " + time);
        }

        private static string O(int n)
        {
            int[][] groups = new int[][]
            {
                new int[]{0,1,4,5,8,9},
                new int[]{2,3,6,7,10,11},
                new int[]{12,13,16,17,20,21},
                new int[]{14,15,18,19,22,23}
            };

            for (int i = 0; i < 4; i++)
            {
                if (groups[i].Contains(n))
                {
                    return i.ToString();
                }
            }
            return " ";
        }
    }
}
