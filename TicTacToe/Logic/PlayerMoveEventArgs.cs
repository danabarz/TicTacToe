
namespace TicTacToe.Logic
{
    public class PlayerMoveEventArgs
    {
        public int BoardRow { get; set; }
        public int BoardColumn { get; set; }
        public PlayerMarker? PlayerMarker { get; set; }
    }
}
