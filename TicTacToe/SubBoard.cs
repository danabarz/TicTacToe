using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TicTacToe
{
    public class SubBoard : Board
    {
        public SubBoard(int subBoardIndex, int boardOriginLeft, int boardOriginTop) : base()
        {
            BoardIndex = subBoardIndex;
            BoardOriginLocation = new Point(boardOriginLeft, boardOriginTop);
        }
    }
}
