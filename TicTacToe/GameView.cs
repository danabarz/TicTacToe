using System;

namespace TicTacToe
{
    public class GameView
    {
        private const int TextOriginTop = 38;
        private const int PrintBoardStartLocation = 2;
        private readonly Game Game;
        private readonly BoardView SummaryBoardView;
        private readonly BoardView[,] SubBoardsView;

        public GameView()
        {
            Game = new Game();
            SummaryBoardView = new BoardView(Game._summaryBoard);
            SubBoardsView = new BoardView[Game._summaryBoard.Dimensions, Game._summaryBoard.Dimensions];
        }

        public void RunGame()
        {
            int numberOfTurns = 0;
            Game.AskingGameType += OnSelectingGameType;
            Game.AskingPlayersName += OnHumanVsHumanTypeSelected;
            Game.InitializedSubBoardsView += OnSubBoardsInitialized;
            Game._summaryBoard.UpdatedBoardPieces += OnBoardUpdated;
            PrintHomePage();
            Game.InitGameType();
            Console.Clear();
            Game._gameType.MangedPlayerTurns += OnPlayerPlayed;
            Game.InitSubBoards();

            while (Game._summaryBoard.Winner == null)
            {
                Game._gameType.TurnManager(numberOfTurns);
                Game._gameType.CurrentPlayer.PrintingHumanOutput += OnHumanPlaying;
                Game._gameType.CurrentPlayer.ClearedSpecificLine += OnLocationEntered;
                var playerMove = Game._gameType.CurrentPlayer.ChooseMove(Game);
                playerMove._board.UpdatedBoardPieces += OnBoardUpdated;
                playerMove._board.UpdatedSubBoardWinner += Game._summaryBoard.OnSubBoardWinnerUpdated;
                playerMove._board.UpdateBoard(playerMove);
                playerMove._board.HaveWinner(Game);
                Game._summaryBoard.HaveWinner(Game);
                numberOfTurns++;
            }
            PrintResult(Game._summaryBoard.Winner);
        }

        private void OnSubBoardsInitialized(object source, GameEventArgs args)
        {
            for (int i = 0; i < Game._summaryBoard.Dimensions; i++)
            {
                for (int j = 0; j < Game._summaryBoard.Dimensions; j++)
                {
                    SubBoardsView[i, j] = new BoardView(args.SubBoards[i, j]);
                    PrintBoards(SubBoardsView[i, j]);
                }
            }
            PrintBoards(SummaryBoardView);
        }

        private void OnBoardUpdated(object source, BoardEventArgs args)
        {
            if (args.GameBoard.Column == 3)
            {
                PrintMarkers(SummaryBoardView);
            }
            else
            {
                PrintMarkers(SubBoardsView[args.GameBoard.Row, args.GameBoard.Column]);
            }
        }

        private void OnPlayerPlayed(object source, PlayerEventArgs args)
        {
            if (args.Count != 0)
            {
                ClearCurrentConsoleLine(Console.CursorLeft, TextOriginTop);
            }
            SetBaseTextPosition();
            SetBaseColor();
            Console.WriteLine(args.Player.PlayerName + "it's your turn");
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

            for (int i = boardView._originLocation.X; i <= boardView._originLocation.X + boardView.SpaceBetweenPieces * boardView._board.Dimensions;
                i += boardView.SpaceBetweenPieces)
            {
                for (int j = boardView._originLocation.Y; j <= boardView._originLocation.Y + boardView.SpaceBetweenPieces * boardView._board.Dimensions; j++)
                {
                    if (i <= boardView._board.Dimensions * boardView.SpaceBetweenPieces * boardView._board.Dimensions && i % (boardView._board.Dimensions *
                        boardView.SpaceBetweenPieces) == 0 || i <= boardView._board.Dimensions * boardView.SpaceBetweenPieces * boardView._board.Dimensions &&
                        j % (boardView._board.Dimensions * boardView.SpaceBetweenPieces) == 0)
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
            for (int j = boardView._originLocation.Y; j <= boardView._originLocation.Y + boardView.SpaceBetweenPieces * boardView._board.Dimensions;
                j += boardView.SpaceBetweenPieces)
            {
                for (int i = boardView._originLocation.X; i <= boardView._originLocation.X + boardView.SpaceBetweenPieces * boardView._board.Dimensions; i++)
                {
                    if (i <= boardView._board.Dimensions * boardView.SpaceBetweenPieces * boardView._board.Dimensions && i % (boardView._board.Dimensions *
                        boardView.SpaceBetweenPieces) == 0 || i <= boardView._board.Dimensions * boardView.SpaceBetweenPieces * boardView._board.Dimensions &&
                        j % (boardView._board.Dimensions * boardView.SpaceBetweenPieces) == 0)
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
            for (int row = 0; row < boardView._board.Dimensions; row++)
            {
                for (int col = 0; col < boardView._board.Dimensions; col++)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(boardView._originLocation.X + PrintBoardStartLocation + col * boardView.SpaceBetweenPieces,
                        boardView._originLocation.Y + PrintBoardStartLocation + row * boardView.SpaceBetweenPieces);
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
