using System;

namespace TicTacToe.Logic
{
    public class PlayerEventArgs : EventArgs
    {
        public Player Player { get; set; }
    }
}
