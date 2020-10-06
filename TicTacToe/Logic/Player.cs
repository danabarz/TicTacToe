namespace TicTacToe.Logic
{
    public abstract class Player
    {
        protected string? _playerName;
        protected PlayerMarker _idPlayer;

        public string? PlayerName => _playerName;
        public PlayerMarker IdPlayer => _idPlayer;
    }
}
