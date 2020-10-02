using System.Drawing;
using System;
using TicTacToe.Logic;

namespace TicTacToe.Presentation
{
    public class SubBoardView : BoardView
    {
        private SubBoard _subBoard;
        public SubBoardView(SubBoard subBoard) : base(subBoard)
        {
            _subBoard = subBoard;
            _originLocation = new Point(_subBoard.Column * Game.BoardDimensions * SpaceBetweenPieces, _subBoard.Row * Game.BoardDimensions * SpaceBetweenPieces);
        }

        public BoundingBox BoundingBox { get; }
    }
}
