using System;
using TicTacToe.Logic;

namespace TicTacToe.Presentation
{
    public class GameView
    {
        private const int TextOriginTop = 38;
        private const int PrintBoardStartLocation = 2;
        private Game _game;
        private SummaryBoardView _summaryBoardView;
        private SubBoardView[,] _subBoardsView;

        public GameView()
        {
            _game = new Game();
            _summaryBoardView = new SummaryBoardView(_game._summaryBoard);
            _subBoardsView = new SubBoardView[_game._summaryBoard.dimensions, _game._summaryBoard.dimensions];
        }

        public void RunGame()
        {
            bool isGameStillOn = true;
            _game.AskingGameType += OnSelectingGameType;
            _game.AskingPlayersName += OnHumanVsHumanTypeSelected;
            _game.InitializedSubBoardsView += OnSubBoardsInitialized;
            _game._summaryBoard.UpdatedBoardPieces += OnBoardUpdated;
            PrintHomePage();
            _game.InitGameType();
            Console.Clear();
            _game._gameType.MangedPlayerTurns += OnPlayerPlayed;
            _game.InitSubBoards();

            while (isGameStillOn)
            {
                _game._gameType.TurnManager();
                _game._gameType.CurrentPlayer.PrintingHumanOutput += OnHumanPlaying;
                _game._gameType.CurrentPlayer.ClearedSpecificLine += OnLocationEntered;
                var playerMove = _game._gameType.CurrentPlayer.ChooseMove(_game);
                playerMove._subBoard.UpdatedBoardPieces += OnBoardUpdated;
                playerMove._subBoard.UpdatedSubBoardWinner += _game._summaryBoard.OnSubBoardWinnerUpdated;
                playerMove._subBoard.UpdateBoard(playerMove._row, playerMove._column, playerMove._marker);
                playerMove._subBoard.SetWinnerIfNeeded();
                _game._summaryBoard.SetWinnerIfNeeded();
                isGameStillOn = (_game._summaryBoard.Winner != null) ? false : true;
            }
            PrintResult(_game._summaryBoard.Winner);
        }

        private void OnSubBoardsInitialized(object source, SubBoardEventArgs args)
        {
            for (int i = 0; i < _game._summaryBoard.dimensions; i++)
            {
                for (int j = 0; j < _game._summaryBoard.dimensions; j++)
                {
                    _subBoardsView[i, j] = new SubBoardView(args.SubBoards[i, j]);
                    PrintBoards(_subBoardsView[i, j]);
                }
            }
            PrintBoards(_summaryBoardView);
        }

        private void OnBoardUpdated(object source, EventArgs args)
        {
            for (int i = 0; i < _game._summaryBoard.dimensions; i++)
            {
                for (int j = 0; j < _game._summaryBoard.dimensions; j++)
                {
                    PrintMarkers(_subBoardsView[i, j]);
                }
            }
            PrintMarkers(_summaryBoardView);
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

        private void OnSelectingGameType(object source, EventArgs args)
        {
            Console.WriteLine("\nPlease select game type: \nFor Human Vs Human game please press 1\nFor Human Vs Computer game please press 2");
        }

        private void OnHumanVsHumanTypeSelected(object source, IntEventArgs args)
        {
            Console.WriteLine("\nPlayer {0} - Please enter your name: ", args.Count);
        }

        private void PrintBoards(BoardView boardView)
        {
            Console.SetCursorPosition(boardView._originLocation.X, boardView._originLocation.Y);

            for (int i = boardView._originLocation.X; i <= boardView._originLocation.X + boardView.spaceBetweenPieces * _game._summaryBoard.dimensions; i += boardView.spaceBetweenPieces)
            {
                for (int j = boardView._originLocation.Y; j <= boardView._originLocation.Y + boardView.spaceBetweenPieces * _game._summaryBoard.dimensions; j++)
                {
                    if (i <= _game._summaryBoard.dimensions * boardView.spaceBetweenPieces * _game._summaryBoard.dimensions && i % (_game._summaryBoard.dimensions *
                        boardView.spaceBetweenPieces) == 0 || i <= _game._summaryBoard.dimensions * boardView.spaceBetweenPieces * _game._summaryBoard.dimensions &&
                        j % (_game._summaryBoard.dimensions * boardView.spaceBetweenPieces) == 0)
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
            for (int j = boardView._originLocation.Y; j <= boardView._originLocation.Y + boardView.spaceBetweenPieces * _game._summaryBoard.dimensions; j += boardView.spaceBetweenPieces)
            {
                for (int i = boardView._originLocation.X; i <= boardView._originLocation.X + boardView.spaceBetweenPieces * _game._summaryBoard.dimensions; i++)
                {
                    if (i <= _game._summaryBoard.dimensions * boardView.spaceBetweenPieces * _game._summaryBoard.dimensions && i % (_game._summaryBoard.dimensions *
                        boardView.spaceBetweenPieces) == 0 || i <= _game._summaryBoard.dimensions * boardView.spaceBetweenPieces * _game._summaryBoard.dimensions &&
                        j % (_game._summaryBoard.dimensions * boardView.spaceBetweenPieces) == 0)
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
            for (int row = 0; row < _game._summaryBoard.dimensions; row++)
            {
                for (int col = 0; col < _game._summaryBoard.dimensions; col++)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(boardView._originLocation.X + PrintBoardStartLocation + col * boardView.spaceBetweenPieces, boardView._originLocation.Y + PrintBoardStartLocation + row * boardView.spaceBetweenPieces);
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
