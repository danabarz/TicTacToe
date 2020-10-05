namespace TicTacToe.Logic
{
    public abstract class Player
    {
        protected string? _playerName;
        protected PlayerMarker _idPlayer;

        public string? PlayerName => _playerName;
        public PlayerMarker IdPlayer => _idPlayer;

        public PlayerMove ChooseMove(MainBoard mainBoard)
        {
            var miniMax = new MinMax();
            return miniMax.FindBestMove(mainBoard, _idPlayer);
        }
    }
}
