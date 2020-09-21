using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class SubBoard : Board
    {
        public SubBoard(int subBoardIndex, int boardOriginLeft, int boardOriginTop) : base()
        {
            this.BoardIndex = subBoardIndex;
            this.BoardOriginLeft = boardOriginLeft;
            this.BoardOriginTop = boardOriginTop;
        }

        public SubBoard(PlayerMarker?[,] previousSubBoard)
        {
            this.GameBoard = previousSubBoard;
        }

        public override object Clone()
        {
            Board board = new SubBoard(this.GameBoard);
            return board;

        }
    }
}
