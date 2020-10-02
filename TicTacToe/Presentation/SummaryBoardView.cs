using System.Drawing;
using TicTacToe.Logic;

namespace TicTacToe.Presentation
{
    public class SummaryBoardView : BoardView
    {
        private SummaryBoard _summaryBoard;
        public SummaryBoardView(MainBoard board, Point topLeft) : base(board)
        {
            _summaryBoard = (SummaryBoard)board;
            _originLocation = new Point(Game.BoardDimensions * Game.BoardDimensions * SpaceBetweenPieces + SpaceBetweenPieces, Game.BoardDimensions * SpaceBetweenPieces);

            // TODO: Register for events from main board about winners/ties
        }
    }
}
