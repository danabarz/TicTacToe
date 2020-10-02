using System;

namespace TicTacToe.Logic
{
    public sealed class Game // Sealed because we want to keep a private constructor
    {
        public const int BoardDimensions = 3;

        public static Game CreateHumanVsHuman(string player1Name, string player2Name)
            => new Game(new HumanPlayer(PlayerMarker.X, player1Name), new HumanPlayer(PlayerMarker.O, player2Name));

        public static Game CreateHumanVsComputer()
            => new Game(new HumanPlayer(), new ComputerPlayer());

        private readonly Player[] _players;
        private int _currentPlayerIndex;

        private Game(params Player[] players)
        {
            MainBoard = new MainBoard();
            _players = players;
            _currentPlayerIndex = 0;
        }

        public MainBoard MainBoard { get; }

        public event EventHandler<PlayerEventArgs>? HumanPlayerMoveRequested;

        public void Start()
        {
            RunUntilHumanPlayerMoveRequested();
        }

        public PlayerMarker? Winner => MainBoard.Winner;

        public void AcceptHumanPlayerMoveAndProceed(PlayerMove move)
        {
            // Validate input - what if cell already taken? Need to re-ask for input by re-invoking the HumanPlayerMoveRequested event
            if (CurrentPlayer.IdPlayer == move.PlayerMarker)
            {
                AcceptMove(move);
            }

            RunUntilHumanPlayerMoveRequested();
        }

        private void RunUntilHumanPlayerMoveRequested()
        {
            while (CurrentPlayer is ComputerPlayer)
            {
                var move = CurrentPlayer.ChooseMove(this);
                AcceptMove(move);
            }

            HumanPlayerMoveRequested?.Invoke(this, new PlayerEventArgs(CurrentPlayer, 0));
        }

        private void AcceptMove(PlayerMove move)
        {
            MainBoard[move.SubBoardRow, move.SubBoardCol].UpdateBoard(move.AtomicCellRow, move.AtomicCellCol, move.PlayerMarker);
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Length;
        }

        private Player CurrentPlayer => _players[_currentPlayerIndex];
    }
}
