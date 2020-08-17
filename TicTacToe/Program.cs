using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Runtime.Serialization.Formatters;
using System.Threading;


namespace TicTacToe
{
    class Program
    {
        const int startPosition = 2;
        const int spaceBetween = 4;
        const int numOfBoards = 10;
        const int innerCells = 3;
        const int setLocation = 38;
        const int cellsNumber = innerCells * innerCells;
        const int distanceBoards = innerCells * spaceBetween;
        const char player1 = 'X';
        const char player2 = 'O';
        const char empty = ' ';
        static Random rand = new Random();
        static void Main(string[] args)
        {
            char[][] board = new char[numOfBoards][];
            char currentPlayer = player2;
            int z;

            HomePage();
            InitBoard(board);
            BuildBoard(board);

            while (!SubGameOver(cellsNumber, board, currentPlayer)) 
            {
                currentPlayer = ChangePlayer(currentPlayer);
                if (currentPlayer == player1)
                {
                    z = HumanTurn(board, player1);
                }
                else
                {
                    z = ComputerTurn(board, player2);
                }
                SubGameOver(z, board, currentPlayer);
                BuildBoard(board);
            }

            Console.Clear();
            if (Draw(cellsNumber, board))
            {
                DrawArt();
            }
            else if (currentPlayer == player1)
            {
                HumanWon();
            }
            else
            {
                HumanLose();
            }
        }
        private static char ChangePlayer(char currentPlayer)
        {
            if (currentPlayer == player1)
            {
                currentPlayer = player2;
                return currentPlayer;
            }
            else
            {
                currentPlayer = player1;
                return currentPlayer;
            }
        }
        private static void InitBoard(char[][] board)
        {

            for (int i = 0; i < numOfBoards; i++)
            {
                board[i] = new char[cellsNumber];
                for (int j = 0; j < cellsNumber; j++)
                {
                    board[i][j] = empty;

                }
            }
        }

        private static void PrintBoard(int x, int y, char[][] board, int smallBoard)
        {
            // Printing the game board
            Console.SetCursorPosition(x, y);
            for (int i = x; i <= x + distanceBoards; i += spaceBetween)
            {
                for (int j = y; j <= y + distanceBoards; j++)
                {
                    if (i <= distanceBoards * innerCells && i % distanceBoards == 0 || i <= distanceBoards * innerCells && j % distanceBoards == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    Console.SetCursorPosition(i, j);
                    Console.Write("#");

                }
            }

            for (int j = y; j <= y + distanceBoards; j += spaceBetween)
            {
                for (int i = x; i <= x + distanceBoards; i ++)
                {
                    if (i <= distanceBoards * innerCells && i % distanceBoards == 0 || i <= distanceBoards * innerCells && j % distanceBoards == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
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
            int z = 0;
            for (int j = y + startPosition; j <= y + distanceBoards - startPosition; j += spaceBetween)
            {
                for (int i = x + startPosition; i <= x + distanceBoards - startPosition; i += spaceBetween)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(i, j);
                    Console.Write(board[smallBoard][z]);
                    z++;                  
                }
            }

        }

        private static void BuildBoard(char[][] board)
        {
            PrintBoard(0, 0, board, 0);
            PrintBoard(distanceBoards, 0, board, 1);
            PrintBoard(distanceBoards * 2, 0, board, 2);
            PrintBoard(0, distanceBoards, board, 3);
            PrintBoard(distanceBoards, distanceBoards, board, 4);
            PrintBoard(distanceBoards * 2, distanceBoards, board, 5);
            PrintBoard(0, distanceBoards * 2, board, 6);
            PrintBoard(distanceBoards, distanceBoards * 2, board, 7);
            PrintBoard(distanceBoards * 2, distanceBoards * 2, board, 8);
            PrintBoard(distanceBoards * innerCells + spaceBetween, distanceBoards, board, 9);
        }   
        private static int HumanTurn(char[][] board, char player1)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, setLocation);
            Console.Write("Human Turn");
            Console.SetCursorPosition(0, setLocation + 1);
            Console.Write("Please enter sub board and location: ");
            string location = Console.ReadLine();
            string[] numbers = location.Split(new char[] { ' ', ',', '.'});
            string input = numbers[0];
            string input2 = numbers[1];
            bool res = int.TryParse(input, out int x);
            x--;
            bool res2 = int.TryParse(input2, out int y);
            y--;
            while (!res || !res2 || board[x][y] != ' ' || x >= cellsNumber || y >= cellsNumber)
            {
                Console.SetCursorPosition(0, setLocation + 1);
                ClearCurrentConsoleLine();
                Console.Write("Oops... Please enter valid input: ");
                location = Console.ReadLine();
                numbers = location.Split(new char[] { ' ', ',', '.'});
                input = numbers[0];
                input2 = numbers[1];
                res = int.TryParse(input, out x);
                x--;
                res2 = int.TryParse(input2, out y);
                y--;
            }
            Console.SetCursorPosition(0, setLocation + 1);
            ClearCurrentConsoleLine();
            ClearCurrentConsoleLine();
            board[x][y] = player1;
            return x;
        }
        private static int ComputerTurn(char[][] board, char player2)
        {
            
            // crate collection list that contain all the empty spaces and the random choose from that
            int x, y;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, setLocation);
            Console.Write("Computer Turn");
            do
            {
                x = rand.Next(cellsNumber);
                y = rand.Next(cellsNumber);
            }
            while (board[x][y] != empty);
            board[x][y] = player2;
            Console.SetCursorPosition(0, setLocation);
            ClearCurrentConsoleLine();
            return x;
        }

