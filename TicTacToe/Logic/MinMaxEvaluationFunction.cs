namespace TicTacToe.Logic
{
    public class MinMaxEvaluationFunction
    {
        public static int Evaluate(Board<AtomicCell> gameBoard, PlayerMarker playerMarker, PlayerMarker opponentMarker)
        {
            if (gameBoard.CheckIfGameOver() == playerMarker)
            {
                return MinMax.maxValue;
            }

            return (gameBoard.CheckIfGameOver() == opponentMarker) ? MinMax.minValue : 0;
        }
    }
}
