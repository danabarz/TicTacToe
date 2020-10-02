using System;

namespace TicTacToe.Logic
{
    public class MainBoard : Board<SubBoard>
    {
        public MainBoard() : base((cellRow, cellCol) => new SubBoard(cellRow, cellCol))
        {
        }

        public override void SetWinnerIfNeeded()
        {
            throw new NotImplementedException();
        }
    }
}
