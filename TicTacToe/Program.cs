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
        const int startPosition = 2;
        const int spaceBetweenCells = 4;
        const int numOfBoards = 10;
        const int boardDimensions = 3;
        const int textOriginTop = 38;
        const int boardCellNumber = boardDimensions * boardDimensions;
        const int distanceBetweenBoards = boardDimensions * spaceBetweenCells;
        const char player1Marker = 'X';
        const char player2Marker = 'O';
        const char emptyMarker = ' ';
        const char tieMarker = '-';
        static readonly Random rand = new Random();
        static readonly char[][] gameBoards = new char[numOfBoards][];

        static void Main(string[] args)
        {
            char currentPlayer = player2Marker;
            int subBoardIndex;

            HomePage();
            InitBoard();
            PrintSubBoards();

            while (!IsSubGameOver(boardCellNumber, currentPlayer)) 
            {
                currentPlayer = SwitchPlayer(currentPlayer);
                subBoardIndex = (currentPlayer == player1Marker) ? HumanChooseSubBoardAndCell() : ComputerChooseSubBoardAndCell();
                // todo: check how to use the return
                IsSubGameOver(subBoardIndex, currentPlayer);
                PrintSubBoards();
            }

            Console.Clear();
            if (IsTieForSubGame(boardCellNumber))
            {
                TieArt();
            }
            else if (currentPlayer == player1Marker)
            {
                PrintWon();
            }
            else
            {
                PrintLose();
            }
        }
        private static char SwitchPlayer(char currentPlayer)
        {
            //todo: cyclic arrays- eldar
             return currentPlayer = (currentPlayer == player1Marker) ? player2Marker : player1Marker;
        }
        private static void InitBoard()
        {

            for (int i = 0; i < numOfBoards; i++)
            {
                gameBoards[i] = new char[boardCellNumber];
                for (int j = 0; j < boardCellNumber; j++)
                {
                    gameBoards[i][j] = emptyMarker;

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
            for (int j = boardOriginTop + startPosition; j <= boardOriginTop + distanceBetweenBoards - startPosition; j += spaceBetweenCells)
            {
                for (int i = boardOriginLeft + startPosition; i <= boardOriginLeft + distanceBetweenBoards - startPosition; i += spaceBetweenCells)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(i, j);
                    Console.Write(gameBoards[subBoard][cellInBoard]);
                    cellInBoard++;                  
                }
            }
        }

        private static void PrintSubBoards()
        {
            int boardIndex = 0;
            PrintBoardAndPieces(0, 0, boardIndex++);
            PrintBoardAndPieces(distanceBetweenBoards, 0, boardIndex++);
            PrintBoardAndPieces(distanceBetweenBoards * startPosition, 0, boardIndex++);
            PrintBoardAndPieces(0, distanceBetweenBoards, boardIndex++);
            PrintBoardAndPieces(distanceBetweenBoards, distanceBetweenBoards, boardIndex++);
            PrintBoardAndPieces(distanceBetweenBoards * startPosition, distanceBetweenBoards, boardIndex++);
            PrintBoardAndPieces(0, distanceBetweenBoards * startPosition, boardIndex++);
            PrintBoardAndPieces(distanceBetweenBoards, distanceBetweenBoards * startPosition, boardIndex++);
            PrintBoardAndPieces(distanceBetweenBoards * startPosition, distanceBetweenBoards * startPosition, boardIndex++);
            PrintBoardAndPieces(distanceBetweenBoards * boardDimensions + spaceBetweenCells, distanceBetweenBoards, boardIndex);
        }   
        private static int HumanChooseSubBoardAndCell()
        {
            SetBaseTextPosition();
            SetBaseColor();
            Console.Write("Human Turn");
            Console.SetCursorPosition(0, textOriginTop + 1);
            Console.Write("Please enter sub board and location: ");
            string subBoardAndCellInput = Console.ReadLine();
            var chosenAndValidPosition = SplitStringAndParse(subBoardAndCellInput);
            while (chosenAndValidPosition.Item1 == -1 || chosenAndValidPosition.Item2 == -1)
            {
                Console.SetCursorPosition(0, textOriginTop + 1);
                ClearCurrentConsoleLine();
                Console.Write("Oops... Please enter valid input: ");
                subBoardAndCellInput = Console.ReadLine();
                chosenAndValidPosition = SplitStringAndParse(subBoardAndCellInput);
            }
            Console.SetCursorPosition(0, textOriginTop + 1);
            ClearCurrentConsoleLine();
            SetBaseTextPosition();
            ClearCurrentConsoleLine();
            gameBoards[chosenAndValidPosition.Item1][chosenAndValidPosition.Item2] = player1Marker;
            return chosenAndValidPosition.Item1;
            
            Tuple<int, int> SplitStringAndParse(string subBoardAndCellInput)
            {
                string[] numbers = subBoardAndCellInput.Split(new char[] { ' ', ',', '.' });
                string input = numbers[0];
                string input2 = numbers[1];
                bool res = int.TryParse(input, out int x);
                bool res2 = int.TryParse(input2, out int y);
                if (res && res2 && x <= boardCellNumber && y <= boardCellNumber && gameBoards[--x][--y] == emptyMarker)
                {
                    return Tuple.Create(x, y);
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
            gameBoards[subBoardNumber][cellNumber] = player2Marker;
            Thread.Sleep(1000);
            ClearCurrentConsoleLine();
            return subBoardNumber;
        }

        private static List<Tuple<int, int>> FindEmptyCells()
        {
            var numbers = new List<Tuple<int, int>>();
            for (int i = 0; i < boardCellNumber; i++)
            {
                char[] empty = gameBoards[i];
                int[] matchPositions = empty.Select((value, index) => value == emptyMarker ? index : -1).Where( index => index != -1).ToArray();
                foreach(int x in matchPositions)
                {
                    numbers.Add(new Tuple<int, int>(i, x));
                }
            }
            return numbers;
        }

        //todo : this meyhos should return the player that won/tie/or that the sub game is not over- use enums
        //todo: create IsFullGameOver- think how to not include the final board in the same array as the other
        public static bool IsSubGameOver(int subBoardIndex, char currentPlayer)
        {
            //make another array to the final/big board- not include this one!!
            if (subBoardIndex == boardCellNumber)
            {
                return (IsHorizontalWinForSubGame(subBoardIndex) || IsVerticalWinForSubGame(subBoardIndex) || IsDiagonalWinForSubGame(subBoardIndex) || IsTieForSubGame(subBoardIndex));
            }
            // todo: check which player won a sub game and set the summary board accordingly
            if ((IsHorizontalWinForSubGame(subBoardIndex) || IsVerticalWinForSubGame(subBoardIndex) || IsDiagonalWinForSubGame(subBoardIndex)) && gameBoards[boardCellNumber][subBoardIndex] == emptyMarker)
            {
                gameBoards[boardCellNumber][subBoardIndex] = currentPlayer;
                return false;
            }
            else if (IsTieForSubGame(subBoardIndex) && gameBoards[boardCellNumber][subBoardIndex] == emptyMarker)
            {
                gameBoards[boardCellNumber][subBoardIndex] = tieMarker;
                return false;
            }
            return false;
        }


        //create a delegate to all the win's check!
        public static bool IsHorizontalWinForSubGame(int subBoardIndex)
        {
            if ((gameBoards[subBoardIndex][0] == gameBoards[subBoardIndex][1] && gameBoards[subBoardIndex][0] == gameBoards[subBoardIndex][2] && gameBoards[subBoardIndex][0] != emptyMarker)
            || (gameBoards[subBoardIndex][3] == gameBoards[subBoardIndex][4] && gameBoards[subBoardIndex][3] == gameBoards[subBoardIndex][5] && gameBoards[subBoardIndex][3] != emptyMarker)
            || (gameBoards[subBoardIndex][6] == gameBoards[subBoardIndex][7] && gameBoards[subBoardIndex][6] == gameBoards[subBoardIndex][8] && gameBoards[subBoardIndex][6] != emptyMarker))
            {
                return true;
            }
            return false;
        }

        public static bool IsVerticalWinForSubGame(int subBoardIndex)
        {
            if ((gameBoards[subBoardIndex][0] == gameBoards[subBoardIndex][3] && gameBoards[subBoardIndex][0] == gameBoards[subBoardIndex][6] && gameBoards[subBoardIndex][0] != emptyMarker)
                || (gameBoards[subBoardIndex][1] == gameBoards[subBoardIndex][4] && gameBoards[subBoardIndex][1] == gameBoards[subBoardIndex][7] && gameBoards[subBoardIndex][1] != emptyMarker)
            || (gameBoards[subBoardIndex][2] == gameBoards[subBoardIndex][5] && gameBoards[subBoardIndex][2] == gameBoards[subBoardIndex][8] && gameBoards[subBoardIndex][2] != emptyMarker))
            {
                return true;
            }
            return false;
        }

        public static bool IsDiagonalWinForSubGame(int subBoardIndex)
        {
            if ((gameBoards[subBoardIndex][0] == gameBoards[subBoardIndex][4] && gameBoards[subBoardIndex][0] == gameBoards[subBoardIndex][8] && gameBoards[subBoardIndex][0] != emptyMarker)
            || (gameBoards[subBoardIndex][2] == gameBoards[subBoardIndex][4] && gameBoards[subBoardIndex][2] == gameBoards[subBoardIndex][6] && gameBoards[subBoardIndex][2] != emptyMarker))
            {
                return true;
            }
            return false;
        }


        public static bool IsTieForSubGame(int subBoardIndex)
        {
            for (int i = 0; i < boardCellNumber; i++)
            {
                char [] subBoard = gameBoards[subBoardIndex];
                char emptyPosition = Array.Find(subBoard, cell => cell == emptyMarker);
                if (emptyPosition == emptyMarker)
                {
                    return false;
                }
            }
            return true;
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
        
        public static void TieArt()
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