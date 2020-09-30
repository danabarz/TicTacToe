
namespace TicTacToe
{
    public class HumanVsHuman : GameType
    {
        private readonly HumanPlayer HumanPlayer1;
        private readonly HumanPlayer HumanPlayer2;

        public HumanVsHuman(string player1Name, string player2Name)
        {
            HumanPlayer1 = new HumanPlayer(PlayerMarker.X, player1Name);
            HumanPlayer2 = new HumanPlayer(PlayerMarker.O, player2Name);
            CurrentPlayer = HumanPlayer2;
        }

        protected override Player SwitchPlayer()
        {
            return (CurrentPlayer.IdPlayer == HumanPlayer1.IdPlayer) ? HumanPlayer2 : HumanPlayer1;
        }
    }
}
