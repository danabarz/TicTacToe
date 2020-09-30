using System;
using System.Collections.Generic;

namespace TicTacToe
{
    public abstract class Board
    {
        public readonly int Dimensions = 3;
        public Board()
        {
            GameBoard = new PlayerMarker?[Dimensions, Dimensions];
            Winner = null;
        }

        public event EventHandler<BoardEventArgs> UpdatedBoardPieces;
        public event EventHandler<PlayerMoveEventArgs> UpdatedSubBoardWinner;

        public PlayerMarker?[,] GameBoard { get; private set; }
        public PlayerMarker? Winner { get; private set; }
        public int Row { get; protected set; }
        public int Column { get; protected set; }

        private void OnBoardUpdated(Board board)
        {
            UpdatedBoardPieces?.Invoke(this, new BoardEventArgs { GameBoard = board});
        }

        private void OnSubBoardWinnerUpdated(PlayerMove playerMove)
        {
            UpdatedSubBoardWinner?.Invoke(this, new PlayerMoveEventArgs { PlayerMove = playerMove });
        }

        public void UpdateBoard(PlayerMove playerMove)
        {
            if (GameBoard[playerMove._row, playerMove._column] == null)
            {
                GameBoard[playerMove._row, playerMove._column] = playerMove._marker;
                OnBoardUpdated(playerMove._board);
            }
        }

        public void HaveWinner(Game game)
        {
            PlayerMarker? winnerMarker = CheckIfGameOver();
            if (winnerMarker != null)
            {
                Winner = winnerMarker;
                if (Column != Dimensions)
                {
                    var summaryBoardMove = new PlayerMove(game._summaryBoard, Row, Column, Winner);
                    OnSubBoardWinnerUpdated(summaryBoardMove);
                }
            }
        }

        public List<Tuple<int, int>> FindOpenMoves()
        {
            var emptyLocationsOnBoard = new List<Tuple<int, int>>();
            for (int i = 0; i < Dimensions; i++)
            {
                for (int j = 0; j < Dimensions; j++)
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
            for (int i = 0; i < Dimensions; i++)
            {
                int count = 0;
                PlayerMarker? playerMarkerHorizontal = GameBoard[i, i];
                for (int j = 0; j < Dimensions; j++)
                {
                    if (GameBoard[i, j] == playerMarkerHorizontal)
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
                PlayerMarker? playerMarkerVertical = GameBoard[j, j];
                for (int i = 0; i < Dimensions; i++)
                {
                    if (GameBoard[i, j] == playerMarkerVertical)
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
            PlayerMarker? playerMarkerDiagonalOne = GameBoard[row, row];

            for (int j = 0; j < Dimensions; j++)
            {
                if (GameBoard[j, j] == playerMarkerDiagonalOne)
                {
                    count++;
                }
            }

            if (count == Dimensions && playerMarkerDiagonalOne != null && playerMarkerDiagonalOne != PlayerMarker.Tie)
            {
                return playerMarkerDiagonalOne;
            }

            count = 0;
            PlayerMarker? playerMarkerDiagonalTwo = GameBoard[row, column];

            while (row < Dimensions && column >= 0)
            {
                if (GameBoard[row, column] == playerMarkerDiagonalTwo)
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
            PlayerMarker? WinnerForBoard = (HorizontalWinForGame() ?? VerticalWinForGame()) ?? (DiagonalWinForGame() ?? TieWinForGame());
            return WinnerForBoard;
        }

        public PlayerMarker GetOponenentPiece(PlayerMarker playerMarker)
        {
            return (playerMarker == PlayerMarker.X) ? PlayerMarker.O : PlayerMarker.X;
        }
    }
}
