using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class SummaryBoard : Board
    {
        private const int spaceBetweenCells = 4;
        private const int distanceBetweenBoards = boardDimensions * spaceBetweenCells;

        public SummaryBoard() : base()
        {
            this.BoardIndex = 10;
            this.BoardOriginLeft = distanceBetweenBoards * boardDimensions + spaceBetweenCells;
            this.BoardOriginTop = distanceBetweenBoards;
        }

        public void OnSubBoardHaveOwner(object source, TicTacToeEventArgs args)
        {
            args.PlayerMove.Board.UpdateBoard(args.PlayerMove);
        }
    }
}

