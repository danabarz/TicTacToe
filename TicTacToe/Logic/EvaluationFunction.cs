
namespace TicTacToe.Logic
{
    public class EvaluationFunction
    {
        public int Evaluate(Board<AtomicCell> gameBoard, PlayerMarker playerMarker)
        {
            if (gameBoard.CheckIfGameOver() == gameBoard.GetOponenentPiece(playerMarker))
            {
                return int.MaxValue;
            }
            else if (gameBoard.CheckIfGameOver() == playerMarker)
            {
                return int.MinValue;
            }
            return 0;
        }
    }
}
