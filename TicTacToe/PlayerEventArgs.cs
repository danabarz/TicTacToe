using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    public class PlayerEventArgs : EventArgs
    {
        public Player Player { get; set; }
        public int CountTimes { get; set; }
    }
}
