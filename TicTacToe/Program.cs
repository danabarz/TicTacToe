using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Threading;


namespace TicTacToe
{
    class Program
    {
        const int printBoardStartLocation = 2;
        const int spaceBetweenCells = 4;
        const int boardDimensions = 3;
        const int textOriginTop = 38;
        const int boardCellNumber = boardDimensions * boardDimensions;
        const int distanceBetweenBoards = boardDimensions * spaceBetweenCells;
        const char player1Marker = 'X';
        const char player2Marker = 'O';
        const char emptyMarker = ' ';
        const char tieMarker = '-';
        static readonly Random rand = new Random();
        static readonly char[][] gameSubBoards = new char[boardCellNumber][];
        static readonly char[] gameSummaryBoard = new char[boardCellNumber];

        static void Main(string[] args)
        {
            char currentPlayer = player2Marker;
            int subBoardIndex;
            HomePage();
            InitBoard();
            PrintGameBoards();

            while (FullGameOver() == SummaryBoardStatus.GameStillOn) 
            {
                currentPlayer = SwitchPlayer(currentPlayer);
                subBoardIndex = (currentPlayer == player1Marker) ? HumanChooseSubBoardAndCell() : ComputerChooseSubBoardAndCell();
                SubGameOver(subBoardIndex);
                PrintGameBoards();
            }
            Console.Clear();
            switch (FullGameOver())
            {
                case SummaryBoardStatus.WinX:
                    PrintWon();
                    break;
                case SummaryBoardStatus.WinO:
                    PrintLose();
                    break;
                case SummaryBoardStatus.Tie:
                    PrintTie();
                    break;
            }
        }
        private static char SwitchPlayer(char currentPlayer)
        {
             return currentPlayer = (currentPlayer == player1Marker) ? player2Marker : player1Marker;
        }
        private static void InitBoard()
        {

            for (int i = 0; i < boardCellNumber; i++)
            {
                gameSubBoards[i] = new char[boardCellNumber];
                gameSummaryBoard[i] = emptyMarker;
                for (int j = 0; j < boardCellNumber; j++)
                {
                    gameSubBoards[i][j] = emptyMarker;
                }
            }
        }
        private static void PrintBoardAndPieces(int boardOriginLeft, int boardOriginTop, int subBoard)
        {
            // Printing the game board
            Console.SetCursorPosition(boardOriginLeft, boardOriginTop);
            for (int i = boardOriginLeft; i <= boardOriginLeft + distanceBetweenBoards; i += spaceBetweenCells)
            {
                for (int j = boardOriginTop; j <= boardOriginTop + distanceBetweenBoards; j++)
                {
                    if (i <= distanceBetweenBoards * boardDimensions && i % distanceBetweenBoards == 0 || i <= distanceBetweenBoards * boardDimensions && j % distanceBetweenBoards == 0)
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

            for (int j = boardOriginTop; j <= boardOriginTop + distanceBetweenBoards; j += spaceBetweenCells)
            {
                for (int i = boardOriginLeft; i <= boardOriginLeft + distanceBetweenBoards; i ++)
                {
                    if (i <= distanceBetweenBoards * boardDimensions && i % distanceBetweenBoards == 0 || i <= distanceBetweenBoards * boardDimensions && j % distanceBetweenBoards == 0)
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

            // Updating the game board
            int cellInBoard = 0;
            for (int j = boardOriginTop + printBoardStartLocation; j <= boardOriginTop + distanceBetweenBoards - printBoardStartLocation; j += spaceBetweenCells)
            {
                for (int i = boardOriginLeft + printBoardStartLocation; i <= boardOriginLeft + distanceBetweenBoards - printBoardStartLocation; i += spaceBetweenCells)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(i, j);
                    char updatePieceInBoard = (subBoard == boardCellNumber) ? gameSummaryBoard[cellInBoard] : gameSubBoards[subBoard][cellInBoard];
                    Console.Write(updatePieceInBoard);
                    cellInBoard++;                  
                }
            }
        }

        private static void PrintGameBoards()
        {
            int subBoardIndex = 0;
            PrintBoardAndPieces(0, 0, subBoardIndex++);
            PrintBoardAndPieces(distanceBetweenBoards, 0, subBoardIndex++);
            PrintBoardAndPieces(distanceBetweenBoards * printBoardStartLocation, 0, subBoardIndex++);
            PrintBoardAndPieces(0, distanceBetweenBoards, subBoardIndex++);
            PrintBoardAndPieces(distanceBetweenBoards, distanceBetweenBoards, subBoardIndex++);
            PrintBoardAndPieces(distanceBetweenBoards * printBoardStartLocation, distanceBetweenBoards, subBoardIndex++);
            PrintBoardAndPieces(0, distanceBetweenBoards * printBoardStartLocation, subBoardIndex++);
            PrintBoardAndPieces(distanceBetweenBoards, distanceBetweenBoards * printBoardStartLocation, subBoardIndex++);
            PrintBoardAndPieces(distanceBetweenBoards * printBoardStartLocation, distanceBetweenBoards * printBoardStartLocation, subBoardIndex++);
            PrintBoardAndPieces(distanceBetweenBoards * boardDimensions + spaceBetweenCells, distanceBetweenBoards, subBoardIndex);
        }   
        private static int HumanChooseSubBoardAndCell()
        {
            SetBaseTextPosition();
            SetBaseColor();
            Console.Write("Human Turn");
            string askPositionFromUser = "Please enter sub board and cell: ";
            var(subBoardNumber, cellNumber) = PrintMessageAndCheckValid(askPositionFromUser);
            while (subBoardNumber == -1 || cellNumber == -1)
            {
                Console.SetCursorPosition(0, textOriginTop + 1);
                ClearCurrentConsoleLine();
                askPositionFromUser = "Oops... Please enter valid input: ";
                (subBoardNumber, cellNumber) = PrintMessageAndCheckValid(askPositionFromUser);
            }
            Console.SetCursorPosition(0, textOriginTop + 1);
            ClearCurrentConsoleLine();
            SetBaseTextPosition();
            ClearCurrentConsoleLine();
            gameSubBoards[subBoardNumber][cellNumber] = player1Marker;
            return subBoardNumber;

            static Tuple<int, int> PrintMessageAndCheckValid(string massegeForUser)
            {
                Console.SetCursorPosition(0, textOriginTop + 1);
                Console.Write(massegeForUser);
                string subBoardAndCellInput = Console.ReadLine();
                string[] numbers = subBoardAndCellInput.Split(new char[] { ' ', ',', '.' });
                string input = numbers[0];
                string input2 = numbers[1];
                bool res = int.TryParse(input, out int subBoardNumber);
                bool res2 = int.TryParse(input2, out int cellNumber);
                if (res && res2 && subBoardNumber <= boardCellNumber && cellNumber <= boardCellNumber && gameSubBoards[--subBoardNumber][--cellNumber] == emptyMarker)
                {
                    return Tuple.Create(subBoardNumber, cellNumber);
                }
                return Tuple.Create(-1, -1);
            }
        }
        private static int ComputerChooseSubBoardAndCell()
        {
            var emptyLocation= FindEmptyCells();
            int subBoardNumber, cellNumber;
            int randomNumber;
            SetBaseTextPosition();
            SetBaseColor();
            Console.Write("Computer Turn");
            randomNumber = rand.Next(emptyLocation.Count());
            subBoardNumber = emptyLocation[randomNumber].Item1;
            cellNumber = emptyLocation[randomNumber].Item2;
            gameSubBoards[subBoardNumber][cellNumber] = player2Marker;
            Thread.Sleep(1000);
            ClearCurrentConsoleLine();
            return subBoardNumber;
        }

        private static List<Tuple<int, int>> FindEmptyCells()
        {
            var numbers = new List<Tuple<int, int>>();
            for (int i = 0; i < boardCellNumber; i++)
            {
                char[] empty = gameSubBoards[i];
                int[] matchPositions = empty.Select((value, index) => value == emptyMarker ? index : -1).Where( index => index != -1).ToArray();
                foreach(int x in matchPositions)
                {
                    numbers.Add(new Tuple<int, int>(i, x));
                }
            }
            return numbers;
        }
        public static SummaryBoardStatus FullGameOver()
        {
            SummaryBoardStatus theWinner = SummaryBoardStatus.GameStillOn;
            theWinner = HorizontalWinForSummaryBoard(theWinner);
            if (IsWinForSummaryGame(theWinner))
            {
                return theWinner;
            }
            theWinner = VerticalWinForSummaryBoard(theWinner);
            if (IsWinForSummaryGame(theWinner))
            {
                return theWinner;
            }
            theWinner = DiagonalWinForSummaryBoard(theWinner);
            if (IsWinForSummaryGame(theWinner))
            {
                return theWinner;
            }
            theWinner = TieForSummaryBoard(theWinner);
            if (IsWinForSummaryGame(theWinner))
            {
                return theWinner;
            }
            return theWinner;

            static bool IsWinForSummaryGame(SummaryBoardStatus theWinner)
            {
                return (theWinner != SummaryBoardStatus.GameStillOn);
            }
        }

        public enum SummaryBoardStatus { WinX, WinO, Tie, GameStillOn}

        public static SummaryBoardStatus HorizontalWinForSummaryBoard(SummaryBoardStatus theWinner)
        {
            for (int i = 0; i < boardCellNumber; i += 3)
            {
                if (gameSummaryBoard[i] == gameSummaryBoard[i + 1] && gameSummaryBoard[i] ==  gameSummaryBoard[i + 2]
                && gameSummaryBoard[i] != emptyMarker && gameSummaryBoard[i] != tieMarker)
                {
                    theWinner = (gameSummaryBoard[i] == player1Marker) ? SummaryBoardStatus.WinX : SummaryBoardStatus.WinO;
                    return theWinner;
                }
            }
            return theWinner;
        }

        public static SummaryBoardStatus VerticalWinForSummaryBoard(SummaryBoardStatus theWinner)
        {
            for (int i = 0; i < boardDimensions; i++)
            {
                if (gameSummaryBoard[i] == gameSummaryBoard[i + 3] && gameSummaryBoard[i] == gameSummaryBoard[i + 6]
                && gameSummaryBoard[i] != emptyMarker && gameSummaryBoard[i] != tieMarker)
                {
                    theWinner = (gameSummaryBoard[i] == player1Marker) ? SummaryBoardStatus.WinX : SummaryBoardStatus.WinO;
                    return theWinner;
                }
            }
            return theWinner;
        }

        public static SummaryBoardStatus DiagonalWinForSummaryBoard(SummaryBoardStatus theWinner)
        {
            if ((gameSummaryBoard[0] == gameSummaryBoard[4] && gameSummaryBoard[0] == gameSummaryBoard[8] && gameSummaryBoard[0] != emptyMarker && gameSummaryBoard[0] != tieMarker)
            || (gameSummaryBoard[2] == gameSummaryBoard[4] && gameSummaryBoard[2] == gameSummaryBoard[6] && gameSummaryBoard[2] != emptyMarker && gameSummaryBoard[2] != tieMarker))
            {
                theWinner = (gameSummaryBoard[4] == player1Marker) ? SummaryBoardStatus.WinX : SummaryBoardStatus.WinO;
                return theWinner;
            }
            return theWinner;
        }
        public static SummaryBoardStatus TieForSummaryBoard(SummaryBoardStatus theWinner)
        {
            for (int i = 0; i < boardCellNumber; i++)
            {
                char emptyPosition = Array.Find(gameSummaryBoard, cell => cell == emptyMarker);
                if (emptyPosition != emptyMarker)
                {
                    theWinner = SummaryBoardStatus.Tie;
                    return theWinner;
                }
            }
            return theWinner;
        }

        public static void SubGameOver(int subBoardIndex)
        {
            HorizontalWinForSubGame(subBoardIndex);
            VerticalWinForSubGame(subBoardIndex);
            DiagonalWinForSubGame(subBoardIndex);
            TieForSubGame(subBoardIndex);
        }
        public static void HorizontalWinForSubGame(int subBoardIndex)
        {
            for (int i = 0; i < boardCellNumber; i += boardCellNumber)
            {
                if (gameSubBoards[subBoardIndex][i] == gameSubBoards[subBoardIndex][i+1] && gameSubBoards[subBoardIndex][i] == gameSubBoards[subBoardIndex][i+2]
                && gameSubBoards[subBoardIndex][i] != emptyMarker)
                {
                    gameSummaryBoard[subBoardIndex] = gameSubBoards[subBoardIndex][i];
                }
            }
        }
        public static void VerticalWinForSubGame(int subBoardIndex)
        {
            for (int i = 0; i < boardDimensions; i++)
            {
                if (gameSubBoards[subBoardIndex][i] == gameSubBoards[subBoardIndex][i+ boardDimensions] && gameSubBoards[subBoardIndex][i] == gameSubBoards[subBoardIndex][i+ boardDimensions * 2]
                && gameSubBoards[subBoardIndex][i] != emptyMarker)
                {
                    gameSummaryBoard[subBoardIndex] = gameSubBoards[subBoardIndex][i];
                }
            }
        }
        public static void DiagonalWinForSubGame(int subBoardIndex)
        {
            if ((gameSubBoards[subBoardIndex][0] == gameSubBoards[subBoardIndex][4] && gameSubBoards[subBoardIndex][0] == gameSubBoards[subBoardIndex][8] && gameSubBoards[subBoardIndex][0] != emptyMarker)
            || (gameSubBoards[subBoardIndex][2] == gameSubBoards[subBoardIndex][4] && gameSubBoards[subBoardIndex][2] == gameSubBoards[subBoardIndex][6] && gameSubBoards[subBoardIndex][2] != emptyMarker))
            {
                gameSummaryBoard[subBoardIndex] = gameSubBoards[subBoardIndex][4];
            }
        }
        public static void TieForSubGame(int subBoardIndex)
        {
            for (int i = 0; i < boardCellNumber; i++)
            {
                char [] subBoard = gameSubBoards[subBoardIndex];
                char emptyPosition = Array.Find(subBoard, cell => cell == emptyMarker);
                if (emptyPosition != emptyMarker)
                {
                    char pieceToAddSummaryBoard = tieMarker;
                    gameSummaryBoard[subBoardIndex] = pieceToAddSummaryBoard;
                }
            }
        }
        public static void PrintWon()
        {
            SetBaseColor();
            Console.WriteLine("     )                                   ____ ");
            Console.WriteLine("  ( /(             (  (                 |   / ");
            Console.WriteLine("  )\\())       (    )\\))(   '            |  /  ");
            Console.WriteLine(" ((_)\\  (    ))\\  ((_)()\\ )  (    (     | /   ");
            Console.WriteLine("__ ((_) )\\  /((_) _(())\\_)() )\\   )\\ )  |/    ");
            Console.WriteLine("\\ \\ / /((_)(_))(  \\ \\((_)/ /((_) _(_/( (      ");
            Console.WriteLine(" \\ V // _ \\| || |  \\ \\/\\/ // _ \\| ' \\)))\\     ");
            Console.WriteLine("  |_| \\___/ \\_,_|   \\_/\\_/ \\___/|_||_|((_)    ");
        }

        public static void PrintLose()
        {
            SetBaseColor();
            Console.WriteLine("  ▄████  ▄▄▄       ███▄ ▄███▓▓█████     ▒█████   ██▒   █▓▓█████  ██▀███   ▐██▌ ");
            Console.WriteLine(" ██▒ ▀█▒▒████▄    ▓██▒▀█▀ ██▒▓█   ▀    ▒██▒  ██▒▓██░   █▒▓█   ▀ ▓██ ▒ ██▒ ▐██▌ ");
            Console.WriteLine("▒██░▄▄▄░▒██  ▀█▄  ▓██    ▓██░▒███      ▒██░  ██▒ ▓██  █▒░▒███   ▓██ ░▄█ ▒ ▐██▌ ");
            Console.WriteLine("░▓█  ██▓░██▄▄▄▄██ ▒██    ▒██ ▒▓█  ▄    ▒██   ██░  ▒██ █░░▒▓█  ▄ ▒██▀▀█▄   ▓██▒ ");
            Console.WriteLine("░▒▓███▀▒ ▓█   ▓██▒▒██▒   ░██▒░▒████▒   ░ ████▓▒░   ▒▀█░  ░▒████▒░██▓ ▒██▒ ▒▄▄  ");
            Console.WriteLine(" ░▒   ▒  ▒▒   ▓▒█░░ ▒░   ░  ░░░ ▒░ ░   ░ ▒░▒░▒░    ░ ▐░  ░░ ▒░ ░░ ▒▓ ░▒▓░ ░▀▀▒ ");
            Console.WriteLine("  ░   ░   ▒   ▒▒ ░░  ░      ░ ░ ░  ░     ░ ▒ ▒░    ░ ░░   ░ ░  ░  ░▒ ░ ▒░ ░  ░ ");
            Console.WriteLine("░ ░   ░   ░   ▒   ░      ░      ░      ░ ░ ░ ▒       ░░     ░     ░░   ░     ░ ");
        }
        
        public static void PrintTie()
        {
            SetBaseColor();
            Console.WriteLine("  _ _   _       _   _      ");
            Console.WriteLine(" (_) | ( )     | | (_)     ");
            Console.WriteLine("  _| |_|/ ___  | |_ _  ___ ");
            Console.WriteLine(" | | __| / __| | __| |/ _ \\");
            Console.WriteLine(" | | |_  \\__ \\ | |_| |  __/");
            Console.WriteLine(" |_|\\__| |___/  \\__|_|\\___|");
        }

        public static void HomePage()
        {
            SetBaseColor();
            Console.WriteLine("  __  ______  _            __        _______    ______        ______        ");
            Console.WriteLine(" / / / / / /_(_)_ _  ___ _/ /____   /_  __(_)__/_  __/__ ____/_  __/__  ___ ");
            Console.WriteLine("/ /_/ / / __/ /  ' \\/ _ `/ __/ -_)   / / / / __// / / _ `/ __// / / _ \\/ -_)");
            Console.WriteLine("\\____/_/\\__/_/_/_/_/\\_,_/\\__/\\__/   /_/ /_/\\__//_/  \\_,_/\\__//_/  \\___/\\__/ ");
            Console.WriteLine("\nWelcome to Tic Tac Toe, please press any key to start play");
            Console.ReadKey();
            Console.Clear();
        }


        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
        private static void SetBaseColor()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        private static void SetBaseTextPosition()
        {
            Console.SetCursorPosition(0, textOriginTop);
        }
    }
}