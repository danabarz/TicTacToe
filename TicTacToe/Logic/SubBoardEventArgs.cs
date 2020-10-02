using System;

namespace TicTacToe.Logic
{
    public class SubBoardEventArgs : EventArgs
    {
        public SubBoard[,] SubBoards { get; set; }
    }
}
