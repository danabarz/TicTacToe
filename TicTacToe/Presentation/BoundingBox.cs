using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TicTacToe.Presentation
{
    public struct BoundingBox
    {
        public BoundingBox(Point topLeft, int width, int height)
        {
            TopLeft = topLeft;
            TopRight = TopLeft + new Size(width, 0);
            BottomLeft = TopLeft + new Size(0, height);
            BottomRight = TopLeft + new Size(width, height);
            Width = width;
            Height = height;
        }

        public BoundingBox(Point topLeft, Point bottomRight)
        {
            Width = bottomRight.X - topLeft.X;
            Height = bottomRight.Y - topLeft.Y;
            TopLeft = topLeft;
            TopRight = TopLeft + new Size(Width, 0);
            BottomLeft = TopLeft + new Size(0, Height);
            BottomRight = bottomRight;
        }

        public Point TopLeft { get; }
        public Point TopRight { get; }
        public Point BottomLeft { get; }
        public Point BottomRight { get; }
        public int Height { get; }
        public int Width { get; }
    }
}
