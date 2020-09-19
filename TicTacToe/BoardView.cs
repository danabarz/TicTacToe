using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
     class BoardView
     {
        private const int printBoardStartLocation = 2;
        private const int spaceBetweenPieces = 4;
        private const int boardDimensions = 3;
        private const int distanceBetweenBoards = boardDimensions * spaceBetweenPieces;
        private const int numberOfSubBOards = boardDimensions * boardDimensions;

        public event EventHandler<EventArgs> SetColorBoard;

        protected virtual void OnPrintBoardView()
        {
            if (SetColorBoard != null)
            {
                SetColorBoard(this, EventArgs.Empty);
            }
        }
        
        public void PrintBoardView(Board gameBoard)
        {
            Console.SetCursorPosition(gameBoard.BoardOriginLeft, gameBoard.BoardOriginTop);
            for (int i = gameBoard.BoardOriginLeft; i <= gameBoard.BoardOriginLeft + distanceBetweenBoards; i += spaceBetweenPieces)
            {
                for (int j = gameBoard.BoardOriginTop; j <= gameBoard.BoardOriginTop + distanceBetweenBoards; j++)
                {
                    if (i <= distanceBetweenBoards * boardDimensions && i % distanceBetweenBoards == 0 || i <= distanceBetweenBoards * boardDimensions && j % distanceBetweenBoards == 0)
                    {
                        OnPrintBoardView();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    Console.SetCursorPosition(i, j);
                    Console.Write("#");
                }
            }

            for (int j = gameBoard.BoardOriginTop; j <= gameBoard.BoardOriginTop + distanceBetweenBoards; j += spaceBetweenPieces)
            {
                for (int i = gameBoard.BoardOriginLeft; i <= gameBoard.BoardOriginLeft + distanceBetweenBoards; i++)
                {
                    if (i <= distanceBetweenBoards * boardDimensions && i % distanceBetweenBoards == 0 || i <= distanceBetweenBoards * boardDimensions && j % distanceBetweenBoards == 0)
                    {
                        OnPrintBoardView();
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

        private void PrintPieces(PlayerMarker?[,] gameBoard, int boardOriginLeft, int boardOriginTop)
        {
            for (int row = 0; row < boardDimensions; row++)
            {
                for (int col = 0; col < boardDimensions; col++)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(boardOriginLeft + printBoardStartLocation + col * spaceBetweenPieces, boardOriginTop + printBoardStartLocation + row * spaceBetweenPieces);
                    Console.Write(gameBoard[row, col]);
                }
                Console.WriteLine();
            }
        }

        public void OnBoardUpdated(object source, TicTacToeEventArgs args)
        {
            PrintPieces(args.GameBoard, args.BoardOriginLeft, args.BoardOriginTop);
        }

        public void OnBoardsInitialized(object source, TicTacToeEventArgs args)
        {
            PrintBoardView(args.SummaryBoard);
            for (int i = 0; i < numberOfSubBOards; i++)
            {
                PrintBoardView(args.SubBoards[i]);
            }
        }
    }
}

