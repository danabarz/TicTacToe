namespace TicTacToe.Logic
{
    public class SubBoard : Board<AtomicCell>, IBoardCell
    {
        public SubBoard(int row, int col) : base((boardCell) => new AtomicCell(boardCell.Row, boardCell.Column))
        {
            Row = row;
            Column = col;
        }

        public int Row { get; }
        public int Column { get; }
        PlayerMarker? IBoardCell.OwningPlayer => Winner;
    }
}
