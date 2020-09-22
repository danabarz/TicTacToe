using System;

namespace TicTacToe
{
    public abstract class GameType
    {

        public event EventHandler<PlayerEventArgs> MangedPlayerTurns;

        public Player CurrentPlayer { get; set; }
        //public HumanPlayer HumanPlayer1 { get; set; }

        public abstract Player SwitchPlayer();

        public void TurnManager(int numberOfTimes)
        {
            CurrentPlayer = SwitchPlayer();
            OnPlayerPlayed(CurrentPlayer, numberOfTimes);
        }

        protected void OnPlayerPlayed(Player player, int numberOfTimes)
        {
            MangedPlayerTurns?.Invoke(this, new PlayerEventArgs { Player = player, CountTimes = numberOfTimes });
        }

    }
}
