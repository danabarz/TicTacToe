using System;

namespace TicTacToe
{
    public class Game
    {
        private const int IndexGameTypeOne = 1;
        private const int IndexGameTypeTwo = 2;
        public SummaryBoard _summaryBoard;
        public SubBoard[,] _subBoards;
        public GameType _gameType;

        public Game()
        {
            _summaryBoard = new SummaryBoard();
            _subBoards = new SubBoard[_summaryBoard.Dimensions, _summaryBoard.Dimensions];
        }

        public event EventHandler<GameEventArgs> InitializedSubBoardsView;
        public event EventHandler<EventArgs> AskingGameType;
        public event EventHandler<IntEventArgs> AskingPlayersName;

        private void OnSubBoardsInitialized(SubBoard[,] SubBoards)
        {
            InitializedSubBoardsView?.Invoke(this, new GameEventArgs { SubBoards = SubBoards });
        }

        private void OnSelectingGameType()
        {
            AskingGameType?.Invoke(this, EventArgs.Empty);
        }

        private void OnHumanVsHumanTypeSelected(int number)
        {
            AskingPlayersName?.Invoke(this, new IntEventArgs { Count = number });
        }

        public void InitGameType()
        {
            bool isValidInput;
            int gameTypeIndex;
            do
            {
                OnSelectingGameType();
                string gameTypeString = Console.ReadLine();
                isValidInput = int.TryParse(gameTypeString, out gameTypeIndex);
            }
            while (!isValidInput || gameTypeIndex > IndexGameTypeTwo || gameTypeIndex < IndexGameTypeOne);

            if (gameTypeIndex == IndexGameTypeOne)
            {
                OnHumanVsHumanTypeSelected(IndexGameTypeOne);
                string player1 = Console.ReadLine();
                OnHumanVsHumanTypeSelected(IndexGameTypeTwo);
                string player2 = Console.ReadLine();
                _gameType = new HumanVsHuman(player1, player2);
            }
            else
            {
                _gameType = new HumanVsComputer();
            }
        }

        public void InitSubBoards()
        {
            for (int i = 0; i < _summaryBoard.Dimensions; i++)
            {
                for (int j = 0; j < _summaryBoard.Dimensions; j++)
                {
                    _subBoards[i, j] = new SubBoard(i, j);
                }
            }
            OnSubBoardsInitialized(_subBoards);
        }
    }
}
