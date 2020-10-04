using System;

namespace TicTacToe.Logic
{
    public class PlayerEventArgs : EventArgs
    {
        public PlayerEventArgs(Player player, int attemptsCount)
        {
            Player = player;
            AttemptsCount = attemptsCount;
        }
        public PlayerEventArgs(Player player)
        {
            Player = player;
        }

        public Player Player { get; }
        public int AttemptsCount { get; }
    }
}
