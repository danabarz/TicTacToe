namespace TicTacToe.Logic
{
    public interface IBoardCell
    {
        int Row { get; }
        int Column { get; }
        PlayerMarker? OwningPlayer { get; }
    }
}
