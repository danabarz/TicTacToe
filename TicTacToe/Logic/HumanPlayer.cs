namespace TicTacToe.Logic
{
    public class HumanPlayer : Player
    {
        public HumanPlayer() : base("Human", PlayerMarker.X) { }

        public HumanPlayer(string playerName, PlayerMarker playerMarker) : base(playerName, playerMarker) { }
    }
}
