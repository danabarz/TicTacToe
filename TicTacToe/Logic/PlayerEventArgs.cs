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

        public Player Player { get; }
        public int AttemptsCount { get; }
    }
}
