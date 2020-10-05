using System;

namespace TicTacToe.Logic
{
    public class MinMax
    {
        public const int minValue = -10;
        public const int maxValue = 10;
        private readonly EvaluationFunction evaluateFunction = new EvaluationFunction();

        private int Minmax(Board<AtomicCell> gameBoard, int depth, bool isMax, PlayerMarker playerMarker)
        {
            int value = evaluateFunction.Evaluate(gameBoard, playerMarker);
            int emptyLocationIndex = 0;

            if (value == maxValue)
            {
                return value - depth;
            }
            else if (value == minValue)
            {
                return value + depth;
            }
            else if (gameBoard.CheckIfGameOver() == PlayerMarker.Tie)
            {
                return 0;
            }
            else if (isMax)
            {
                return AddMarkerAndCheckBoardValue(minValue);
            }
            return AddMarkerAndCheckBoardValue(maxValue);


            int AddMarkerAndCheckBoardValue(int defultValue)
            {
                int bestValue = defultValue;
                var openLocations = gameBoard.FindOpenMoves();
                while (emptyLocationIndex < openLocations.Count)
                {
                    if (gameBoard[openLocations[emptyLocationIndex].Item1, openLocations[emptyLocationIndex].Item2].SetOwningPlayerIfAvailable(playerMarker))
                    {
                        if (defultValue == minValue)
                        {
                            bestValue = Math.Max(bestValue, Minmax(gameBoard, depth + 1, !isMax, gameBoard.GetOponenentPiece(playerMarker)));
                        }
                        else
                        {
                            bestValue = Math.Min(bestValue, Minmax(gameBoard, depth + 1, !isMax, gameBoard.GetOponenentPiece(playerMarker)));
                        }

                        gameBoard[openLocations[emptyLocationIndex].Item1, openLocations[emptyLocationIndex].Item2].SetOwningPlayerToNull();
                        emptyLocationIndex++;
                    }
                }

                return bestValue;
            }
        }

        public PlayerMove FindBestMove(MainBoard mainBoard, PlayerMarker playerMarker)
        {
            int bestValue = minValue;
            int boardRow = minValue;
            int boardColumn = minValue;
            int cellRow = minValue;
            int cellColumn = minValue;
            int emptyLocationIndex = 0;

            for (int i = 0; i < Game.BoardDimensions; i++)
            {
                for (int j = 0; j < Game.BoardDimensions; j++)
                {
                    var openLocations = mainBoard[i, j].FindOpenMoves();
                    while (emptyLocationIndex < openLocations.Count && mainBoard[i, j].Winner == null)
                    {
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

                        emptyLocationIndex++;
                    }
                }
            }

            return new PlayerMove(boardRow, boardColumn, cellRow, cellColumn, playerMarker);
        }
    }
}
