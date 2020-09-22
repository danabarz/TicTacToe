using System;
using System.Collections.Generic;

namespace TicTacToe
{
    public abstract class Player
    {
        protected const int BoardDimensions = 3;

        public event EventHandler<IntEventArgs> PrintingHumanOutput;
        public event EventHandler<EventArgs> ClearedSpecificLine;

        public PlayerMarker IdPlayer { get; set; }
        public string PlayerName { get; set; }

        protected void OnHumanPlaying(int numberOfAttempts)
        {
            PrintingHumanOutput?.Invoke(this, new IntEventArgs { CountTimes = numberOfAttempts });
        }

        protected void OnLocationEntered()
        {
            ClearedSpecificLine?.Invoke(this, EventArgs.Empty);
        }

        public abstract PlayerMove ChooseMove(List<SubBoard> subBoards);
    }
}
