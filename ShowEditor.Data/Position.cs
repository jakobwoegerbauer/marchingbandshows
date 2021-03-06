﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.Data
{
    /// <summary>
    /// Position of a player
    /// </summary>
    public class Position
    {
        /// <summary>
        /// X coordinate
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y coordinate
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// The rotation in degrees. No rotation is looking in the positive x direction
        /// </summary>
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
