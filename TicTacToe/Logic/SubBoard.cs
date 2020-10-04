namespace TicTacToe.Logic
{
    public class SubBoard : Board<AtomicCell>, IBoardCell
    {
        public SubBoard(int row, int col) : base((cellRow, cellCol) => new AtomicCell(cellRow, cellCol))
        {
            Row = row;
            Column = col;
        }

        public int Row { get; }
        public int Column { get; }
        PlayerMarker? IBoardCell.OwningPlayer => Winner;
    }
}
