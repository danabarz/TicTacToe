using System;

namespace TicTacToe
{
    public class BoardEventArgs : EventArgs
    {
        public Board GameBoard { get; set; }
    }
}
