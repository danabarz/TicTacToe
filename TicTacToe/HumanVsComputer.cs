
namespace TicTacToe
{
    public class HumanVsComputer : GameType
    {
        public HumanVsComputer()
        {
            HumanPlayer = new HumanPlayer();
            ComputerPlayer = new ComputerPlayer();
            CurrentPlayer = ComputerPlayer;
        }
        private ComputerPlayer ComputerPlayer { get; }
        private HumanPlayer HumanPlayer { get; }


        public override Player SwitchPlayer()
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
