using System;

namespace TicTacToe
{
    public class PlayerEventArgs : EventArgs
    {
        public Player Player { get; set; }
        public int Count { get; set; }
    }
}
