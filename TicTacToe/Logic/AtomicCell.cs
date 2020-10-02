namespace TicTacToe.Logic
{
    public class AtomicCell : IBoardCell
    {
        public AtomicCell(int row, int col)
        {
            OwningPlayer = null;
            Row = row;
            Column = col;
        }

        public int Row { get; }
        public int Column { get; }

        public PlayerMarker? OwningPlayer { get; private set; }

        public bool SetOwningPlayerIfAvailable(PlayerMarker owningPlayer)
        {
            bool wasAvailable = OwningPlayer == null;

            if (wasAvailable)
            {
                OwningPlayer = owningPlayer;
            }

            return wasAvailable;
        }
    }
}
