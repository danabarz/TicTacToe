using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TicTacToe
{
    public class ComputerPlayer : Player
    {
        public ComputerPlayer()
        {
            IdPlayer = PlayerMarker.O;
            PlayerName = "Computer";
        }

        public override PlayerMove ChooseMove(List<SubBoard> subBoards)
        {
            //todo: check first the last sub board that was updated and just after that check the other sub boards
            var miniMax = new MiniMax();
            foreach (SubBoard i in subBoards)
            {
                if (i.Winner == null)
                {
                    return miniMax.FindBestMove(i, this.IdPlayer);
                }
            }
            return null;
        }
   

        /*
        private readonly Random rand = new Random();
        
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
    }
}
