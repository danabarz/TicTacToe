using System;
using System.Drawing;
using TicTacToe.Logic;

namespace TicTacToe.Presentation
{
    public class SubBoardView : BoardView
    {
        private readonly SubBoard _subBoard;
        public SubBoardView(SubBoard subBoard, Point topLeft)
        {
            _subBoard = subBoard;
            BoundingBox = new BoundingBox(topLeft, height, width);
            _subBoard.UpdatedBoardPieces += OnUpdatedBoardPieces;
        }

        public override void PrintMarkers()
        {
            for (int row = 0; row < Game.BoardDimensions; row++)
            {
                for (int col = 0; col < Game.BoardDimensions; col++)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(BoundingBox.TopLeft.X + PrintBoardStartLocation + col * SpaceBetweenPieces, BoundingBox.TopLeft.Y + PrintBoardStartLocation + row * SpaceBetweenPieces);
                    Console.Write(_subBoard[row, col].OwningPlayer);
                }
                Console.WriteLine();
            }
        }
    }
}
