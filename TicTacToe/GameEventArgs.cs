using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    public class GameEventArgs : EventArgs
    {
        public List<SubBoard> SubBoards { get; set; }
        public SummaryBoard SummaryBoard { get; set; }
    }
}
