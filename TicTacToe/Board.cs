using System;
using System.Collections.Generic;

namespace TicTacToe
{
    public abstract class Board
    {
        public int dimensions = 3;
        public Board()
        {
            GameBoard = new PlayerMarker?[dimensions, dimensions];
            Winner = null;
        }

        public event EventHandler<EventArgs> UpdatedBoardPieces;

        public PlayerMarker?[,] GameBoard { get;}
        public PlayerMarker? Winner { get; protected set; }

        private void OnBoardUpdated()
        {
            UpdatedBoardPieces?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateBoard(int cellRow, int cellColumn, PlayerMarker? playerMarker)
        {
            if (GameBoard[cellRow, cellColumn] == null)
            {
                GameBoard[cellRow, cellColumn] = playerMarker;
                OnBoardUpdated();
            }
        }

        public abstract void SetWinnerIfNeeded();

        public List<Tuple<int, int>> FindOpenMoves()
        {
            var emptyLocationsOnBoard = new List<Tuple<int, int>>();
            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    if (GameBoard[i, j] == null)
                    {
                        emptyLocationsOnBoard.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            return emptyLocationsOnBoard;
        }

        private PlayerMarker? HorizontalWinForGame()
        {
            for (int i = 0; i < dimensions; i++)
            {
                int count = 0;
                var playerMarkerHorizontal = GameBoard[i, i];
                for (int j = 0; j < dimensions; j++)
                {
                    if (GameBoard[i, j] == playerMarkerHorizontal)
                    {
                        count++;
                    }
                    if (count == dimensions && playerMarkerHorizontal != null && playerMarkerHorizontal != PlayerMarker.Tie)
                    {
                        return playerMarkerHorizontal;
                    }
                }
            }
            return null;
        }

        private PlayerMarker? VerticalWinForGame()
        {
            for (int j = 0; j < dimensions; j++)
            {
                int count = 0;
                var playerMarkerVertical = GameBoard[j, j];
                for (int i = 0; i < dimensions; i++)
                {
                    if (GameBoard[i, j] == playerMarkerVertical)
                    {
                        count++;
                    }
                    if (count == dimensions && playerMarkerVertical != null && playerMarkerVertical != PlayerMarker.Tie)
                    {
                        return playerMarkerVertical;
                    }
                }
            }
            return null;
        }

        private PlayerMarker? DiagonalWinForGame()
        {
            int count = 0;
            int row = 0;
            int column = 2;
            var playerMarkerDiagonalOne = GameBoard[row, row];

            for (int j = 0; j < dimensions; j++)
            {
                if (GameBoard[j, j] == playerMarkerDiagonalOne)
                {
                    count++;
                }
            }

            if (count == dimensions && playerMarkerDiagonalOne != null && playerMarkerDiagonalOne != PlayerMarker.Tie)
            {
                return playerMarkerDiagonalOne;
            }

            count = 0;
            var playerMarkerDiagonalTwo = GameBoard[row, column];

            while (row < dimensions && column >= 0)
            {
                if (GameBoard[row, column] == playerMarkerDiagonalTwo)
                {
                    count++;
                }
                row++;
                column--;
            }

            if (count == dimensions && playerMarkerDiagonalTwo != null && playerMarkerDiagonalTwo != PlayerMarker.Tie)
            {
                return playerMarkerDiagonalTwo;
            }
            return null;
        }

        private PlayerMarker? TieWinForGame()
        {
            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    if (GameBoard[i, j] == null)
                    {
                        return null;
                    }
                }
            }
            return PlayerMarker.Tie;
        }

        public PlayerMarker? CheckIfGameOver()
        {
            var WinnerForBoard = (HorizontalWinForGame() ?? VerticalWinForGame()) ?? (DiagonalWinForGame() ?? TieWinForGame());
            return WinnerForBoard;
        }

        public PlayerMarker GetOponenentPiece(PlayerMarker playerMarker)
        {
            return (playerMarker == PlayerMarker.X) ? PlayerMarker.O : PlayerMarker.X;
        }
    }
}
