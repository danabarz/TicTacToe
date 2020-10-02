
using System;

namespace TicTacToe.Logic
{
    public class SubBoard : Board
    {
        public SubBoard(int row, int col) : base()
        {
            BoardRow = row;
            BoardColumn = col;
        }
        public int BoardRow { get; protected set; }
        public int BoardColumn { get; protected set; }

        public event EventHandler<PlayerMoveEventArgs> UpdatedSubBoardWinner;

        private void OnSubBoardWinnerUpdated(int boardRow, int boardColumn, PlayerMarker? winnerMarker)
        {
            UpdatedSubBoardWinner?.Invoke(this, new PlayerMoveEventArgs { BoardRow = boardRow, BoardColumn = boardColumn, PlayerMarker = winnerMarker });
        }

        public override void SetWinnerIfNeeded()
        {
            var winnerMarker = CheckIfGameOver();
            if (winnerMarker != null)
            {
                Winner = winnerMarker;
                OnSubBoardWinnerUpdated(BoardRow, BoardColumn, Winner);
            }
        }
    }
}
