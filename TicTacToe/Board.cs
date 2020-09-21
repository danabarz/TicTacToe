using System;
using System.Collections.Generic;
using System.Data.Common;

namespace TicTacToe
{
    abstract class Board
    {
        public const int boardDimensions = 3;

        public Board()
        {
            this.GameBoard = new PlayerMarker?[boardDimensions, boardDimensions];
            this.Owner = null;
        }

        public delegate int FindLocation(int index);

        public FindLocation findRow = index => index / boardDimensions;
        public FindLocation findColumn = index => index % boardDimensions;

        public event EventHandler<TicTacToeEventArgs> UpdateBoardPieces;
        public event EventHandler<TicTacToeEventArgs> SubBoardHaveOwner;

        public PlayerMarker?[,] GameBoard { get; set; }
        public PlayerMarker? Owner { get; set; }
        public int BoardOriginLeft { get; set; }
        public int BoardOriginTop { get; set; }
        public int BoardIndex { get; set; }
        public int Rows { get; set; } = 3;
        public int Columns { get; set; } = 3;

        protected virtual void OnBoardUpdated()
        {
            if (UpdateBoardPieces != null)
            {
                UpdateBoardPieces(this, new TicTacToeEventArgs() { GameBoard = this.GameBoard, BoardOriginLeft = this.BoardOriginLeft, BoardOriginTop = this.BoardOriginTop });
            }
        }

        protected virtual void OnSubBoardHaveOwner(PlayerMove playerMove)
        {
            if (SubBoardHaveOwner != null)
            {
                SubBoardHaveOwner(this, new TicTacToeEventArgs() { PlayerMove = playerMove });
            }
        }

        public void UpdateBoard(PlayerMove playerMove)
        {
            if (this.GameBoard[playerMove.Row, playerMove.Column] == null)
            {
                this.GameBoard[playerMove.Row, playerMove.Column] = playerMove.Piece;
            }
            OnBoardUpdated();
        }

        public void HaveOwner(Board gameBoard)
        {
            PlayerMarker? TheWinnerMarker = CheckIfGameOver();
            if (TheWinnerMarker != null)
            {
                this.Owner = (PlayerMarker)TheWinnerMarker;
                int outerRow = findRow(this.BoardIndex);
                int outerCol = findColumn(this.BoardIndex);
                PlayerMove summaryBoardMove = new PlayerMove(gameBoard, outerRow, outerCol, (PlayerMarker)TheWinnerMarker);
                OnSubBoardHaveOwner(summaryBoardMove);
            }
        }

        public List<Tuple<int, int>> FindOpenMoves()
        {
            var emptyLocationsOnBoard = new List<Tuple<int, int>>();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (this.GameBoard[i, j] == null)
                    {
                        emptyLocationsOnBoard.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            return emptyLocationsOnBoard;
        }

        public PlayerMarker? HorizontalWinForGame()
        {
            for (int i = 0; i < Rows; i++)
            {
                int count = 0;
                PlayerMarker? playerMarkerHorizontal = this.GameBoard[i, i];
                for (int j = 0; j < Columns; j++)
                {
                    if (this.GameBoard[i, j] == playerMarkerHorizontal)
                    {
                        count++;
                    }

                    if (count == boardDimensions && playerMarkerHorizontal != null && playerMarkerHorizontal != PlayerMarker.Tie)
                    {
                        return playerMarkerHorizontal;
                    }
                }
            }
            return null;
        }

        public PlayerMarker? VerticalWinForGame()
        {
            for (int j = 0; j < Columns; j++)
            {
                int count = 0;
                PlayerMarker? playerMarkerVertical = this.GameBoard[j, j];
                for (int i = 0; i < Rows; i++)
                {
                    if (this.GameBoard[i, j] == playerMarkerVertical)
                    {
                        count++;
                    }

                    if (count == boardDimensions && playerMarkerVertical != null && playerMarkerVertical != PlayerMarker.Tie)
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

            PlayerMarker? playerMarkerDiagonalOne = this.GameBoard[row, row];

            for (int j = 0; j < Rows; j++)
            {
                if (this.GameBoard[j, j] == playerMarkerDiagonalOne)
                {
                    count++;
                }
            }

            if (count == boardDimensions && playerMarkerDiagonalOne != null && playerMarkerDiagonalOne != PlayerMarker.Tie)
            {
                return playerMarkerDiagonalOne;
            }

            count = 0;
            PlayerMarker? playerMarkerDiagonalTwo = this.GameBoard[row, column];

            while (row < Rows && column >= 0)
            {
                if (this.GameBoard[row, column] == playerMarkerDiagonalTwo)
                {
                    count++;
                }
                row++;
                column--;
            }

            if (count == boardDimensions && playerMarkerDiagonalTwo != null && playerMarkerDiagonalTwo != PlayerMarker.Tie)
            {
                return playerMarkerDiagonalTwo;
            }
            return null;
        }

        public PlayerMarker? TieWinForGame()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (this.GameBoard[i, j] == null)
                    {
                        return null;
                    }
                }
            }
            return PlayerMarker.Tie;
        }

        public PlayerMarker? CheckIfGameOver()
        {
            PlayerMarker? OwnerForBoard = (HorizontalWinForGame() ?? VerticalWinForGame()) ?? (DiagonalWinForGame() ?? TieWinForGame());
            return OwnerForBoard;
        }

        public PlayerMarker GetOponenentPiece(PlayerMarker playerMarker)
        {
            return (playerMarker == PlayerMarker.X) ? PlayerMarker.O : PlayerMarker.X;
        }
    }
}
