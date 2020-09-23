using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TicTacToe
{
     public class BoardView
     {
        private const int PrintBoardStartLocation = 2;
        private const int SpaceBetweenPieces = 4;
        private const int BoardDimensions = 3;
        private const int DistanceBetweenBoards = BoardDimensions * SpaceBetweenPieces;
        private const int NumberOfSubBoards = BoardDimensions * BoardDimensions;



        //todo: create constructor with board origin location - get board as parmater
        public event EventHandler<EventArgs> ChangedBoardColor;

        protected virtual void OnPrintedBoardView()
        {
            ChangedBoardColor?.Invoke(this, EventArgs.Empty);
        }
        
        public void PrintBoardView(Board gameBoard)
        {
            Console.SetCursorPosition(gameBoard.BoardOriginLocation.X, gameBoard.BoardOriginLocation.Y);
            for (int i = gameBoard.BoardOriginLocation.X; i <= gameBoard.BoardOriginLocation.X + DistanceBetweenBoards; i += SpaceBetweenPieces)
            {
                for (int j = gameBoard.BoardOriginLocation.Y; j <= gameBoard.BoardOriginLocation.Y + DistanceBetweenBoards; j++)
                {
                    if (i <= DistanceBetweenBoards * BoardDimensions && i % DistanceBetweenBoards == 0 || i <= DistanceBetweenBoards * BoardDimensions && j % DistanceBetweenBoards == 0)
                    {
                        OnPrintedBoardView();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    Console.SetCursorPosition(i, j);
                    Console.Write("#");
                }
            }

            for (int j = gameBoard.BoardOriginLocation.Y; j <= gameBoard.BoardOriginLocation.Y + DistanceBetweenBoards; j += SpaceBetweenPieces)
            {
                for (int i = gameBoard.BoardOriginLocation.X; i <= gameBoard.BoardOriginLocation.X + DistanceBetweenBoards; i++)
                {
                    if (i <= DistanceBetweenBoards * BoardDimensions && i % DistanceBetweenBoards == 0 || i <= DistanceBetweenBoards * BoardDimensions && j % DistanceBetweenBoards == 0)
                    {
                        OnPrintedBoardView();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    Console.SetCursorPosition(i, j);
                    Console.Write("#");
                }
            }
        }

        private void PrintMarkers(PlayerMarker?[,] gameBoard, Point BoardOriginLocation)
        {
            for (int row = 0; row < BoardDimensions; row++)
            {
                for (int col = 0; col < BoardDimensions; col++)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(BoardOriginLocation.X + PrintBoardStartLocation + col * SpaceBetweenPieces, BoardOriginLocation.Y + PrintBoardStartLocation + row * SpaceBetweenPieces);
                    if (gameBoard[row, col] == PlayerMarker.Tie)
                    {
                        Console.Write("-");
                    }
                    else
                    {
                        Console.Write(gameBoard[row, col]);
                    }
                }
                Console.WriteLine();
            }
        }

        public void OnBoardUpdated(object source, BoardEventArgs args)
        {
            PrintMarkers(args.GameBoard, args.BoardOriginLocation);
        }

        public void OnBoardsInitialized(object source, GameEventArgs args)
        {
            PrintBoardView(args.SummaryBoard);
            for (int i = 0; i < NumberOfSubBoards; i++)
            {
                PrintBoardView(args.SubBoards[i]);
            }
        }
    }
}

