using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TicTacToe
{
    public class HumanPlayer : Player
    {
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

        public override PlayerMove ChooseMove(Game game)
        {
            int cellRow = int.MinValue;
            int cellColumn = int.MinValue;
            int boardColumn = int.MinValue;
            int boardRow = int.MinValue;
            int numberOfAttempts = 0;
            var SubBoardOpenMoves = new List<Tuple<int, int>>();
            var desiredCell = new Tuple<int, int>(cellRow, cellColumn);

            do
            {
                OnHumanPlaying(++numberOfAttempts);
                var desiredMove = ValidFormatAndRange(Console.ReadLine(), game._summaryBoard.Dimensions);
                if (desiredMove.Item1 != int.MinValue || desiredMove.Item2 != int.MinValue)
                {
                    boardRow = GetRow(desiredMove.Item1);
                    boardColumn = GetColumn(desiredMove.Item1);
                    SubBoardOpenMoves = game._subBoards[boardRow, boardColumn].FindOpenMoves();

                    cellRow =  GetRow(desiredMove.Item2);
                    cellColumn = GetColumn(desiredMove.Item2);
                    desiredCell = Tuple.Create(cellRow, cellColumn);
                }
                OnLocationEntered();
            }
            while (!SubBoardOpenMoves.Contains(desiredCell));

            return new PlayerMove(game._subBoards[boardRow, boardColumn], cellRow, cellColumn, IdPlayer);

            int GetRow(int index)
            {
                return index / game._summaryBoard.Dimensions;
            }

            int GetColumn(int index)
            {
                return index % game._summaryBoard.Dimensions;
            }
        }

        private Tuple<int, int> ValidFormatAndRange(string subBoardAndCellIndex, int dimension)
        {
            string regexPattern = @"\d[,. ]\d";
            if (Regex.Match(subBoardAndCellIndex, regexPattern).Success)
            {
                string[] userInput = Regex.Split(subBoardAndCellIndex , @"[,. ]");
                string strSubBoardIndex = userInput[0];
                string strCellIndex = userInput[1];
                bool isSubBoardIndexValid = int.TryParse(strSubBoardIndex, out int subBoardIndex);
                bool isCellIndexValid = int.TryParse(strCellIndex, out int cellIndex);

                if (isSubBoardIndexValid && isCellIndexValid && subBoardIndex <= dimension * dimension && cellIndex <= dimension * dimension)
                {
                    return Tuple.Create(--subBoardIndex, --cellIndex);
                }
                return Tuple.Create(int.MinValue, int.MinValue);
            }
            return Tuple.Create(int.MinValue, int.MinValue);
        }
    }
}
