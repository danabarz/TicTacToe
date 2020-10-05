using System;
using TicTacToe.Logic;

namespace TicTacToe.Presentation
{
    public abstract class BoardView
    {
        public const int SpaceBetweenPieces = 4;
        protected const int PrintBoardStartLocation = 2;
        protected const int height = Game.BoardDimensions * SpaceBetweenPieces;
        protected const int width = Game.BoardDimensions * SpaceBetweenPieces;
        public BoundingBox BoundingBox { get; protected set; }

        public void PrintBoard()
        {
            Console.SetCursorPosition(BoundingBox.TopLeft.X, BoundingBox.TopLeft.Y);

            for (int i = BoundingBox.TopLeft.X; i <= BoundingBox.BottomRight.X; i += SpaceBetweenPieces)
            {
                for (int j = BoundingBox.TopLeft.Y; j <= BoundingBox.BottomRight.Y; j++)
                {
                    CheckWhichColorAndPrint(i, j);
                }
            }
            
            for (int j = BoundingBox.TopLeft.Y; j <= BoundingBox.BottomRight.Y; j += SpaceBetweenPieces)
            {
                for (int i = BoundingBox.TopLeft.X; i <= BoundingBox.BottomRight.X; i++)
                {
                    CheckWhichColorAndPrint(i, j);
                }
            }

            void CheckWhichColorAndPrint(int i, int j)
            {
                if (i <= BoundingBox.BottomRight.X && (i % (BoundingBox.Width / Game.BoardDimensions) == 0 || j % (BoundingBox.Height / Game.BoardDimensions) == 0))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                Console.SetCursorPosition(i, j);
                Console.Write("#");
            }
        }

        public abstract void PrintMarkers();

        protected void OnUpdatedBoardPieces(object? source, EventArgs args)
        {
            PrintMarkers();
        }
    }
}

