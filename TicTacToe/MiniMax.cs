using System;

namespace TicTacToe
{
    public class MiniMax
    {
        readonly EvaluationFunction evaluateFunction = new EvaluationFunction();

        private int Minimax(Board gameBoard, int depth, Boolean isMax, PlayerMarker playerMarker)
        {
            int value = evaluateFunction.Evaluate(gameBoard, playerMarker);

            if (value == int.MaxValue)
            {
                return value - depth;
            }
            else if (value == int.MinValue)
            {
                return value + depth;
            }
            else if (gameBoard.CheckIfGameOver() == PlayerMarker.Tie)
            {
                return 0;
            }
            else if (isMax)
            {
                return AddMarkerAndCheckBoardValue(int.MinValue);
            }
            return AddMarkerAndCheckBoardValue(int.MaxValue);


            int AddMarkerAndCheckBoardValue(int defultValue)
            {
                int bestValue = defultValue;
                var (row, col) = GetEmptyCell(gameBoard);
                gameBoard.GameBoard[row, col] = playerMarker;
                if (defultValue == int.MinValue)
                {
                    bestValue = Math.Max(bestValue, Minimax(gameBoard, depth + 1, !isMax, gameBoard.GetOponenentPiece(playerMarker)));
                }
                else
                {
                    bestValue = Math.Min(bestValue, Minimax(gameBoard, depth + 1, !isMax, gameBoard.GetOponenentPiece(playerMarker)));
                }
                gameBoard.GameBoard[row, col] = null;
                return bestValue;
            }
        }

        private Tuple<int, int> GetEmptyCell(Board gameBoard)
        {
            for (int i = 0; i < gameBoard.Dimensions; i++)
            {
                for (int j = 0; j < gameBoard.Dimensions; j++)
                {
                    if (gameBoard.GameBoard[i, j] == null)
                    {
                        return Tuple.Create(i, j);
                    }
                }
            }
            return null;
        }

        public PlayerMove FindBestMove(Game game, PlayerMarker playerMarker)
        {
            var (boardRow, boardColumn) = UpdateBoardForMinMaxCheck(game._summaryBoard);
            var (cellRow, cellColumn) = UpdateBoardForMinMaxCheck(game._subBoards[boardRow, boardColumn]);
            return new PlayerMove(game._subBoards[boardRow, boardColumn], cellRow, cellColumn, playerMarker);


            Tuple<int, int> UpdateBoardForMinMaxCheck(Board gameBoard)
            {
                int bestValue = int.MinValue;
                int row = int.MinValue;
                int col =int.MinValue;

                for (int i = 0; i < gameBoard.Dimensions; i++)
                {
                    for (int j = 0; j < gameBoard.Dimensions; j++)
                    {
                        if (gameBoard.GameBoard[i, j] == null)
                        {
                            gameBoard.GameBoard[i, j] = playerMarker;
                            int moveValue = Minimax(gameBoard, 0, false, gameBoard.GetOponenentPiece(playerMarker));
                            gameBoard.GameBoard[i, j] = null;
                            if (moveValue > bestValue)
                            {
                                row = i;
                                col = j;
                                bestValue = moveValue;
                            }
                        }
                    }
                }
                return Tuple.Create(row, col);
            }
        }
    }
}
