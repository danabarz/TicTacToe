using System;
using System.Drawing;
using System.Linq;
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

        private const int TextOriginTop = 38;
        private const int PrintBoardStartLocation = 2;

        private Game _game;
        private MainBoardView _mainBoardView;
        private SummaryBoardView _summaryBoardView;
        private PlayerMove _humanPlayerMove;

        public void RunGame()
        {
            PrintHomePage();
            Console.Clear();

            _game = InitGame();
            _mainBoardView = new MainBoardView(_game.MainBoard, new Point(0, 0));
            var middleSubBoardRow = Game.BoardDimensions / 2;
            _summaryBoardView = new SummaryBoardView(_game.MainBoard, _mainBoardView[middleSubBoardRow, Game.BoardDimensions - 1].BoundingBox.TopRight + new Size(BoardView.SpaceBetweenPieces, 0));

            _game.HumanPlayerMoveRequested += OnHumanPlayerMoveRequested;
            _game.Start();

            while (_game.Winner == null)
            {
                _game.AcceptHumanPlayerMoveAndProceed(_humanPlayerMove);

                //_game._gameType.TurnManager();
                //_game._gameType.CurrentPlayer.PrintingHumanOutput += OnHumanPlaying;
                //_game._gameType.CurrentPlayer.ClearedSpecificLine += OnLocationEntered;
                //var playerMove = _game._gameType.CurrentPlayer.ChooseMove(_game);
                //playerMove._subBoard.UpdatedBoardPieces += OnBoardUpdated;
                //playerMove._subBoard.UpdatedSubBoardWinner += _game._summaryBoard.OnSubBoardWinnerUpdated;
                //playerMove._subBoard.UpdateBoard(playerMove._row, playerMove._column, playerMove._marker);
                //playerMove._subBoard.SetWinnerIfNeeded();
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
                isValidInput = int.TryParse(gameTypeString, out gameTypeOption);
            }
            while (!isValidInput || !allGameTypeOptions.Contains(gameTypeOption));

            if (gameTypeOption == (int)GameType.HumanVsComputer)
            {
                return Game.CreateHumanVsComputer();
            }

            Console.WriteLine("\nPlayer 1 - Please enter your name: ");
            string player1 = Console.ReadLine();
            Console.WriteLine("\nPlayer 2 - Please enter your name: ");
            string player2 = Console.ReadLine();
            return Game.CreateHumanVsHuman(player1, player2);

            int[] GetEnumValuesAsIntegers<TEnum>() where TEnum : Enum
                => Enum.GetValues(typeof(TEnum)).Cast<int>().ToArray();
        }

        private void OnSubBoardsInitialized(object source, SubBoardEventArgs args)
        {
            for (int i = 0; i < Game.BoardDimensions; i++)
            {
                for (int j = 0; j < Game.BoardDimensions; j++)
                {
                    _subBoardsView[i, j] = new SubBoardView(args.SubBoards[i, j]);
                    PrintBoards(_subBoardsView[i, j]);
                }
            }
            PrintBoards(_summaryBoardView);
        }

        private void OnBoardUpdated(object source, EventArgs args)
        {
            for (int i = 0; i < Game.BoardDimensions; i++)
            {
                for (int j = 0; j < Game.BoardDimensions; j++)
                {
                    PrintMarkers(_subBoardsView[i, j]);
                }
            }
            PrintMarkers(_summaryBoardView);
        }

        private void OnHumanPlayerMoveRequested(object? sender, PlayerEventArgs e)
        {
            throw new NotImplementedException();

            _humanPlayerMove = null; // TODO: set human player move
        }

        private void OnPlayerPlayed(object source, PlayerEventArgs args)
        {           
            ClearCurrentConsoleLine(Console.CursorLeft, TextOriginTop);
            SetBaseTextPosition();
            SetBaseColor();
            Console.WriteLine(args.Player.PlayerName + " it's your turn");
        }

        private void OnHumanPlaying(object source, IntEventArgs args)
        {
            Console.SetCursorPosition(Console.CursorLeft, TextOriginTop + 1);
            if (args.Count == 1)
            {
                Console.WriteLine("Please enter sub board and cell, for example- \"1 1\": ");
            }
            else
            {
                Console.WriteLine("Oops... Something went wrong, please try again: ");
            }
        }

        private void OnLocationEntered(object source, EventArgs args)
        {
            ClearCurrentConsoleLine(Console.CursorLeft, Console.CursorTop - 1);
            ClearCurrentConsoleLine(Console.CursorLeft, TextOriginTop + 1);
        }

        private void PrintBoards(BoardView boardView)
        {
            Console.SetCursorPosition(boardView._originLocation.X, boardView._originLocation.Y);

            for (int i = boardView._originLocation.X; i <= boardView._originLocation.X + boardView.SpaceBetweenPieces * Game.BoardDimensions; i += boardView.SpaceBetweenPieces)
            {
                for (int j = boardView._originLocation.Y; j <= boardView._originLocation.Y + boardView.SpaceBetweenPieces * Game.BoardDimensions; j++)
                {
                    if (i <= Game.BoardDimensions * boardView.SpaceBetweenPieces * Game.BoardDimensions && i % (Game.BoardDimensions *
                        boardView.SpaceBetweenPieces) == 0 || i <= Game.BoardDimensions * boardView.SpaceBetweenPieces * Game.BoardDimensions &&
                        j % (Game.BoardDimensions * boardView.SpaceBetweenPieces) == 0)
                    {
                        SetBaseColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    Console.SetCursorPosition(i, j);
                    Console.Write("#");
                }
            }
            for (int j = boardView._originLocation.Y; j <= boardView._originLocation.Y + boardView.SpaceBetweenPieces * Game.BoardDimensions; j += boardView.SpaceBetweenPieces)
            {
                for (int i = boardView._originLocation.X; i <= boardView._originLocation.X + boardView.SpaceBetweenPieces * Game.BoardDimensions; i++)
                {
                    if (i <= Game.BoardDimensions * boardView.SpaceBetweenPieces * Game.BoardDimensions && i % (Game.BoardDimensions *
                        boardView.SpaceBetweenPieces) == 0 || i <= Game.BoardDimensions * boardView.SpaceBetweenPieces * Game.BoardDimensions &&
                        j % (Game.BoardDimensions * boardView.SpaceBetweenPieces) == 0)
                    {
                        SetBaseColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    Console.SetCursorPosition(i, j);
                    Console.Write("#");
                }
            }
        }

        private void PrintMarkers(BoardView boardView)
        {
            for (int row = 0; row < Game.BoardDimensions; row++)
            {
                for (int col = 0; col < Game.BoardDimensions; col++)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(boardView._originLocation.X + PrintBoardStartLocation + col * boardView.SpaceBetweenPieces, boardView._originLocation.Y + PrintBoardStartLocation + row * boardView.SpaceBetweenPieces);
                    if (boardView._board.GameBoard[row, col] == PlayerMarker.Tie)
                    {
                        Console.Write("-");
                    }
                    else
                    {
                        Console.Write(boardView._board.GameBoard[row, col]);
                    }
                }
                Console.WriteLine();
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
            Console.SetCursorPosition(0, TextOriginTop);
        }

        private void ClearCurrentConsoleLine(int originLeft, int originTop)
        {
            Console.SetCursorPosition(originLeft, originTop);
            Console.WriteLine(new String(' ', Console.BufferWidth));
        }
    }
}
