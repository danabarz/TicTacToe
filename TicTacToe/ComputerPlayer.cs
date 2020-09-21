using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TicTacToe
{
    class ComputerPlayer : Player
    {
        private readonly Random rand = new Random();

        public ComputerPlayer()
        {
            IdPlayer = PlayerMarker.O;
            PlayerName = "Computer";
        }

        /*
        public override PlayerMove ChooseMove(List<SubBoard> subBoards)
        {
            List<Tuple<int, int>> SubBoardOpenMoves;
            int subBoardRandomIndex;
            do
            {
                subBoardRandomIndex = rand.Next(boardDimensions * boardDimensions);
                SubBoardOpenMoves = subBoards[subBoardRandomIndex].FindOpenMoves();
            }
            while (SubBoardOpenMoves.Count == 0);
            int randomNumber = rand.Next(SubBoardOpenMoves.Count());
            int innerRow = SubBoardOpenMoves[randomNumber].Item1;
            int innerCol = SubBoardOpenMoves[randomNumber].Item2;
            Thread.Sleep(2000);
            return new PlayerMove(subBoards[subBoardRandomIndex], innerRow, innerCol, IdPlayer);
        }*/

        public override PlayerMove ChooseMove(List<SubBoard> subBoards)
        {
            //List<Tuple<int, int>> SubBoardOpenMoves;

            MiniMax miniMax = new MiniMax();
            int bestVal = -1000;
            SubBoard bestSubBoard = subBoards[0];

            foreach (SubBoard i in subBoards)
            {
                int moveVal = miniMax.Minimax(i, 0, true, this.IdPlayer);
                if (moveVal > bestVal)
                {
                    bestSubBoard = i;
                    bestVal = moveVal;
                }
            }
            Thread.Sleep(2000);
            return miniMax.FindBestMove(bestSubBoard, this.IdPlayer);



            /*
            int subBoardRandomIndex;
            do
            {
                subBoardRandomIndex = rand.Next(boardDimensions * boardDimensions);
                SubBoardOpenMoves = subBoards[subBoardRandomIndex].FindOpenMoves();
            }
            while (SubBoardOpenMoves.Count == 0);
            int randomNumber = rand.Next(SubBoardOpenMoves.Count());
            int innerRow = SubBoardOpenMoves[randomNumber].Item1;
            int innerCol = SubBoardOpenMoves[randomNumber].Item2;
            Thread.Sleep(2000);
            return new PlayerMove(subBoards[subBoardRandomIndex], innerRow, innerCol, IdPlayer);
            */
        }
    }
}
