using System;

namespace TicTacToe.Logic
{
    public class MinMax
    {
        public const int minValue = -100;
        public const int maxValue = 100;
        private readonly EvaluationFunction evaluateFunction = new EvaluationFunction();
    
        public PlayerMove FindBestMove(MainBoard mainBoard, PlayerMarker playerMarker)
        {
            int bestValue = minValue;
            int boardRow = minValue;
            int boardColumn = minValue;
            int cellRow = minValue;
            int cellColumn = minValue;
            var open = mainBoard.FindOpenMoves();

            while (open.Count > 0)
            {
                var first = open.Dequeue();
                var subBoard = mainBoard[first.Item1, first.Item2];
                var openCell = subBoard.FindOpenMoves();
                var firstCell = openCell.Dequeue();

                if (subBoard[firstCell.Item1, firstCell.Item2].SetOwningPlayerIfAvailable(playerMarker))
                {
                    int moveVal = Minmax(subBoard, 0, false, playerMarker);
                    subBoard[firstCell.Item1, firstCell.Item2].SetOwningPlayerToNull();

                    if (moveVal > bestValue)
                    {
                        boardRow = subBoard.Row;
                        boardColumn = subBoard.Column;
                        cellRow = firstCell.Item1;
                        cellColumn = firstCell.Item2;
                        bestValue = moveVal;
                    }
                }
            }

            return new PlayerMove(boardRow, boardColumn, cellRow, cellColumn, playerMarker);
        }

        private int Minmax(Board<AtomicCell> gameBoard, int depth, bool isMax, PlayerMarker playerMarker)
        {
            var opponent = gameBoard.GetOpponentPiece(playerMarker);
            int value = evaluateFunction.Evaluate(gameBoard, playerMarker);

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

            if (isMax)
            {
                return Math.Max(minValue, AddMarkerAndCheckBoardValue(playerMarker) ?? minValue);
            }

            return Math.Min(maxValue, AddMarkerAndCheckBoardValue(opponent) ?? maxValue);
            

            int? AddMarkerAndCheckBoardValue(PlayerMarker player)
            {
                var open = gameBoard.FindOpenMoves();

                while (open.Count > 0)
                {
                    var first = open.Dequeue();

                    if (gameBoard[first.Item1, first.Item2].SetOwningPlayerIfAvailable(player))
                    {
                        int moveVal = Minmax(gameBoard, depth + 1, !isMax, playerMarker);
                        gameBoard[first.Item1, first.Item2].SetOwningPlayerToNull();
                        return moveVal;
                    }
                }

                return null;
            }
        }
    }
}
