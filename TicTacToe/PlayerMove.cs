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
        public Board Board { get; }
        public int Row { get; }
        public int Column { get; }
        public PlayerMarker? Marker { get; }
    }
}
