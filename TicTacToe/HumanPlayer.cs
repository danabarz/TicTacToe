using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TicTacToe
{
    public class HumanPlayer : Player
    {
        private const int NumberOfSubBoards = 9;
        private Tuple<int, int> boardAndCell;

        public HumanPlayer()
        {
            IdPlayer = PlayerMarker.X;
            PlayerName = "Human";
        }

        public HumanPlayer(PlayerMarker playerMarker, string playerName)
        {
            IdPlayer = playerMarker;
            PlayerName = playerName;
        }

        public override PlayerMove ChooseMove(List<SubBoard> subBoards)
        {
            int innerRow = int.MinValue;
            int innerColumn = int.MinValue;
            int numberOfAttempts = 0;
            var SubBoardOpenMoves = new List<Tuple<int, int>>();
            var rowAndColumn = new Tuple<int, int>(innerRow, innerColumn);
            do
            {
                OnHumanPlaying(++numberOfAttempts);
                boardAndCell = ValidFormatAndRange(Console.ReadLine());
                if (boardAndCell.Item1 != int.MinValue || boardAndCell.Item2 != int.MinValue)
                {
                    SubBoardOpenMoves = subBoards[boardAndCell.Item1].FindOpenMoves();
                    innerRow = subBoards[boardAndCell.Item1].GetRow(boardAndCell.Item2);
                    innerColumn = subBoards[boardAndCell.Item1].GetColumn(boardAndCell.Item2);
                    rowAndColumn = Tuple.Create(innerRow, innerColumn);
                }
                OnLocationEntered();
            }
            while (!SubBoardOpenMoves.Contains(rowAndColumn));
            return new PlayerMove(subBoards[boardAndCell.Item1], innerRow, innerColumn, IdPlayer);
        }

        public Tuple<int, int> ValidFormatAndRange(string subBoardAndCellIndex)
        {
            string regexPattern = @"\d[,. ]\d";
            if (Regex.Match(subBoardAndCellIndex, regexPattern).Success)
            {
                string[] userInput = Regex.Split(subBoardAndCellIndex , @"[,. ]");
                string strBoardIndex = userInput[0];
                string strCellIndex = userInput[1];
                bool validBoardIndex = int.TryParse(strBoardIndex, out int subBoardIndex);
                bool validCellIndex = int.TryParse(strCellIndex, out int cellIndex);

                if (validBoardIndex && validCellIndex && subBoardIndex <= NumberOfSubBoards && cellIndex <= NumberOfSubBoards)
                {
                    return Tuple.Create(--subBoardIndex, --cellIndex);
                }
                return Tuple.Create(int.MinValue, int.MinValue);
            }
            return Tuple.Create(int.MinValue, int.MinValue);
        }
    }
}
