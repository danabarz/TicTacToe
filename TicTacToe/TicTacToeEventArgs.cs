using System;
using System.Collections.Generic;

namespace TicTacToe
{
    class TicTacToeEventArgs : EventArgs
    {
        public PlayerMarker?[,] GameBoard { get; set; }
        public PlayerMove PlayerMove { get; set; }
        public int BoardOriginLeft { get; set; }
        public int BoardOriginTop { get; set; }
        public PlayerMarker? Owner { get; set; }
        public Player Player { get; set; }
        public List<SubBoard> SubBoards { get; set; }
        public SummaryBoard SummaryBoard { get; set; }
        public int NumberOfTimes { get; set; }
    }
}
