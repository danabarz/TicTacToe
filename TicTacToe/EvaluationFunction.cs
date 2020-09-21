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


    
            //int maxValue = EvaluatePiece(gameBoard, player.IdPlayer);
            //int minValue = EvaluatePiece(gameBoard, player.GetOponenentPiece());

            //return maxValue - minValue;
        }

        /*
        private int EvaluatePiece(Board gameBoard, PlayerMarker playerMarker)
        {
            return EvaluateRows(gameBoard, playerMarker) + EvaluateColums(gameBoard, playerMarker) + EvaluateDiagonals(gameBoard, playerMarker);
        }

        private int EvaluateRows(Board gameBoard, PlayerMarker playerMarker)
        {
            int score = 0;
            int count;
            for (int i = 0; i < gameBoard.Rows; i++)
            {
                count = 0;
                bool rowClean = true;
                for (int j = 0; j < gameBoard.Columns; j++)
                {
                    PlayerMarker? pieceMarker = gameBoard.GameBoard[i, j];

                    if (pieceMarker == playerMarker)
                    {
                        count++;
                    }
                    else if (pieceMarker != playerMarker && pieceMarker != null)
                    {
                        rowClean = false;
                        break;
                    }
                }

                if(rowClean)
                {
                    score += count;
                }
            }
            return score;
        }

        private int EvaluateColums(Board gameBoard, PlayerMarker playerMarker)
        {
            int score = 0;
            int count;
            for (int j = 0; j < gameBoard.Columns; j++)
            {
                count = 0;
                bool columnClean = true;
                for (int i = 0; i < gameBoard.Rows; i++)
                {
                    PlayerMarker? pieceMarker = gameBoard.GameBoard[i, j];

                    if (pieceMarker == playerMarker)
                    {
                        count++;
                    }
                    else if (pieceMarker != playerMarker && pieceMarker != null)
                    {
                        columnClean = false;
                        break;
                    }
                }
                if(columnClean)
                {
                    score += count;
                }
            }
            return score;
        }

        private int EvaluateDiagonals(Board gameBoard, PlayerMarker playerMarker)
        {
            int score = 0;
            int count = 0;
            bool diagonalClean = true;
            PlayerMarker? pieceMarker;

            for (int i = 0; i < gameBoard.Rows; i++)
            {
                pieceMarker = gameBoard.GameBoard[i, i];

                if (pieceMarker == playerMarker)
                {
                    count++;
                }
                else if (pieceMarker != playerMarker && pieceMarker != null)
                {
                    diagonalClean = false;
                    break;
                }
            }

            if (diagonalClean)
            {
                score += count;
            }

            int row = 0;
            int column = 2;
            count = 0;
            diagonalClean = true;

            while (row < gameBoard.Rows && column >= 0)
            {
                pieceMarker = gameBoard.GameBoard[row, column];
                if (pieceMarker == playerMarker)
                {
                    count++;
                }
                else if (pieceMarker != playerMarker && pieceMarker != null)
                {
                    diagonalClean = false;
                    break;
                }
                row++;
                column--;
            }

            if (diagonalClean)
            {
                score += count;
            }

            return score;
        }*/
    }
}
