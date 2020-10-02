using System;

namespace TicTacToe.Logic
{
    public abstract class Player
    {
        public event EventHandler<IntEventArgs> PrintingHumanOutput;
        public event EventHandler<EventArgs> ClearedSpecificLine;

        public PlayerMarker IdPlayer { get; protected set; }
        public string PlayerName { get; protected set; }

        protected void OnHumanPlaying(int numberOfAttempts)
        {
            PrintingHumanOutput?.Invoke(this, new IntEventArgs { Count = numberOfAttempts });
        }

        protected void OnLocationEntered()
        {
            ClearedSpecificLine?.Invoke(this, EventArgs.Empty);
        }

        public abstract PlayerMove ChooseMove(Game game);
    }
}
