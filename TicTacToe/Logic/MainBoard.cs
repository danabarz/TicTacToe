namespace TicTacToe.Logic
{
    public class MainBoard : Board<SubBoard>
    {
        public MainBoard() : base((boardCell) => new SubBoard(boardCell.Row, boardCell.Column)) { }
    }
}
