using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    public class MiniMax
    {
        private const int BoardDimensions = 3;
        private readonly Random rand = new Random();
        readonly EvaluationFunction evaluateFunction = new EvaluationFunction();


        //todo: improve this method
        private int Minimax(Board gameBoard, int depth, Boolean isMax, PlayerMarker playerMarker)
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
                gameBoard.GameBoard[row, col] = gameBoard.GetOponenentPiece(playerMarker);
                best = Math.Min(best, Minimax(gameBoard, depth + 1, !isMax, playerMarker));
                gameBoard.GameBoard[row, col] = null;

                return best;
            }
        }
        public PlayerMove FindBestMove(Board gameBoard, PlayerMarker playerMarker)
        {
            int bestVal = int.MinValue;
            int row = -1;
            int col = -1;

            for (int i = 0; i < BoardDimensions; i++)
            {
                for (int j = 0; j < BoardDimensions; j++)
                {
                    if (gameBoard.GameBoard[i, j] == null)
                    {
                        gameBoard.GameBoard[i, j] = playerMarker;
                        int moveVal = Minimax(gameBoard, 0, false, playerMarker);
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

        private Tuple<int, int> TravrseBoardCell(Board gameBoard, PlayerMarker playerMarker)
        {
            for (int i = 0; i < BoardDimensions; i++)
            {
                for (int j = 0; j < BoardDimensions; j++)
                {
                    if (gameBoard.GameBoard[i, j] == null)
                    {
                        return Tuple.Create(i, j);
                    }
                }
            }
            return null;
        }



        /*
        public PlayerMove FindBestMove(List<SubBoard> subBoards, PlayerMarker playerMarker)
        {

            int bestVal = int.MinValue;
            int row = int.MinValue;
            int col = int.MinValue;
            var bestSubBoard = subBoards[0];

            foreach (SubBoard board in subBoards)
            {
                if (board.Winner != null)
                {
                    continue;
                }

                for (int i = 0; i < BoardDimensions; i++)
                {
                    for (int j = 0; j < BoardDimensions; j++)
                    {
                        if (board.GameBoard[i, j] == null)
                        {
                            board.GameBoard[i, j] = playerMarker;
                            int moveVal = Minimax(board, 0, false, playerMarker);
                            board.GameBoard[i, j] = null;
                            if (moveVal > bestVal)
                            {
                                row = i;
                                col = j;
                                bestVal = moveVal;
                                bestSubBoard = board;
                            }
                        }
                    }
                }
            }
            return new PlayerMove(bestSubBoard, row, col, playerMarker);
        }*/
    }
}
