
namespace TicTacToe.Logic
{
    public class HumanVsComputer : GameType
    {
        private ComputerPlayer _computerPlayer;
        private HumanPlayer _humanPlayer;

        public HumanVsComputer()
        {
            _humanPlayer = new HumanPlayer();
            _computerPlayer = new ComputerPlayer();
            CurrentPlayer = _computerPlayer;
        }

        protected override Player SwitchPlayer()
        {
            if (_humanPlayer.GetType().Equals(CurrentPlayer.GetType()))
            {
                return _computerPlayer;
            }
            else
            {
                return _humanPlayer;
            }
        }
    }
}
