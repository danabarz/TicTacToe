
namespace TicTacToe
{
    public class HumanVsComputer : GameType
    {
        private readonly ComputerPlayer ComputerPlayer;
        private readonly HumanPlayer HumanPlayer;

        public HumanVsComputer()
        {
            HumanPlayer = new HumanPlayer();
            ComputerPlayer = new ComputerPlayer();
            CurrentPlayer = ComputerPlayer;
        }

        protected override Player SwitchPlayer()
        {
            if (HumanPlayer.GetType().Equals(CurrentPlayer.GetType()))
            {
                return ComputerPlayer;
            }
            else
            {
                return HumanPlayer;
            }
        }
    }
}
