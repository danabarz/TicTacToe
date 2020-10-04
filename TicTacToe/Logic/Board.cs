using System;
using System.Collections.Generic;

namespace TicTacToe.Logic
{
    public abstract class Board<TCell>
        where TCell : class, IBoardCell
    {
        protected const int Dimensions = Game.BoardDimensions;

        protected Board(Func<int, int, TCell> cellFactory)
        {
            GameBoard = new TCell[Dimensions, Dimensions];
            Winner = null;

            for (int i = 0; i < Dimensions; i++)
            {
                for (int j = 0; j < Dimensions; j++)
                {
                    GameBoard[i, j] = cellFactory(i, j);
                }
            }
        }

        public event EventHandler<EventArgs>? UpdatedBoardPieces;

        protected TCell[,] GameBoard { get; }
        public PlayerMarker? Winner { get; protected set; }

        public TCell this[int row, int col] => GameBoard[row, col];

        public void SetWinner()
        {
            var winnerMarker = CheckIfGameOver();
            if (winnerMarker != null)
            {
                Winner = winnerMarker;
            }

            UpdatedBoardPieces?.Invoke(this, EventArgs.Empty);
        }

        public int GetRow(int index)
        {
            return index / Dimensions;
        }

        public int GetColumn(int index)
        {
            return index % Dimensions;
        }

        public List<Tuple<int, int>> FindOpenMoves()
        {
            var emptyLocationsOnBoard = new List<Tuple<int, int>>();
            for (int i = 0; i < Dimensions; i++)
            {
                for (int j = 0; j < Dimensions; j++)
                {
                    if (GameBoard[i, j].OwningPlayer == null)
                    {
                        emptyLocationsOnBoard.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            return emptyLocationsOnBoard;
        }

        public PlayerMarker GetOponenentPiece(PlayerMarker playerMarker)
        {
            return playerMarker == PlayerMarker.X ? PlayerMarker.O : PlayerMarker.X;
        }

        public PlayerMarker? CheckIfGameOver()
        {
            var winnerForBoard = (HorizontalWinForGame() ?? VerticalWinForGame()) ?? DiagonalWinForGame() ?? TieWinForGame();
            return winnerForBoard;
        }

        private PlayerMarker? HorizontalWinForGame()
        {
            for (int i = 0; i < Dimensions; i++)
            {
                int count = 0;
                var playerMarkerHorizontal = GameBoard[i, i].OwningPlayer;
                for (int j = 0; j < Dimensions; j++)
                {
                    if (GameBoard[i, j].OwningPlayer == playerMarkerHorizontal)
                    {
                        count++;
                    }
                    if (count == Dimensions && playerMarkerHorizontal != null && playerMarkerHorizontal != PlayerMarker.Tie)
                    {
                        return playerMarkerHorizontal;
                    }
                }
            }
            return null;
        }

        private PlayerMarker? VerticalWinForGame()
        {
            for (int j = 0; j < Dimensions; j++)
            {
                int count = 0;
                var playerMarkerVertical = GameBoard[j, j].OwningPlayer;
                for (int i = 0; i < Dimensions; i++)
                {
                    if (GameBoard[i, j].OwningPlayer == playerMarkerVertical)
                    {
                        count++;
                    }
                    if (count == Dimensions && playerMarkerVertical != null && playerMarkerVertical != PlayerMarker.Tie)
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
            var playerMarkerDiagonalOne = GameBoard[row, row].OwningPlayer;

            for (int j = 0; j < Dimensions; j++)
            {
                if (GameBoard[j, j].OwningPlayer == playerMarkerDiagonalOne)
                {
                    count++;
                }
            }

            if (count == Dimensions && playerMarkerDiagonalOne != null && playerMarkerDiagonalOne != PlayerMarker.Tie)
            {
                return playerMarkerDiagonalOne;
            }

            count = 0;
            var playerMarkerDiagonalTwo = GameBoard[row, column].OwningPlayer;

            while (row < Dimensions && column >= 0)
            {
                if (GameBoard[row, column].OwningPlayer == playerMarkerDiagonalTwo)
                {
                    count++;
                }
                row++;
                column--;
            }

            if (count == Dimensions && playerMarkerDiagonalTwo != null && playerMarkerDiagonalTwo != PlayerMarker.Tie)
            {
                return playerMarkerDiagonalTwo;
            }
            return null;
        }

        private PlayerMarker? TieWinForGame()
        {
            for (int i = 0; i < Dimensions; i++)
            {
                for (int j = 0; j < Dimensions; j++)
                {
                    if (GameBoard[i, j].OwningPlayer == null)
                    {
                        return null;
                    }
                }
            }
            return PlayerMarker.Tie;
        }
    }
}
