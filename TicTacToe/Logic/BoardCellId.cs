namespace TicTacToe.Logic
{
    public struct BoardCellId
    {
        public BoardCellId(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; private set; }
        public int Column { get; private set; }

        public void SetRowAndColumn(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