        private static SortedList<int, int> CreateList(char[][] board)
        {
            SortedList<int, int> number = new SortedList<int, int>();
            for (int i = 0; i < cellsNumber; i++)
            {
                for (int j = 0; j < cellsNumber; j++)
                {
                    if (board[i][j] == empty)
                    {
                        number.Add(i, j);
                    }
                }
            }
            return number;
        }
        public static bool SubGameOver(int z, char[][] board, char currentPlayer)
        {
            if (z == cellsNumber)
            {
                if (Horizontal(z, board) || Vertical(z, board) || Diagonal(z, board) || Draw(z, board))
                {
                    return true;
                }
                else { return false; }
            }
            else
            {
                if ((Horizontal(z, board) || Vertical(z, board) || Diagonal(z, board)) && board[cellsNumber][z] == empty)
                {
                    board[cellsNumber][z] = currentPlayer;
                    return false;
                }
                else if (Draw(z, board) && board[cellsNumber][z] == empty)
                {
                    board[cellsNumber][z] = '-';
                    return false;
                }
            }
            return false;
        }

        public static bool Horizontal(int z, char[][] board)
        {
            if ((board[z][0] == board[z][1] && board[z][0] == board[z][2] && board[z][0] != empty) || (board[z][3] == board[z][4] && board[z][3] == board[z][5] && board[z][3] != empty)
            || (board[z][6] == board[z][7] && board[z][6] == board[z][8] && board[z][6] != empty))
            {
                return true;
            }
            return false;
        }

        public static bool Vertical(int z, char[][] board)
        {
            if ((board[z][0] == board[z][3] && board[z][0] == board[z][6] && board[z][0] != empty) || (board[z][1] == board[z][4] && board[z][1] == board[z][7] && board[z][1] != empty)
            || (board[z][2] == board[z][5] && board[z][2] == board[z][8] && board[z][2] != empty))
            {
                return true;
            }
            return false;
        }

        public static bool Diagonal(int z, char[][] board)
        {
            if ((board[z][0] == board[z][4] && board[z][0] == board[z][8] && board[z][0] != empty) || (board[z][2] == board[z][4] && board[z][2] == board[z][6] && board[z][2] != empty))
            {
                return true;
            }
            return false;
        }

