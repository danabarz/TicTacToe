using System;

namespace TicTacToe.Logic
{
    public class HumanPlayerMoveEventArgs : EventArgs
    {
        public HumanPlayerMoveEventArgs(Player player, int attemptsCount)
        {
            Player = player;
            AttemptsCount = attemptsCount;
        }
        public HumanPlayerMoveEventArgs(Player player)
        {
            Player = player;
        }

        public Player Player { get; }
        public int AttemptsCount { get; }
    }
}
