using System;

namespace TicTacToe
{
    public class GameEventArgs : EventArgs
    {
        public SubBoard[,] SubBoards { get; set; }
    }
}
