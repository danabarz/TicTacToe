
namespace TicTacToe.Logic
{
    public class PlayerMove
    {
        public PlayerMove(int subBoardRow, int subBoardCol, int atomicCellRow, int atomicCellCol, PlayerMarker playerMarker)
        {
            SubBoardRow = subBoardRow;
            SubBoardCol = subBoardCol;
            AtomicCellRow = atomicCellRow;
            AtomicCellCol = atomicCellCol;
            PlayerMarker = playerMarker;
        }

        public int SubBoardRow { get; }
        public int SubBoardCol { get; }
        public int AtomicCellRow { get; }
        public int AtomicCellCol { get; }
        public PlayerMarker PlayerMarker { get; }
    }
}
