using ShowEditor.Data;
using ShowEditor.Simulator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowEditor.WinFormsPlayer.ViewModels
{
    class Player
    {
        public static float DrawRadius = 3;
        public static float CenterX = 20;
        public static float CenterY = 700;
        public static float Scale = 10;

        public int Index { get; }
        public Position Position { get; set; }
        public bool IsSelected { get; set; }

        public Player(int index, Position position)
        {
            Index = index;
            Position = position;
        }

        public void Draw(Graphics graphics)
        {
            using (Pen p = new Pen(IsSelected ? Color.Green : Color.Black))
            {
                float cx = Scale * (float)Position.X + CenterX;
                float cy = Scale * (float)Position.Y + CenterY;
                graphics.DrawEllipse(p, cx- DrawRadius, cy- DrawRadius, 2 * DrawRadius, 2 * DrawRadius);

                if(IsSelected)
                    graphics.FillEllipse(p.Brush, cx - DrawRadius, cy - DrawRadius, 2 * DrawRadius, 2 * DrawRadius);

                float fx = (float)Math.Round(Math.Cos(PositionHelper.ToRadians(Position.Rotation)) * Scale, 1);
                float fy = (float)Math.Round(Math.Sin(PositionHelper.ToRadians(Position.Rotation)) * Scale, 1);
                graphics.DrawLine(p, cx, cy, cx+ fx, cy+ fy);
            }
        }

        public bool IsHovered(int x, int y)
        {
            return Math.Sqrt(Math.Pow(x - CenterX - Scale * Position.X, 2) + Math.Pow(y - CenterY - Scale * Position.Y, 2)) <= DrawRadius;
        }
    }
}
