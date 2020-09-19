
namespace TicTacToe
{
    class HumanVsHuman : GameType
    {
        public HumanVsHuman(string player1Name, string player2Name)
        {
            this.HumanPlayer1.PlayerName = player1Name;
            this.HumanPlayer2 = new HumanPlayer(PlayerMarker.O, player2Name);
            this.CurrentPlayer = HumanPlayer2;
        }

        public HumanPlayer HumanPlayer2 { get; set; }

        public override Player SwitchPlayer()
        {
            return (this.CurrentPlayer.IdPlayer == HumanPlayer1.IdPlayer) ? HumanPlayer2 : HumanPlayer1;
        }
    }
}
