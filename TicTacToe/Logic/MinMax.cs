using System;

namespace TicTacToe.Logic
{
    public class MinMax
    {
        public const int minValue = -100;
        public const int maxValue = 100;
        public MinMax(PlayerMarker playerMarker)
        {
            PlayerMarker = playerMarker;
            OpponentPlayerMarker = Game.GetOpponentMarker(PlayerMarker);
        }

        private PlayerMarker PlayerMarker { get; }
        private PlayerMarker OpponentPlayerMarker { get; }

        public PlayerMove FindBestMove(MainBoard mainBoard)
        {
            int bestValue = minValue;
            int alpha = minValue;
            int beta = maxValue;
            var subBoardId = new BoardCellId(minValue, minValue);
            var cellId = new BoardCellId(minValue, minValue);
            var openSubBoards = mainBoard.FindOpenMoves();

            while (openSubBoards.Count > 0)
            {
                var firstOpenSubBoard = openSubBoards.Dequeue();
                var subBoard = mainBoard[firstOpenSubBoard.Row, firstOpenSubBoard.Column];
                var openCells = subBoard.FindOpenMoves();

                while (openCells.Count > 0)
                {
                    var firstOpenCell = openCells.Dequeue();

                    if (subBoard[firstOpenCell.Row, firstOpenCell.Column].SetOwningPlayerIfAvailable(PlayerMarker))
                    {
                        int moveVal = Minmax(subBoard, 0, false, alpha, beta);

                        if (moveVal > bestValue)
                        {
                            subBoardId.SetRowAndColumn(subBoard.Row, subBoard.Column);
                            cellId.SetRowAndColumn(firstOpenCell.Row, firstOpenCell.Column);
                            bestValue = moveVal;
                        }

                        alpha = Math.Max(alpha, bestValue);
                    }

                    subBoard[firstOpenCell.Row, firstOpenCell.Column].SetNoOwningPlayer();

                    if (beta <= alpha)
                    {
                        break;
                    }
                }
            }

            return new PlayerMove(subBoardId, cellId, PlayerMarker);
        }

        private int Minmax(Board<AtomicCell> gameBoard, int depth, bool isMax, int alpha, int beta)
        {
            int result = MinMaxEvaluationFunction.Evaluate(gameBoard, PlayerMarker, OpponentPlayerMarker);

            if (result == maxValue)
            {
                return result - depth;
            }

            else if (result == minValue)
            {
                return result + depth;
            }

            else if (gameBoard.CheckIfGameOver() == PlayerMarker.Tie)
            {
                return 0;
            }

            return (isMax) ? AddMarkerAndCheckBoardValue(PlayerMarker, minValue) : AddMarkerAndCheckBoardValue(OpponentPlayerMarker, maxValue);          

            int AddMarkerAndCheckBoardValue(PlayerMarker marker, int bestVal)
            {
                var openCells = gameBoard.FindOpenMoves();
                int best = bestVal;

                while (openCells.Count > 0)
                {
                    var firstOpenCell = openCells.Dequeue();

                    if (gameBoard[firstOpenCell.Row, firstOpenCell.Column].SetOwningPlayerIfAvailable(marker))
                    {
                        if (marker == PlayerMarker)
                        {
                            best = Math.Max(best, Minmax(gameBoard, depth + 1, !isMax, alpha, beta));
                            alpha = Math.Max(alpha, best);
                        }

                        else
                        {
                            best = Math.Min(best, Minmax(gameBoard, depth + 1, !isMax, alpha, beta));
                            beta = Math.Min(beta, best);

                        }
                    }

                    gameBoard[firstOpenCell.Row, firstOpenCell.Column].SetNoOwningPlayer();

                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                return best;
            }
        }
    }
}
