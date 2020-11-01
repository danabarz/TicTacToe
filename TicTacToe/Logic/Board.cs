using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Logic
{
    public abstract class Board<TCell>
        where TCell : class, IBoardCell
    {
        protected const int Dimensions = Game.BoardDimensions;
        private readonly IEnumerable<int> _rows = Enumerable.Range(0, Dimensions);
        private readonly IEnumerable<int> _columns = Enumerable.Range(0, Dimensions);

        protected Board(Func<BoardCellId ,TCell> cellFactory)
        {
            GameBoard = new TCell[Dimensions, Dimensions];
            Winner = null;

            for (int i = 0; i < Dimensions; i++)
            {
                for (int j = 0; j < Dimensions; j++)
                {     
                    GameBoard[i, j] = cellFactory(new BoardCellId(i, j));
                }
            }
        }

        public event EventHandler<EventArgs>? UpdatedBoardPieces;

        protected TCell[,] GameBoard { get; }
        public PlayerMarker? Winner { get; private set; }

        public TCell this[int row, int col] => GameBoard[row, col];

        public bool UpdateBoard()
        {
            UpdatedBoardPieces?.Invoke(this, EventArgs.Empty);
            return SetWinnerIfGameOver();
        }

        public Queue<BoardCellId> FindOpenMoves()
        {
            var emptyLocationsOnBoard = new Queue<BoardCellId>();
            for (int i = 0; i < Dimensions; i++)
            {
                for (int j = 0; j < Dimensions; j++)
                {
                    if (GameBoard[i, j].OwningPlayer == null)
                    {
                        emptyLocationsOnBoard.Enqueue(new BoardCellId(i, j));
                    }
                }
            }

            return emptyLocationsOnBoard;
        }

        public PlayerMarker? CheckIfGameOver()
        {
            if (Winner == null)
            {
                var winnerForBoard = (HorizontalWinForGame(GameBoard) ?? VerticalWinForGame()) ?? DiagonalWinForGame();
                return (winnerForBoard == null && TieForGame()) ?  PlayerMarker.Tie : winnerForBoard;
            }

            return Winner;
        }

        private bool SetWinnerIfGameOver()
        {
            var winnerMarker = CheckIfGameOver();
            if (winnerMarker != null)
            {
                Winner = winnerMarker;
                return true;
            }

            return false;
        }

        private PlayerMarker? HorizontalWinForGame(TCell[,] gameBoard)
        {
            foreach (var row in _rows)
            {
                var playerMarkerMainDiagonal = gameBoard[row, row].OwningPlayer;
                var horizontalWinResult = _columns.Where(col => gameBoard[row, col].OwningPlayer == playerMarkerMainDiagonal).ToList();

                if (horizontalWinResult.Count() == Dimensions && playerMarkerMainDiagonal != null && playerMarkerMainDiagonal != PlayerMarker.Tie)
                {
                    return playerMarkerMainDiagonal;
                }
            }

            return null;
        }

        private PlayerMarker? VerticalWinForGame()
        {
            return HorizontalWinForGame(TransposeGameBoard(GameBoard));
        }

        private PlayerMarker? DiagonalWinForGame()
        {
            return CheckDiagonalWin(_columns) ?? CheckDiagonalWin(_columns.Reverse());

            PlayerMarker? CheckDiagonalWin(IEnumerable<int> columns)
            {
                var playerMarkerDiagonal = GameBoard[_rows.First(), columns.First()].OwningPlayer;
                var diagonalResult = _rows.Where(row => GameBoard[row, columns.ElementAt(row)].OwningPlayer == playerMarkerDiagonal).ToList();
                return (diagonalResult.Count() == Dimensions && playerMarkerDiagonal != null && playerMarkerDiagonal != PlayerMarker.Tie) ? playerMarkerDiagonal : null;
            }
        }

        private bool TieForGame()
        {
            var tieResult = from row in _rows from col in _columns where GameBoard[row, col].OwningPlayer != null select row;         
            return tieResult.Count() == Dimensions * Dimensions;
        }

        private TCell[,] TransposeGameBoard(TCell[,] gameBoard)
        {
            TCell[,] transposeGameBoard = new TCell[Dimensions, Dimensions];

            for (int i = 0; i < Dimensions; i++)
            {
                for (int j = 0; j < Dimensions; j++)
                {
                    transposeGameBoard[j, i] = gameBoard[i, j];
                }
            }

            return transposeGameBoard;
        }
    }
}
