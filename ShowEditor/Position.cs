using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Data
{
    public class Position
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Rotation { get; set; }

        public Position(double x, double y, double rotation)
        {
            X = x;
            Y = y;
            Rotation = rotation;
        }

        public Position Copy()
        {
            return new Position(X, Y, Rotation);
        }

        public override string ToString()
        {
            return $"x: {X}   y: {Y}   r: {Rotation}";
        }
    }
}
