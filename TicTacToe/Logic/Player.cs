namespace TicTacToe.Logic
{
    public abstract class Player
    {
        protected Player(string playerName, PlayerMarker marker)
        {
            PlayerName = playerName;
            Marker = marker;
        }

        public string? PlayerName { get; }
        public PlayerMarker Marker { get; }
    }
}
