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
        public int Rows { get { return Convert.ToInt32(Data.Parameters["rows"]); } set { Data.Parameters["rows"] = value; } }
        public int Columns { get { return Convert.ToInt32(Data.Parameters["columns"]); } set { Data.Parameters["columns"] = value; } }
        public double Depth { get { return Convert.ToDouble(Data.Parameters["depth"]); } set { Data.Parameters["depth"] = value; } }
        public double SideMargin { get { return Convert.ToDouble(Data.Parameters["sidemargin"]); } set { Data.Parameters["sidemargin"] = value; } }

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

        public int GetRelativePosition(int index, int relDepRow, int relDepCol)
        {
            int curRow = index / Columns;
            int curCol = index % Columns;
            return (curRow + relDepRow) * Columns + curCol + relDepCol;
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

        public int[] GetColumn(int index)
        {
            int[] column = new int[Rows];
            for(int i = 0; i < Rows; i++)
            {
                column[i] = i * Columns + index;
            }
            return column;
        }

        public override Formation FromData(FormationData data)
        {
            return new RowsFormation(data);
        }
    }
}
