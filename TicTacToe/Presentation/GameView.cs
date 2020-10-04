using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using TicTacToe.Logic;

namespace TicTacToe.Presentation
{
    public class GameView
    {
        private enum GameType
        {
            HumanVsHuman = 1,
            HumanVsComputer
        }

        private Game? _game;
        private MainBoardView? _mainBoardView;
        private SummaryBoardView? _summaryBoardView;
        private HumanPlayerView? _humanPlayerView;
        private PlayerMove? _humanPlayerMove;

        public void RunGame()
        {
            PrintHomePage();
            Console.Clear();

            _game = InitGame();
            _mainBoardView = new MainBoardView(_game.MainBoard, new Point(0, 0));
            var middleSubBoardRow = Game.BoardDimensions / 2;
            _summaryBoardView = new SummaryBoardView(_game.MainBoard, _mainBoardView[middleSubBoardRow, Game.BoardDimensions - 1].BoundingBox.TopRight + new Size(BoardView.SpaceBetweenPieces, 0));
            _humanPlayerView = new HumanPlayerView(_mainBoardView.BoundingBox.BottomLeft + new Size(0, Game.BoardDimensions));
            _mainBoardView.PrintBoard();
            _summaryBoardView.PrintBoard();

            _game.HumanPlayerMoveRequested += OnHumanPlayerMoveRequested;
            _game.CurrentPlayerChanged += OnCurrentPlayerChanged;
            _game.Start();

            while (_game.Winner == null)
            {
                _game.AcceptHumanPlayerMoveAndProceed(_humanPlayerMove);
            }

            PrintResult(_game.Winner);
        }

        private Game InitGame()
        {
            bool isValidInput;
            int gameTypeOption;
            var allGameTypeOptions = GetEnumValuesAsIntegers<GameType>();
            do
            {
                Console.WriteLine($"\nPlease select game type: \nFor Human Vs Human game please press {(int)GameType.HumanVsHuman}\nFor Human Vs Computer game please press {(int)GameType.HumanVsComputer}");
                string gameTypeString = Console.ReadLine();
                Console.Clear();
                isValidInput = int.TryParse(gameTypeString, out gameTypeOption);
            }
            while (!isValidInput || !allGameTypeOptions.Contains(gameTypeOption));

            if (gameTypeOption == (int)GameType.HumanVsComputer)
            {
                return Game.CreateHumanVsComputer();
            }

            Console.Clear();
            Console.WriteLine("\nPlayer 1 - Please enter your name: ");
            string player1 = Console.ReadLine();
            Console.WriteLine("\nPlayer 2 - Please enter your name: ");
            string player2 = Console.ReadLine();
            Console.Clear();
            return Game.CreateHumanVsHuman(player1, player2);

            int[] GetEnumValuesAsIntegers<TEnum>() where TEnum : Enum
                => Enum.GetValues(typeof(TEnum)).Cast<int>().ToArray();
        }

        private void OnHumanPlayerMoveRequested(object? sender, PlayerEventArgs args)
        {
            SetBaseColor();
            var (boardNumber, cellNuber) = _humanPlayerView.AskForBoardAndCell(args.AttemptsCount);
            _humanPlayerMove = new PlayerMove(_game.MainBoard.GetRow(boardNumber), _game.MainBoard.GetColumn(boardNumber), _game.MainBoard.GetRow(cellNuber), _game.MainBoard.GetColumn(cellNuber), args.Player.IdPlayer);
        }

        private void OnCurrentPlayerChanged(object? source, PlayerEventArgs args)
        {
            _humanPlayerView.ClearHumanPlayerLines();
            _humanPlayerView.ClearCurrentConsoleLine(0, _mainBoardView.BoundingBox.BottomLeft.Y + Game.BoardDimensions - 1);
            SetBaseTextPosition();
            SetBaseColor();
            Console.WriteLine(args.Player.PlayerName + " it's your turn");
            if (args.Player.PlayerName.Equals("Computer"))
            {
                Thread.Sleep(1000);
            }
        }

        private void PrintResult(PlayerMarker? playerMarker)
        {
            if (playerMarker == PlayerMarker.X)
            {
                PrintXWon();
            }
            else if (playerMarker == PlayerMarker.O)
            {
                PrintOWon();
            }
            else
            {
                PrintTie();
            }
        }

