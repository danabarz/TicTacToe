using System;
using System.Collections.Generic;

namespace TicTacToe
{
    abstract class Board
    {
        protected const int boardDimensions = 3;

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

        protected virtual void OnBoardUpdated()
        {
            if (UpdateBoardPieces != null)
            {
                UpdateBoardPieces(this, new TicTacToeEventArgs() { GameBoard = this.GameBoard, BoardOriginLeft = this.BoardOriginLeft, BoardOriginTop = this.BoardOriginTop});
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
            for (int i = 0; i < boardDimensions; i++)
            {
                for (int j = 0; j < boardDimensions; j++)
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
            for (int i = 0; i < boardDimensions; i++)
            {
                int j = 0;
                if (this.GameBoard[i, j] == this.GameBoard[i ,++j] && this.GameBoard[i, j] == this.GameBoard[i, ++j] && this.GameBoard[i, j] != null && this.GameBoard[i, j] != PlayerMarker.Tie)
                {
                    return this.GameBoard[i, j];
                }
            }
            return null;
        }

        public PlayerMarker? VerticalWinForGame()
        {
            for (int j = 0; j < boardDimensions; j++)
            {
                int i = 0;
                if (this.GameBoard[i, j] == this.GameBoard[++i, j] && this.GameBoard[i, j] == this.GameBoard[++i, j] && this.GameBoard[i, j] != null && this.GameBoard[i, j] != PlayerMarker.Tie)
                {
                    return this.GameBoard[i, j];
                }
            }
            return null;
        }

        public PlayerMarker? DiagonalWinForGame()
        {
            int i = 0;
            int j = 1;
            if (this.GameBoard[i, i] == this.GameBoard[j, j++] && this.GameBoard[i, i] == this.GameBoard[j, j] && this.GameBoard[i, i] != null && this.GameBoard[i, i] != PlayerMarker.Tie)               
            { 
                return this.GameBoard[i, i];
            }
            else if (this.GameBoard[i++, j--] == this.GameBoard[i, j] && this.GameBoard[i++, j--] == this.GameBoard[i, j] && this.GameBoard[i ,j] != null && this.GameBoard[i, j] != PlayerMarker.Tie)
            {
                return this.GameBoard[i, j];
            }
            return null;
        }

        public PlayerMarker? TieWinForGame()
        {
            for (int i = 0; i < boardDimensions; i++)
            {
                for (int j = 0; j < boardDimensions; j++)
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
    }
}
