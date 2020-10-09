using System;

namespace TicTacToe.Logic
{
    public sealed class Game // Sealed because we want to keep a private constructor
    {
        public const int BoardDimensions = 3;
        private int attemptsCount = 0;

        private readonly Player[] _players;
        private int _currentPlayerIndex;

        public static Game CreateHumanVsHuman(string player1Name, string player2Name)
            => new Game(new HumanPlayer(PlayerMarker.X, player1Name), new HumanPlayer(PlayerMarker.O, player2Name));

        public static Game CreateHumanVsComputer()
            => new Game(new HumanPlayer(), new ComputerPlayer());

        private Game(params Player[] players)
        {
            MainBoard = new MainBoard();
            _players = players;
            _currentPlayerIndex = 0;
        }

        public MainBoard MainBoard { get; }
        public PlayerMarker? Winner => MainBoard.Winner;

        public event EventHandler<PlayerEventArgs>? HumanPlayerMoveRequested;
        public event EventHandler<PlayerEventArgs>? CurrentPlayerChanged;

        public void Start()
        {
            CurrentPlayerChanged?.Invoke(this, new PlayerEventArgs(CurrentPlayer));
            RunUntilHumanPlayerMoveRequested();
        }

        public void AcceptHumanPlayerMoveAndProceed(PlayerMove move)
        {
            if (CurrentPlayer.IdPlayer == move.PlayerMarker)
            {
                var emptyCells = MainBoard[move.SubBoardRow, move.SubBoardCol].FindOpenMoves();

                if (emptyCells.Contains(Tuple.Create(move.AtomicCellRow, move.AtomicCellCol)))
                {
                    AcceptMove(move);
                }
            }

            RunUntilHumanPlayerMoveRequested();
        }

        private void RunUntilHumanPlayerMoveRequested()
        {
            while (CurrentPlayer is ComputerPlayer computerPlayer)
            {
                var move = computerPlayer.ChooseMove(MainBoard);
                AcceptMove(move);
            }

            HumanPlayerMoveRequested?.Invoke(this, new PlayerEventArgs(CurrentPlayer, attemptsCount++));
        }

        private void AcceptMove(PlayerMove move)
        {
            if (MainBoard[move.SubBoardRow, move.SubBoardCol][move.AtomicCellRow, move.AtomicCellCol].SetOwningPlayerIfAvailable(move.PlayerMarker))
            {
                MainBoard[move.SubBoardRow, move.SubBoardCol].SetWinner();
                MainBoard.SetWinner();
                _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Length;
                attemptsCount = 0;
                CurrentPlayerChanged?.Invoke(this, new PlayerEventArgs(CurrentPlayer));
            }
        }

        private Player CurrentPlayer => _players[_currentPlayerIndex];
    }
}
