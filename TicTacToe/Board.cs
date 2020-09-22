using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;

namespace TicTacToe
{
    public abstract class Board
    {
        public const int BoardDimensions = 3;

        public Board()
        {
            GameBoard = new PlayerMarker?[BoardDimensions, BoardDimensions];
            Winner = null;
        }

        public event EventHandler<BoardEventArgs> UpdatedBoardPieces;
        public event EventHandler<PlayerMoveEventArgs> UpdatedSubBoardWinner;

        public PlayerMarker?[,] GameBoard { get; set; }
        public PlayerMarker? Winner { get; set; }
        public Point BoardOriginLocation { get; set; }
        protected int BoardIndex { get; set; }

        protected void OnBoardUpdated()
        {
            UpdatedBoardPieces?.Invoke(this, new BoardEventArgs { GameBoard = this.GameBoard, BoardOriginLocation = this.BoardOriginLocation });
        }

        protected void OnSubBoardWinnerUpdated(PlayerMove playerMove)
        {
            UpdatedSubBoardWinner?.Invoke(this, new PlayerMoveEventArgs { PlayerMove = playerMove });
        }

                public int GetRow(int index)
        {
            return index / BoardDimensions;
        }
        public int GetColumn(int index)
        {
            return index % BoardDimensions;
        }

        public void UpdateBoard(PlayerMove playerMove)
        {
            if (GameBoard[playerMove.Row, playerMove.Column] == null)
            {
                GameBoard[playerMove.Row, playerMove.Column] = playerMove.Marker;
                OnBoardUpdated();
            }
        }

        //todo: not good
        public void HaveWinner(Board gameBoard)
        {
            PlayerMarker? winnerMarker = CheckIfGameOver();
            if (winnerMarker != null)
            {
                Winner = winnerMarker;
                int outerRow = GetRow(this.BoardIndex);
                int outerCol = GetColumn(this.BoardIndex);
                var summaryBoardMove = new PlayerMove(gameBoard, outerRow, outerCol, winnerMarker);
                OnSubBoardWinnerUpdated(summaryBoardMove);
            }
        }

        public List<Tuple<int, int>> FindOpenMoves()
        {
            var emptyLocationsOnBoard = new List<Tuple<int, int>>();
            for (int i = 0; i < BoardDimensions; i++)
            {
                for (int j = 0; j < BoardDimensions; j++)
                {
                    if (GameBoard[i, j] == null)
                    {
                        emptyLocationsOnBoard.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            return emptyLocationsOnBoard;
        }

        public PlayerMarker? HorizontalWinForGame()
        {
            for (int i = 0; i < BoardDimensions; i++)
            {
                int count = 0;
                PlayerMarker? playerMarkerHorizontal = GameBoard[i, i];
                for (int j = 0; j < BoardDimensions; j++)
                {
                    if (GameBoard[i, j] == playerMarkerHorizontal)
                    {
                        count++;
                    }

                    if (count == BoardDimensions && playerMarkerHorizontal != null && playerMarkerHorizontal != PlayerMarker.Tie)
                    {
                        return playerMarkerHorizontal;
                    }
                }
            }
            return null;
        }

        public PlayerMarker? VerticalWinForGame()
        {
            for (int j = 0; j < BoardDimensions; j++)
            {
                int count = 0;
                PlayerMarker? playerMarkerVertical = GameBoard[j, j];
                for (int i = 0; i < BoardDimensions; i++)
                {
                    if (GameBoard[i, j] == playerMarkerVertical)
                    {
                        count++;
                    }

                    if (count == BoardDimensions && playerMarkerVertical != null && playerMarkerVertical != PlayerMarker.Tie)
                    {
                        return playerMarkerVertical;
                    }
                }
            }
            return null;
        }

        public PlayerMarker? DiagonalWinForGame()
        {
            int count = 0;
            int row = 0;
            int column = 2;

            PlayerMarker? playerMarkerDiagonalOne = GameBoard[row, row];

            for (int j = 0; j < BoardDimensions; j++)
            {
                if (GameBoard[j, j] == playerMarkerDiagonalOne)
                {
                    count++;
                }
            }

            if (count == BoardDimensions && playerMarkerDiagonalOne != null && playerMarkerDiagonalOne != PlayerMarker.Tie)
            {
                return playerMarkerDiagonalOne;
            }

            count = 0;
            PlayerMarker? playerMarkerDiagonalTwo = GameBoard[row, column];

            while (row < BoardDimensions && column >= 0)
            {
                if (GameBoard[row, column] == playerMarkerDiagonalTwo)
                {
                    count++;
                }
                row++;
                column--;
            }

            if (count == BoardDimensions && playerMarkerDiagonalTwo != null && playerMarkerDiagonalTwo != PlayerMarker.Tie)
            {
                return playerMarkerDiagonalTwo;
            }
            return null;
        }

        public PlayerMarker? TieWinForGame()
        {
            for (int i = 0; i < BoardDimensions; i++)
            {
                for (int j = 0; j < BoardDimensions; j++)
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
