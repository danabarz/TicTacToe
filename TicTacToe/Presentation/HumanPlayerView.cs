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
            PlayerMove? move = null;
            ClearHumanPlayerLines();
            Console.SetCursorPosition(_topLeft.X, _topLeft.Y);
            Console.WriteLine("Please enter sub board and cell: ");

            while (move == null)
            {
                try
                {
                    (int, int)? desireLocation = CheckValidFormat(Console.ReadLine());
                    if (desireLocation != null)
                    {
                        move = CheckValidNumbers(desireLocation.Value.Item1, desireLocation.Value.Item2, playerMarker);
                    }
                }

                catch (Exception e)
                {
                    ClearHumanPlayerLines();
                    Console.SetCursorPosition(_topLeft.X, _topLeft.Y);
                    Console.WriteLine(e.Message);
                }
            }

            return move;
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


        private PlayerMove? CheckValidNumbers(int boardIndex, int cellINdex, PlayerMarker playerMarker)
        {
            if (boardIndex < Game.BoardDimensions * Game.BoardDimensions && cellINdex < Game.BoardDimensions * Game.BoardDimensions && boardIndex >= 0 && cellINdex >= 0)
            {
                return new PlayerMove(new BoardCellId(GetRow(boardIndex), GetColumn(boardIndex)), new BoardCellId(GetRow(cellINdex), GetColumn(cellINdex)), playerMarker);
            }

            throw new InvalidOperationException("Oops...Not a valid numbers. Please select numbers between 1 - 9");  
        }

        private (int, int)? CheckValidFormat(string subBoardAndCellIndex)
        {
            string regexPattern = @"\d[,. ]\d";
            if (Regex.Match(subBoardAndCellIndex, regexPattern).Success)
            {
                string[] userInput = Regex.Split(subBoardAndCellIndex, @"[,. ]");
                string strSubBoardIndex = userInput[0];
                string strCellIndex = userInput[1];
                int subBoardIndex = int.Parse(strSubBoardIndex);
                int cellIndex = int.Parse(strCellIndex);
                return (--subBoardIndex, --cellIndex);
            }

            throw new FormatException("Oops... Input was not in a correct format- 1,1");
        }

        private int GetRow(int index) => index / Game.BoardDimensions;

        private int GetColumn(int index) => index % Game.BoardDimensions;
    }
}
