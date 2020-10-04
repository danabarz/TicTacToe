namespace TicTacToe.Logic
{
    public class HumanPlayer : Player
    {
        public HumanPlayer()
        {
            IdPlayer = PlayerMarker.X;
            PlayerName = "Human";
        }

        public HumanPlayer(PlayerMarker playerMarker, string playerName)
        {
            IdPlayer = playerMarker;
            PlayerName = playerName;
        }
    }
}
