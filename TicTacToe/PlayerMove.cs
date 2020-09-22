using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    public class PlayerMove
    {
        public PlayerMove(Board board, int row, int column, PlayerMarker? marker)
        {
            Board = board;
            Row = row;
            Column = column;
            Marker = marker;
        }
        public Board Board { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public PlayerMarker? Marker { get; set; }
    }
}
