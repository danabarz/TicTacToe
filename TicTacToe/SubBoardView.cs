using System.Drawing;
using System;

namespace TicTacToe
{
    public class SubBoardView : BoardView
    {
        private SubBoard _subBoard;
        public SubBoardView(Board board) : base(board)
        {
            _subBoard = (SubBoard)board;
            _originLocation = new Point(_subBoard.BoardColumn * _subBoard.dimensions * spaceBetweenPieces, _subBoard.BoardRow * _subBoard.dimensions * spaceBetweenPieces);
        }
    }
}
