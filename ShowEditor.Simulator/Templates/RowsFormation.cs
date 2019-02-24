using ShowEditor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Simulator.Templates
{
    public class RowsFormation : Formation
    {
        public int Rows { get { return (int)Data.Parameters["rows"]; } set { Data.Parameters["rows"] = value; } }
        public int Columns { get { return (int)Data.Parameters["columns"]; } set { Data.Parameters["columns"] = value; } }
        public double Depth { get { return (double)Data.Parameters["depth"]; } set { Data.Parameters["depth"] = value; } }
        public double SideMargin { get { return (double)Data.Parameters["sidemargin"]; } set { Data.Parameters["sidemargin"] = value; } }

        public RowsFormation() : base("RowsFormation")
        {
        }

        public RowsFormation(FormationData data): this()
        {
            Data = data;
        }

        public RowsFormation(int rows, int columns, double depth = 1, double sidemargin = 1) : this()
        {
            Data = new FormationData(FormationTypeIdentifier)
            {
                Name = FormationTypeIdentifier,
                Positions = new Position[rows * columns]
            };
            Rows = rows;
            Columns = columns;
            Depth = depth;
            SideMargin = sidemargin;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    //Data.Positions[row * columns + col] = new Position((rows - row) * Depth, col * SideMargin, 0);
                    Data.Positions[row * columns + col] = new Position(col * SideMargin, -(rows - row) * Depth, -90);
                }
            }
        }

        public int[] GetRow(int index)
        {
            int[] row = new int[Columns];
            for (int i = 0; i < Columns; i++)
            {
                row[i] = index * Columns + i;
            }
            return row;
        }

        public override Formation FromData(FormationData data)
        {
            return new RowsFormation(data);
        }
    }
}