        private void PrintXWon()
        {
            Console.Clear();
            SetBaseColor();
            Console.WriteLine("                                                              ,---,  ");
            Console.WriteLine("                                                           ,`--.' |  ");
            Console.WriteLine(" ,--,     ,--,                                             |   :  :  ");
            Console.WriteLine(" |'. \\   / .`|                                             '   '  ;  ");
            Console.WriteLine(" ; \\ `\\ /' / ;                  .---.   ,---.        ,---, |   |  |  ");
            Console.WriteLine(" `. \\  /  / .'                 /. ./|  '   ,'\\   ,-+-. /  |'   :  ;  ");
            Console.WriteLine("  \\  \\/  / ./               .-'-. ' | /   /   | ,--.'|'   ||   |  '  ");
            Console.WriteLine("   \\  \\.'  /               /___/ \\: |.   ; ,. :|   |  ,\"' |'   :  |  ");
            Console.WriteLine("    \\  ;  ;             .-'.. '   ' .'   | |: :|   | /  | |;   |  ;  ");
            Console.WriteLine("   / \\  \\  \\           /___/ \\:     ''   | .; :|   | |  | |`---'. |  ");
            Console.WriteLine("  ;  /\\  \\  \\          .   \\  ' .\\   |   :    ||   | |  |/  `--..`;  ");
            Console.WriteLine("./__;  \\  ;  \\          \\   \\   ' \\ | \\   \\  / |   | |--'  .--,_     ");
            Console.WriteLine("|   : / \\  \\  ;          \\   \\  |--\"   `----'  |   |/      |    |`.  ");
            Console.WriteLine(";   |/   \\  ' |           \\   \\ |              '---'       `-- -`, ; ");
            Console.WriteLine("`---'     `--`             '---\"                             '---`\"  ");
        }

        private void PrintTie()
        {
            Console.Clear();
            SetBaseColor();
            Console.WriteLine("  _ _   _       _   _      ");
            Console.WriteLine(" (_) | ( )     | | (_)     ");
            Console.WriteLine("  _| |_|/ ___  | |_ _  ___ ");
            Console.WriteLine(" | | __| / __| | __| |/ _ \\");
            Console.WriteLine(" | | |_  \\__ \\ | |_| |  __/");
            Console.WriteLine(" |_|\\__| |___/  \\__|_|\\___|");
        }

        private void PrintOWon()
        {
            Console.Clear();
            SetBaseColor();
            Console.WriteLine("                                                             ,---,  ");
            Console.WriteLine("    ,----..                                               ,`--.' |  ");
            Console.WriteLine("   /   /   \\                                              |   :  :  ");
            Console.WriteLine("   .  /     :                                             '   '  ;  ");
            Console.WriteLine(" .   /   ;.  \\                 .---.   ,---.        ,---, |   |  |  ");
            Console.WriteLine(".   ;   /  ` ;                /. ./|  '   ,'\\   ,-+-. /  |'   :  ;  ");
            Console.WriteLine(";   |  ; \\ ; |             .-'-. ' | /   /   | ,--.'|'   ||   |  '  ");
            Console.WriteLine("|   :  | ; | '            /___/ \\: |.   ; ,. :|   |  ,\"' |'   :  | ");
            Console.WriteLine(".   |  ' ' ' :         .-'.. '   ' .'   | |: :|   | /  | |;   |  ;  ");
            Console.WriteLine("'   ;  \\; /  |        /___/ \\:     ''   | .; :|   | |  | |`---'. |  ");
            Console.WriteLine(" \\   \\  ',  /         .   \\  ' .\\   |   :    ||   | |  |/  `--..`;  ");
            Console.WriteLine("  ;   :    /           \\   \\   ' \\ | \\   \\ / |   | |--'  .--,_     ");
            Console.WriteLine("   \\   \\ .'             \\   \\  |--\"   `----'  |   |/      |    |`.  ");
            Console.WriteLine("    `---`                \\   \\ |              '---'       `-- -`, ; ");
            Console.WriteLine("                          '---\"                             '---`\"  ");
        }

        private void PrintHomePage()
        {
            SetBaseColor();
            Console.Clear();
            Console.WriteLine("  __  ______  _            __        _______    ______        ______        ");
            Console.WriteLine(" / / / / / /_(_)_ _  ___ _/ /____   /_  __(_)__/_  __/__ ____/_  __/__  ___ ");
            Console.WriteLine("/ /_/ / / __/ /  ' \\/ _ `/ __/ -_)   / / / / __// / / _ `/ __// / / _ \\/ -_)");
            Console.WriteLine("\\____/_/\\__/_/_/_/_/\\_,_/\\__/\\__/   /_/ /_/\\__//_/  \\_,_/\\__//_/  \\___/\\__/ ");
            Console.WriteLine("\n\tWelcome to Tic Tac Toe, please press any key");
            Console.ReadKey();
            Console.Clear();
        }

        private void SetBaseColor()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Gray;
        }

        private void SetBaseTextPosition()
        {
            Console.SetCursorPosition(0, _mainBoardView.BoundingBox.BottomLeft.Y + Game.BoardDimensions - 1);
        }
    }
}
