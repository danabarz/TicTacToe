using System;

namespace TicTacToe
{
    public abstract class GameType
    {
        public event EventHandler<PlayerEventArgs> MangedPlayerTurns;
        public Player CurrentPlayer { get; protected set; }

        public void TurnManager()
        {
            CurrentPlayer = SwitchPlayer();
            OnPlayerPlayed(CurrentPlayer);
        }

        private void OnPlayerPlayed(Player player)
        {
            MangedPlayerTurns?.Invoke(this, new PlayerEventArgs { Player = player});
        }
        protected abstract Player SwitchPlayer();
    }
}
