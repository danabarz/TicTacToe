using System;

namespace TicTacToe
{
    public class SubBoardEventArgs : EventArgs
    {
        public SubBoard[,] SubBoards { get; set; }
    }
}
