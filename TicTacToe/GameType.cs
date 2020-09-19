using System;

namespace TicTacToe
{
    abstract class GameType
    {
        public GameType()
        {
            this.HumanPlayer1 = new HumanPlayer();
        }

        public event EventHandler<TicTacToeEventArgs> MangePlayerTurns;

        public Player CurrentPlayer { get; set; }
        public HumanPlayer HumanPlayer1 { get; set; }

        public abstract Player SwitchPlayer();

        public void TurnManager(int numberOfTimes)
        {
            this.CurrentPlayer = SwitchPlayer();
            OnPlayerPlayed(this.CurrentPlayer, numberOfTimes);
        }

        protected virtual void OnPlayerPlayed(Player player, int numberOfTimes)
        {
            if (MangePlayerTurns != null)
            {
                MangePlayerTurns(this, new TicTacToeEventArgs() { Player = player, NumberOfTimes = numberOfTimes });
            }
        }

    }
}
