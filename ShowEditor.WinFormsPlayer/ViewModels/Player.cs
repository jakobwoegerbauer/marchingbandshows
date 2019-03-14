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
        public int Index { get; }
        public Position Position { get; set; }
        public bool IsSelected { get; set; }

        public Player(int index, Position position)
        {
            Index = index;
            Position = position;
        }

        public void Draw(float scale, float mx, float my, float radius, Graphics graphics)
        {
            using (Pen p = new Pen(IsSelected ? Color.Green : Color.Black))
            {
                float cx = scale * (float)Position.X + mx;
                float cy = scale * (float)Position.Y + my;
                graphics.DrawEllipse(p, cx - radius, cy - radius, 2 * radius, 2 * radius);

                if(IsSelected)
                    graphics.FillEllipse(p.Brush, cx - radius, cy - radius, 2 * radius, 2 * radius);

                float fx = (float)Math.Round(Math.Cos(PositionHelper.ToRadians(Position.Rotation)) * scale, 1);
                float fy = (float)Math.Round(Math.Sin(PositionHelper.ToRadians(Position.Rotation)) * scale, 1);
                graphics.DrawLine(p, cx, cy, cx + fx, cy + fy);
            }
        }
    }
}
