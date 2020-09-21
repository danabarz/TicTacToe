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
    }
}
