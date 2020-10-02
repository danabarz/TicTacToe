
namespace TicTacToe
{
    public class HumanVsHuman : GameType
    {
        private HumanPlayer _humanPlayer1;
        private HumanPlayer _humanPlayer2;

        public HumanVsHuman(string player1Name, string player2Name)
        {
            _humanPlayer1 = new HumanPlayer(PlayerMarker.X, player1Name);
            _humanPlayer2 = new HumanPlayer(PlayerMarker.O, player2Name);
            CurrentPlayer = _humanPlayer2;
        }

        protected override Player SwitchPlayer()
        {
            return (CurrentPlayer.IdPlayer == _humanPlayer1.IdPlayer) ? _humanPlayer2 : _humanPlayer1;
        }
    }
}
