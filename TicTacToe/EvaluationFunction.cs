using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class EvaluationFunction
    {
        public int Evaluate(Board gameBoard, PlayerMarker playerMarker)
        {
            if (gameBoard.CheckIfGameOver() == playerMarker)
            {
                return int.MaxValue;
            }
            else if (gameBoard.CheckIfGameOver() == gameBoard.GetOponenentPiece(playerMarker))
            {
                return int.MinValue;
            }
            return 0;
        }
    }
}
