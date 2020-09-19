using System;
using System.Collections.Generic;

namespace TicTacToe
{
    abstract class Player
    {
        protected const int boardDimensions = 3;

        public event EventHandler<TicTacToeEventArgs> PrintHumanOutput;
        public event EventHandler<EventArgs> ClearSpecificLine;

        public PlayerMarker IdPlayer { get; set; }
        public string PlayerName { get; set; }

        protected virtual void OnHumanPlaying(int numberOfAttempts)
        {
            if (PrintHumanOutput != null)
            {
                PrintHumanOutput(this, new TicTacToeEventArgs() { NumberOfTimes = numberOfAttempts });
            }
        }

        protected virtual void OnLocationEntered()
        {
            if (ClearSpecificLine != null)
            {
                ClearSpecificLine(this, EventArgs.Empty);
            }
        }
        public abstract PlayerMove ChooseMove(List<SubBoard> subBoards);
    }
}
