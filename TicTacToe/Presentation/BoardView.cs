using System.Drawing;
using TicTacToe.Logic;

namespace TicTacToe.Presentation
{
    public abstract class BoardView
    {
        public int spaceBetweenPieces = 4;
        public Board _board;
        public Point _originLocation;
        public BoardView(Board board)
        {
            _board = board;
        }
    }
}

