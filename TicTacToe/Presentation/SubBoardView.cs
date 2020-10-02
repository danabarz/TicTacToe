using System.Drawing;
using System;
using TicTacToe.Logic;

namespace TicTacToe.Presentation
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
