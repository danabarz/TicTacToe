using System;
using System.Drawing;
using System.Text.RegularExpressions;
using TicTacToe.Logic;

namespace TicTacToe.Presentation
{
    public class HumanPlayerView
    {
        private readonly Point _topLeft;

        public HumanPlayerView(Point topLeft)
        {
            _topLeft = topLeft;
        }

        public PlayerMove AskForBoardAndCell(int attemps, PlayerMarker playerMarker)
        {
            ClearHumanPlayerLines();
            Console.SetCursorPosition(_topLeft.X, _topLeft.Y);

            if (attemps == 0)
            {
                Console.WriteLine("Please enter sub board and cell, for example- \"1 1\": ");
            }

            else
            {
                Console.WriteLine("Oops... This cell already taken, Please choose another cell: ");
            }

            (int subBoard, int cellBoard) = AskForValidalidInput(CheckValidFormatAndRange(Console.ReadLine()));
            return new PlayerMove(new BoardCellId(GetRow(subBoard), GetColumn(subBoard)), new BoardCellId(GetRow(cellBoard), GetColumn(cellBoard)), playerMarker);
        }

        public void ClearHumanPlayerLines()
        {
            ClearCurrentConsoleLine(_topLeft.X, _topLeft.Y + 1);
            ClearCurrentConsoleLine(_topLeft.X, _topLeft.Y);
        }

        public void ClearCurrentConsoleLine(int originLeft, int originTop)
        {
            Console.SetCursorPosition(originLeft, originTop);
            Console.WriteLine(new String(' ', Console.BufferWidth));
        }


        private (int, int) AskForValidalidInput((int, int)? desiredMove)
        {
            while (desiredMove == null)
            {
                ClearHumanPlayerLines();
                Console.SetCursorPosition(_topLeft.X, _topLeft.Y);
                Console.WriteLine("Oops... Please select numbers between 1-9 and write in a proper format");
                desiredMove = CheckValidFormatAndRange(Console.ReadLine());
            }

            return desiredMove.Value;
        }

        private (int, int)? CheckValidFormatAndRange(string subBoardAndCellIndex)
        {
            string regexPattern = @"\d[,. ]\d";

            if (Regex.Match(subBoardAndCellIndex, regexPattern).Success)
            {
                string[] userInput = Regex.Split(subBoardAndCellIndex, @"[,. ]");
                string strSubBoardIndex = userInput[0];
                string strCellIndex = userInput[1];
                bool isSubBoardIndexValid = int.TryParse(strSubBoardIndex, out int subBoardIndex);
                bool isCellIndexValid = int.TryParse(strCellIndex, out int cellIndex);

                if (isSubBoardIndexValid && isCellIndexValid && subBoardIndex <= Game.BoardDimensions * Game.BoardDimensions && cellIndex <= Game.BoardDimensions * Game.BoardDimensions)
                {
                    return (--subBoardIndex, --cellIndex);
                }
            }

            return null;
        }

        private int GetRow(int index) => index / Game.BoardDimensions;

        private int GetColumn(int index) => index % Game.BoardDimensions;
    }
}
