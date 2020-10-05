namespace TicTacToe.Logic
{
    public class HumanPlayer : Player
    {
        public HumanPlayer()
        {
            _idPlayer = PlayerMarker.X;
            _playerName = "Human";
        }

        public HumanPlayer(PlayerMarker playerMarker, string playerName)
        {
            _idPlayer = playerMarker;
            _playerName = playerName;
        }
    }
}
