
namespace TicTacToe
{
    public class HumanVsHuman : GameType
    {
        public HumanVsHuman(string player1Name, string player2Name)
        {
            HumanPlayer1 = new HumanPlayer(PlayerMarker.X, player1Name);
            HumanPlayer2 = new HumanPlayer(PlayerMarker.O, player2Name);
            CurrentPlayer = HumanPlayer2;
        }

        private HumanPlayer HumanPlayer1 { get; set; }
        private HumanPlayer HumanPlayer2 { get; set; }

        public override Player SwitchPlayer()
        {
            return (CurrentPlayer.IdPlayer == HumanPlayer1.IdPlayer) ? HumanPlayer2 : HumanPlayer1;
        }
    }
}
