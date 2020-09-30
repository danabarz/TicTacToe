using System.Drawing;

namespace TicTacToe
{
     public class BoardView
     {
        public readonly int SpaceBetweenPieces = 4;
        public Board _board;
        public Point _originLocation;

        public BoardView(Board board)
        {
            _board = board;
            if (_board.Column == _board.Dimensions)
            {
                _originLocation = new Point(_board.Column * _board.Dimensions * SpaceBetweenPieces + SpaceBetweenPieces, _board.Row * _board.Dimensions * SpaceBetweenPieces);
            }
            else
            {
                _originLocation = new Point(_board.Column * _board.Dimensions * SpaceBetweenPieces, _board.Row * _board.Dimensions * SpaceBetweenPieces);
            }
        }
    }
}

