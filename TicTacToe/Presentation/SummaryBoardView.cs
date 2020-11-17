using System;
using System.Drawing;
using TicTacToe.Logic;

namespace TicTacToe.Presentation
{
    public class SummaryBoardView : BoardView
    {
        private readonly MainBoard _mainBoard;
        public SummaryBoardView(MainBoard mainBoard, Point topLeft)
        {
            _mainBoard = mainBoard;
            BoundingBox = new BoundingBox(topLeft, height, width);
            _mainBoard.UpdatedBoardPieces += OnUpdatedBoardPieces;
        }

        public override void PrintMarkers()
        {
            for (int row = 0; row < Game.BoardDimensions; row++)
            {
                for (int col = 0; col < Game.BoardDimensions; col++)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(BoundingBox.TopLeft.X + PrintBoardStartLocation + col * SpaceBetweenPieces, BoundingBox.TopLeft.Y + PrintBoardStartLocation + row * SpaceBetweenPieces);

                    if (_mainBoard[row, col].Winner == PlayerMarker.Tie)
                    {
                        Console.Write("-");
                    }

                    else
                    {
                        Console.Write(_mainBoard[row, col].Winner);
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
