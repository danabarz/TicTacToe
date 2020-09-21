using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class MiniMax
    {
        readonly EvaluationFunction evaluateFunction = new EvaluationFunction();

        public int Minimax(Board gameBoard, int depth, Boolean isMax, PlayerMarker playerMarker)
        {
            int score = evaluateFunction.Evaluate(gameBoard, playerMarker);
            int best = int.MinValue;

            if (score == int.MaxValue)
            {
                return score - depth;
            }
            else if (score == int.MinValue)
            {
                return score + depth;
            }
            else if (gameBoard.CheckIfGameOver() == PlayerMarker.Tie)
            {
                return 0;
            }
            else if(isMax)
            {
                var (row, col) = TravrseBoardCell(gameBoard, playerMarker);
                gameBoard.GameBoard[row, col] = playerMarker;
                best = Math.Max(best, Minimax(gameBoard, depth + 1, !isMax, playerMarker));
                gameBoard.GameBoard[row, col] = null;

                return best;
            }
            else
            {
               best = int.MaxValue;
                var (row, col) = TravrseBoardCell(gameBoard, playerMarker);
                gameBoard.GameBoard[row, col] = playerMarker;
                best = Math.Min(best, Minimax(gameBoard, depth + 1, !isMax, playerMarker));
                gameBoard.GameBoard[row, col] = null;
                return best;
            }
        }


        private Tuple<int, int> TravrseBoardCell(Board gameBoard, PlayerMarker playerMarker)
        {
            for (int i = 0; i < gameBoard.Rows; i++)
            {
                for (int j = 0; j < gameBoard.Columns; j++)
                {
                    if (gameBoard.GameBoard[i, j] == null)
                    {
                        return Tuple.Create(i, j);
                    }
                }
            }
            return null;
        }

        public PlayerMove FindBestMove(Board gameBoard, PlayerMarker playerMarker)
        { 
            int bestVal = -1000;
            int row = -1;
            int col = -1;

            for (int i = 0; i < gameBoard.Rows; i++)
            {
                for (int j = 0; j < gameBoard.Columns; j++)
                {
                    if (gameBoard.GameBoard[i, j] == null)
                    {
                        gameBoard.GameBoard[i, j] = playerMarker;
                        int moveVal = Minimax(gameBoard, 0, true, playerMarker);
                        gameBoard.GameBoard[i, j] = null;
                        if (moveVal > bestVal)
                        {
                            row = i;
                            col = j;
                            bestVal = moveVal;
                        }
                    }
                }
            }
            return new PlayerMove(gameBoard, row, col, playerMarker);
        }
    }
}
