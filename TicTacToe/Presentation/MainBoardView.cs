using System.Drawing;
using TicTacToe.Logic;

namespace TicTacToe.Presentation
{
    public class MainBoardView : BoardView
    {
        private readonly MainBoard _mainBoard;
        private readonly SubBoardView[,] _subBoardsView;

        public MainBoardView(MainBoard mainBoard, Point topLeft)
        {
            _mainBoard = mainBoard;
            _subBoardsView = new SubBoardView[Game.BoardDimensions, Game.BoardDimensions];
            var subBoardTopLeft = topLeft;

            for (int i = 0; i < Game.BoardDimensions; i++)
            {
                for (int j = 0; j < Game.BoardDimensions; j++)
                {
                    _subBoardsView[i, j] = new SubBoardView(_mainBoard[i, j], subBoardTopLeft);
                    subBoardTopLeft = _subBoardsView[i, j].BoundingBox.TopRight;
                }

                subBoardTopLeft = _subBoardsView[i, 0].BoundingBox.BottomLeft;
            }

            BoundingBox = new BoundingBox(topLeft, _subBoardsView[Game.BoardDimensions - 1, Game.BoardDimensions - 1].BoundingBox.BottomRight);
        }

        public SubBoardView this[int row, int col] => _subBoardsView[row, col];

        public override void PrintMarkers()
        {
            for (int i = 0; i < Game.BoardDimensions; i++)
            {
                for (int j = 0; j < Game.BoardDimensions; j++)
                {
                    _subBoardsView[i, j].PrintMarkers();
                }
            }
        }
    }
}
