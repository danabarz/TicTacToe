
namespace TicTacToe.Logic
{
    public class PlayerMove
    {
        public SubBoard _subBoard;
        public int _row;
        public int _column;
        public PlayerMarker? _marker;

        public PlayerMove(SubBoard subBoard, int row, int column, PlayerMarker? marker)
        {
            _subBoard = subBoard;
            _row = row;
            _column = column;
            _marker = marker;
        }
    }
}
