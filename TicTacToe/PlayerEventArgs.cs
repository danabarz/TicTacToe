using System;

namespace TicTacToe
{
    public class PlayerEventArgs : EventArgs
    {
        public Player Player { get; set; }
    }
}
