using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TicTacToe
{
    class HumanPlayer : Player
    {
        private const int numberOfSubBoards = 9;
        private Tuple<int, int> boardAndCell;

        public HumanPlayer()
        {
            this.IdPlayer = PlayerMarker.X;
            this.PlayerName = "Human";

        }

        public HumanPlayer(PlayerMarker playerMarker, string playerName)
        {
            this.IdPlayer = playerMarker;
            this.PlayerName = playerName;

        }

        public override PlayerMove ChooseMove(List<SubBoard> subBoards)
        {
            int innerRow = -1;
            int innerColumn = -1;
            int numberOfAttempts = 0;
            List<Tuple<int, int>> SubBoardOpenMoves = new List<Tuple<int, int>>();
            Tuple<int, int> rowAndColumn = new Tuple<int, int>(innerRow, innerColumn);
            do
            {
                OnHumanPlaying(++numberOfAttempts);
                boardAndCell = ValidFormatAndRange(Console.ReadLine());
                if (boardAndCell.Item1 != -1 || boardAndCell.Item2 != -1)
                {
                    SubBoardOpenMoves = subBoards[boardAndCell.Item1].FindOpenMoves();
                    innerRow = subBoards[boardAndCell.Item1].findRow(boardAndCell.Item2);
                    innerColumn = subBoards[boardAndCell.Item1].findColumn(boardAndCell.Item2);
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
                if (validBoardIndex && validCellIndex && subBoardIndex <= numberOfSubBoards && cellIndex <= numberOfSubBoards)
                {
                    return Tuple.Create(--subBoardIndex, --cellIndex);
                }
                return Tuple.Create(-1, -1);
            }
            return Tuple.Create(-1, -1);
        }
    }
}
