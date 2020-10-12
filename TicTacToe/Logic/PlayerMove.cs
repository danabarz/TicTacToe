namespace TicTacToe.Logic
{
    public class PlayerMove
    {
        public PlayerMove(BoardCellId subBoardCellId, BoardCellId atomicCellId, PlayerMarker playerMarker)
        {
            SubBoardCellId = subBoardCellId;
            AtomicCellId = atomicCellId;
            PlayerMarker = playerMarker;
        }

        public BoardCellId SubBoardCellId { get; }
        public BoardCellId AtomicCellId { get; }
        public PlayerMarker PlayerMarker { get; }
    }
}
