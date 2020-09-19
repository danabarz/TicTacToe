using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class PlayerMove
    {
        public PlayerMove(Board board, int row, int column, PlayerMarker piece)
        {
            this.Board = board;
            this.Row = row;
            this.Column = column;
            this.Piece = piece;
        }
        public Board Board { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public PlayerMarker Piece { get; set; }
    }
}
