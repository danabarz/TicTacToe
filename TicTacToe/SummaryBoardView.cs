using System.Drawing;

namespace TicTacToe
{
    public class SummaryBoardView : BoardView
    {
        private SummaryBoard _summaryBoard;
        public SummaryBoardView(Board board) : base(board)
        {
            _summaryBoard = (SummaryBoard)board;
            _originLocation = new Point(_summaryBoard.dimensions * _summaryBoard.dimensions * spaceBetweenPieces + spaceBetweenPieces, _summaryBoard.dimensions * spaceBetweenPieces);
        }
    }
}
