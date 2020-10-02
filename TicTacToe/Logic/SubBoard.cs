
using System;

namespace TicTacToe.Logic
{
    public class SubBoard : Board<AtomicCell>, IBoardCell
    {
        public SubBoard(int row, int col) : base((cellRow, cellCol) => new AtomicCell(cellRow, cellCol))
        {
            Row = row;
            Column = col;
        }

        public int Row { get; }
        public int Column { get; }

        public event EventHandler<PlayerMoveEventArgs> UpdatedSubBoardWinner;

        public void UpdateBoard(int cellRow, int cellColumn, PlayerMarker playerMarker)
        {
            if (GameBoard[cellRow, cellColumn].SetOwningPlayerIfAvailable(playerMarker))
            {
                OnBoardUpdated();

                var winnerMarker = CheckIfGameOver();
                if (winnerMarker != null)
                {
                    Winner = winnerMarker;
                    OnSubBoardWinnerUpdated(Row, Column, Winner);
                }
            }
        }

        private void OnSubBoardWinnerUpdated(int boardRow, int boardColumn, PlayerMarker? winnerMarker)
        {
            UpdatedSubBoardWinner?.Invoke(this, new PlayerMoveEventArgs { BoardRow = boardRow, BoardColumn = boardColumn, PlayerMarker = winnerMarker });
        }

        public override void SetWinnerIfNeeded()
        {
            
        }

        PlayerMarker? IBoardCell.OwningPlayer => Winner;
    }
}
