
namespace TicTacToe
{
    class HumanVsComputer : GameType
    {
        public HumanVsComputer()
        {
            this.ComputerPlayer = new ComputerPlayer();
            this.CurrentPlayer = ComputerPlayer;
        }

        public ComputerPlayer ComputerPlayer { get; set; }

        public override Player SwitchPlayer()
        {
            if (this.HumanPlayer1.GetType().Equals(this.CurrentPlayer.GetType()))
            {
                return this.ComputerPlayer;
            }
            else
            {
                return this.HumanPlayer1;
            }
        }
    }
}
