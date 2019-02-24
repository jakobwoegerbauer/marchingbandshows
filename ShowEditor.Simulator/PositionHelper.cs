using ShowEditor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Simulator
{
    public class PositionHelper
    {
        public static double GetDistance(Position a, Position b)
        {
            return Math.Sqrt(Math.Pow(Math.Abs(a.X - b.X), 2) + Math.Pow(Math.Abs(a.Y - b.Y), 2));
        }

        public static double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        public static double ToDegrees(double rad)
        {
            return rad * 180.0 / Math.PI;
        }

        public static Position Forward(Position a, double distance)
        {
            Position pos = a.Copy();
            pos.X += Math.Cos(ToRadians(pos.Rotation)) * distance;
            pos.Y += Math.Sin(ToRadians(pos.Rotation)) * distance;
            return pos;
        }
    }
}
