using System;

namespace TicTacToe
{
    public abstract class GameType
    {
        public event EventHandler<PlayerEventArgs> MangedPlayerTurns;
        public Player CurrentPlayer { get; protected set; }

        public void TurnManager(int numberOfTimes)
        {
            CurrentPlayer = SwitchPlayer();
            OnPlayerPlayed(CurrentPlayer, numberOfTimes);
        }

        private void OnPlayerPlayed(Player player, int numberOfTimes)
        {
            MangedPlayerTurns?.Invoke(this, new PlayerEventArgs { Player = player, Count = numberOfTimes });
        }
        protected abstract Player SwitchPlayer();
    }
}
