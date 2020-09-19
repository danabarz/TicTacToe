using System;
using System.Collections.Generic;

namespace TicTacToe
{
    class Game
    {
        private const int boardDimensions = 3;
        private const int spaceBetweenCells = 4;
        private const int distanceBetweenBoards = boardDimensions * spaceBetweenCells;

        public Game()
        {
            this.SummaryBoard = new SummaryBoard();
            this.SubBoards = new List<SubBoard>();
        }

        public event EventHandler<TicTacToeEventArgs> PrintBoards;
        public event EventHandler<EventArgs> AskGameType;
        public event EventHandler<TicTacToeEventArgs> AskPlayersName;

        public SummaryBoard SummaryBoard { get; set; }
        public List<SubBoard> SubBoards { get; set; }

        protected virtual void OnBoardsInitialized(List<SubBoard> subBoards, SummaryBoard summaryBoard)
        {
            if (PrintBoards != null)
            {
                PrintBoards(this, new TicTacToeEventArgs() { SubBoards = subBoards, SummaryBoard = summaryBoard });
            }
        }

        protected virtual void OnGameTypeSelected()
        {
            if (AskGameType != null)
            {
                AskGameType(this, EventArgs.Empty);
            }
        }

        protected virtual void OnHumanVsHumanTypeSelected(int number)
        {
            if (AskPlayersName != null)
            {
                AskPlayersName(this, new TicTacToeEventArgs() { NumberOfTimes = number });
            }
        }

        public GameType InitGameType()
        {
            bool isIntEnterd;
            int gameTypeNumber;
            do
            {
                OnGameTypeSelected();
                string gameTypeString = Console.ReadLine();
                Console.Clear();
                isIntEnterd = int.TryParse(gameTypeString, out gameTypeNumber);
            }
            while (!isIntEnterd || gameTypeNumber > 2 || gameTypeNumber < 1);
            if (gameTypeNumber == 1)
            {
                OnHumanVsHumanTypeSelected(1);
                string player1 = Console.ReadLine();
                OnHumanVsHumanTypeSelected(2);
                string player2 = Console.ReadLine();
                Console.Clear();
                return new HumanVsHuman(player1, player2);
            }
            return new HumanVsComputer();;
        }

        public void InitBoards()
        {
            int boardIndex = 0;
            for (int y = 0; y < distanceBetweenBoards * boardDimensions; y += distanceBetweenBoards)
            {
                for (int x = 0; x < distanceBetweenBoards * boardDimensions; x += distanceBetweenBoards)
                {
                    this.SubBoards.Add(new SubBoard(boardIndex, x, y));
                    boardIndex++;
                }
            }
            OnBoardsInitialized(this.SubBoards, this.SummaryBoard);
        }

    }
}
