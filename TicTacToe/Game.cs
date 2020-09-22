using System;
using System.Collections.Generic;

namespace TicTacToe
{
    public class Game
    {
        private const int BoardDimensions = 3;
        private const int SpaceBetweenCells = 4;
        private const int DistanceBetweenBoards = BoardDimensions * SpaceBetweenCells;
        private const int IndexGameTypeOne = 1;
        private const int IndexGameTypeTwo = 2;


        public Game()
        {
            SummaryBoard = new SummaryBoard();
            SubBoards = new List<SubBoard>();
        }

        public event EventHandler<GameEventArgs> PrintingBoards;
        public event EventHandler<EventArgs> AskingGameType;
        public event EventHandler<IntEventArgs> AskingPlayersName;

        public SummaryBoard SummaryBoard { get; set; }
        public List<SubBoard> SubBoards { get; set; }
        protected void OnBoardsInitialized(List<SubBoard> subBoards, SummaryBoard summaryBoard)
        {
            PrintingBoards?.Invoke(this, new GameEventArgs { SubBoards = subBoards, SummaryBoard = summaryBoard });
        }

        protected void OnSelectingGameType()
        {
            AskingGameType?.Invoke(this, EventArgs.Empty);
        }

        protected void OnHumanVsHumanTypeSelected(int number)
        {
            AskingPlayersName?.Invoke(this, new IntEventArgs { CountTimes = number });
        }

        public GameType InitGameType()
        {
            bool isIntEnterd;
            int gameTypeNumber;
            do
            {
                OnSelectingGameType();
                string gameTypeString = Console.ReadLine();
                Console.Clear();
                isIntEnterd = int.TryParse(gameTypeString, out gameTypeNumber);
            }
            while (!isIntEnterd || gameTypeNumber > IndexGameTypeTwo || gameTypeNumber < IndexGameTypeOne);

            if (gameTypeNumber == IndexGameTypeOne)
            {
                OnHumanVsHumanTypeSelected(IndexGameTypeOne);
                string player1 = Console.ReadLine();
                OnHumanVsHumanTypeSelected(IndexGameTypeTwo);
                string player2 = Console.ReadLine();

                Console.Clear();

                return new HumanVsHuman(player1, player2);
            }
            return new HumanVsComputer();
        }

        public void InitBoards()
        {
            int boardIndex = 0;
            for (int y = 0; y < DistanceBetweenBoards * BoardDimensions; y += DistanceBetweenBoards)
            {
                for (int x = 0; x < DistanceBetweenBoards * BoardDimensions; x += DistanceBetweenBoards)
                {
                    this.SubBoards.Add(new SubBoard(boardIndex, x, y));
                    boardIndex++;
                }
            }
            OnBoardsInitialized(SubBoards, SummaryBoard);
        }

    }
}
