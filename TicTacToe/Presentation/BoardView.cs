using System.Drawing;
using TicTacToe.Logic;

namespace TicTacToe.Presentation
{
    public abstract class BoardView
    {
        public const int SpaceBetweenPieces = 4;
        public Board _board;

        public BoardView(Board<AtomicCell> board)
        {
            _board = board;
        }

        public abstract Point TopLeft { get; }
    }
}

