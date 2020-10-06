namespace TicTacToe.Logic
{
    public class EvaluationFunction
    {
        public int Evaluate(Board<AtomicCell> gameBoard, PlayerMarker playerMarker)
        {
            if (gameBoard.CheckIfGameOver() == gameBoard.GetOponenentPiece(playerMarker))
            {
                return MinMax.maxValue;
            }
            else if (gameBoard.CheckIfGameOver() == playerMarker)
            {
                return MinMax.minValue;
            }
            return 0;
        }
    }
}
