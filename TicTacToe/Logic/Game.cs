using System;

namespace TicTacToe.Logic
{
    public sealed class Game // Sealed because we want to keep a private constructor
    {
        public const int BoardDimensions = 3;
        private int _attemptsCount = 0;

        private readonly Player[] _players;
        private int _currentPlayerIndex;

        public static Game CreateHumanVsHuman(string player1Name, string player2Name)
            => new Game(new HumanPlayer(player1Name, PlayerMarker.X), new HumanPlayer(player2Name, PlayerMarker.O));

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

        public event EventHandler<HumanPlayerMoveEventArgs>? HumanPlayerMoveRequested;
        public event EventHandler<HumanPlayerMoveEventArgs>? CurrentPlayerChanged;

        public static PlayerMarker GetOpponentMarker(PlayerMarker playerMarker)
        {
            return playerMarker == PlayerMarker.X ? PlayerMarker.O : PlayerMarker.X;
        }

        public bool Start()
        {
            CurrentPlayerChanged?.Invoke(this, new HumanPlayerMoveEventArgs(CurrentPlayer));
            return RunUntilHumanPlayerMoveRequested();
        }

        public bool AcceptHumanPlayerMoveAndProceed(PlayerMove move)
        {
            if (CurrentPlayer.Marker == move.PlayerMarker)
            {
                var emptyCells = MainBoard[move.SubBoardCellId.Row, move.SubBoardCellId.Column].FindOpenMoves();

                if (emptyCells.Contains((move.AtomicCellId)))
                {
                    if (AcceptMove(move))
                    {
                        return true;
                    }
                }
            }

            return RunUntilHumanPlayerMoveRequested();
        }

        private bool RunUntilHumanPlayerMoveRequested()
        {
            while (CurrentPlayer is ComputerPlayer computerPlayer)
            {
                var move = computerPlayer.ChooseMove(MainBoard);
                if (AcceptMove(move))
                {
                    return true;
                }
            }

            HumanPlayerMoveRequested?.Invoke(this, new HumanPlayerMoveEventArgs(CurrentPlayer, _attemptsCount++));
            return false;
        }

        private bool AcceptMove(PlayerMove move)
        {
            if (MainBoard[move.SubBoardCellId.Row, move.SubBoardCellId.Column][move.AtomicCellId.Row, move.AtomicCellId.Column].SetOwningPlayerIfAvailable(move.PlayerMarker))
            {
                if (MainBoard[move.SubBoardCellId.Row, move.SubBoardCellId.Column].UpdateBoard());
                {
                    _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Length;
                    _attemptsCount = 0;
                    CurrentPlayerChanged?.Invoke(this, new HumanPlayerMoveEventArgs(CurrentPlayer));
                    return MainBoard.UpdateBoard();
                }
            }

            return false;
        }

        private Player CurrentPlayer => _players[_currentPlayerIndex];
    }
}
