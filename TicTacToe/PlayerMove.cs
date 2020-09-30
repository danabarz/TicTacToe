
namespace TicTacToe
{
    public class PlayerMove
    {
        public Board _board;
        public int _row;
        public int _column;
        public PlayerMarker? _marker;

        public PlayerMove(Board board, int row, int column, PlayerMarker? marker)
        {
            _board = board;
            _row = row;
            _column = column;
            _marker = marker;
        }
    }
}