        public static bool Draw(int z, char[][] board)
        {
            int i = 0;
            while (i < cellsNumber)
            {
                if (board[z][i] != empty)
                {
                    i++;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public static void HumanWon()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("     )                                   ____ ");
            Console.WriteLine("  ( /(             (  (                 |   / ");
            Console.WriteLine("  )\\())       (    )\\))(   '            |  /  ");
            Console.WriteLine(" ((_)\\  (    ))\\  ((_)()\\ )  (    (     | /   ");
            Console.WriteLine("__ ((_) )\\  /((_) _(())\\_)() )\\   )\\ )  |/    ");
            Console.WriteLine("\\ \\ / /((_)(_))(  \\ \\((_)/ /((_) _(_/( (      ");
            Console.WriteLine(" \\ V // _ \\| || |  \\ \\/\\/ // _ \\| ' \\)))\\     ");
            Console.WriteLine("  |_| \\___/ \\_,_|   \\_/\\_/ \\___/|_||_|((_)    ");
        }

        public static void HumanLose()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  ▄████  ▄▄▄       ███▄ ▄███▓▓█████     ▒█████   ██▒   █▓▓█████  ██▀███   ▐██▌ ");
            Console.WriteLine(" ██▒ ▀█▒▒████▄    ▓██▒▀█▀ ██▒▓█   ▀    ▒██▒  ██▒▓██░   █▒▓█   ▀ ▓██ ▒ ██▒ ▐██▌ ");
            Console.WriteLine("▒██░▄▄▄░▒██  ▀█▄  ▓██    ▓██░▒███      ▒██░  ██▒ ▓██  █▒░▒███   ▓██ ░▄█ ▒ ▐██▌ ");
            Console.WriteLine("░▓█  ██▓░██▄▄▄▄██ ▒██    ▒██ ▒▓█  ▄    ▒██   ██░  ▒██ █░░▒▓█  ▄ ▒██▀▀█▄   ▓██▒ ");
            Console.WriteLine("░▒▓███▀▒ ▓█   ▓██▒▒██▒   ░██▒░▒████▒   ░ ████▓▒░   ▒▀█░  ░▒████▒░██▓ ▒██▒ ▒▄▄  ");
            Console.WriteLine(" ░▒   ▒  ▒▒   ▓▒█░░ ▒░   ░  ░░░ ▒░ ░   ░ ▒░▒░▒░    ░ ▐░  ░░ ▒░ ░░ ▒▓ ░▒▓░ ░▀▀▒ ");
            Console.WriteLine("  ░   ░   ▒   ▒▒ ░░  ░      ░ ░ ░  ░     ░ ▒ ▒░    ░ ░░   ░ ░  ░  ░▒ ░ ▒░ ░  ░ ");
            Console.WriteLine("░ ░   ░   ░   ▒   ░      ░      ░      ░ ░ ░ ▒       ░░     ░     ░░   ░     ░ ");
        }
        
        public static void DrawArt()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  _ _   _           _                    ");
            Console.WriteLine(" (_) | ( )         | |                   ");
            Console.WriteLine("  _| |_|/ ___    __| |_ __ __ ___      __");
            Console.WriteLine(" | | __| / __|  / _` | '__/ _` \\ \\ /\\ / /");
            Console.WriteLine(" | | |_  \\__ \\ | (_| | | | (_| |\\ V  V / ");
            Console.WriteLine(" |_|\\__| |___/  \\__,_|_|  \\__,_| \\_/\\_/  ");
        }

        public static void HomePage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  __  ______  _            __        _______    ______        ______        ");
            Console.WriteLine(" / / / / / /_(_)_ _  ___ _/ /____   /_  __(_)__/_  __/__ ____/_  __/__  ___ ");
            Console.WriteLine("/ /_/ / / __/ /  ' \\/ _ `/ __/ -_)   / / / / __// / / _ `/ __// / / _ \\/ -_)");
            Console.WriteLine("\\____/_/\\__/_/_/_/_/\\_,_/\\__/\\__/   /_/ /_/\\__//_/  \\_,_/\\__//_/  \\___/\\__/ ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nWelcome to Tic Tac Toe, please press any to begin");
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
    }
}