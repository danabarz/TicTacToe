using System;

namespace TicTacToe.Logic
{
    public class MinMax
    {
        private readonly EvaluationFunction evaluateFunction = new EvaluationFunction();

        private int Minmax(Board<AtomicCell> gameBoard, int depth, bool isMax, PlayerMarker playerMarker)
        {
            int value = evaluateFunction.Evaluate(gameBoard, playerMarker);
            int emptyLocationIndex = 0;

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
                var openLocations = gameBoard.FindOpenMoves();
                gameBoard[openLocations[emptyLocationIndex].Item1, openLocations[emptyLocationIndex].Item2].SetOwningPlayerIfAvailable(playerMarker);
                if (defultValue == int.MinValue)
                {
                    bestValue = Math.Max(bestValue, Minmax(gameBoard, depth + 1, !isMax, gameBoard.GetOponenentPiece(playerMarker)));
                }
                else
                {
                    bestValue = Math.Min(bestValue, Minmax(gameBoard, depth + 1, !isMax, gameBoard.GetOponenentPiece(playerMarker)));
                }
                gameBoard[openLocations[emptyLocationIndex].Item1, openLocations[emptyLocationIndex].Item2].SetOwningPlayerToNull();
                emptyLocationIndex++;
                return bestValue;
            }
        }


        public PlayerMove FindBestMove(MainBoard mainBoard, PlayerMarker playerMarker)
        {
            int bestValue = int.MinValue;
            int boardRow = int.MinValue;
            int boardColumn = int.MinValue;
            int cellRow = int.MinValue;
            int cellColumn = int.MinValue;
            int emptyLocationIndex = 0;

            for (int i = 0; i < Game.BoardDimensions; i++)
            {
                for (int j = 0; j < Game.BoardDimensions; j++)
                {
                    var openLocations = mainBoard[i, j].FindOpenMoves();
                    if (emptyLocationIndex >= openLocations.Count || mainBoard[i, j].Winner != null)
                    {
                        continue;
                    }
                    if (mainBoard[i, j][openLocations[emptyLocationIndex].Item1, openLocations[emptyLocationIndex].Item2].SetOwningPlayerIfAvailable(playerMarker))
                    {
                        int moveValue = Minmax(mainBoard[i, j], 0, false, mainBoard[i, j].GetOponenentPiece(playerMarker));
                        mainBoard[i, j][openLocations[emptyLocationIndex].Item1, openLocations[emptyLocationIndex].Item2].SetOwningPlayerToNull();
                        if (moveValue >= bestValue)
                        {
                            boardRow = i;
                            boardColumn = j;
                            cellRow = openLocations[emptyLocationIndex].Item1;
                            cellColumn = openLocations[emptyLocationIndex].Item2;
                            bestValue = moveValue;
                        }
                    }
                }

                emptyLocationIndex++;
            }

            return new PlayerMove(boardRow, boardColumn, cellRow, cellColumn, playerMarker);
        }
    }
}
