using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TicTacToe
{
    public class SummaryBoard : Board
    {
        private const int SpaceBetweenCells = 4;
        private const int DistanceBetweenBoards = BoardDimensions * SpaceBetweenCells;

        public SummaryBoard() : base()
        {
            BoardIndex = 10;
            BoardOriginLocation = new Point(DistanceBetweenBoards * BoardDimensions + SpaceBetweenCells, DistanceBetweenBoards);
        }

        public void OnSubBoardWinnerUpdated(object source, PlayerMoveEventArgs args)
        {
            args.PlayerMove.Board.UpdateBoard(args.PlayerMove);
        }

    }
}

