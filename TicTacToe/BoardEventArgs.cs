using System;
using System.Collections.Generic;
using System.Drawing;

namespace TicTacToe
{
    public class BoardEventArgs : EventArgs
    {
        public PlayerMarker?[,] GameBoard { get; set; }
        public Point BoardOriginLocation { get; set; }
        public PlayerMarker? Owner { get; set; }
    }
}
