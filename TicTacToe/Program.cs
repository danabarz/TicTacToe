using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Runtime.Serialization.Formatters;
using System.Threading;


namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            char[][] board = new char[10][];
            char player1 = 'X';
            char player2 = 'O';
            char currentPlayer = 'X';
            int z = 9;

            buildBoard();
            initBoard(board);

            do
            {
                if (currentPlayer == player1)
                {
                    z = humanTurn(board, player1);
                }
                else
                {
                    z = computerTurn(board, player2);
                }
                updateBoard(board);
                subGameOver(z, board, currentPlayer);
                currentPlayer = changePlayer(currentPlayer);
            }
            while (!subGameOver(9, board, currentPlayer));

            Console.Clear();
            if (currentPlayer == player1)
            {
                humanWon();
            }
            else
            {
                humanLose();
            }
        }
        private static char changePlayer(char currentPlayer)
        {
            if (currentPlayer == 'X')
            {
                currentPlayer = 'O';
                return currentPlayer;
            }
            else
            {
                currentPlayer = 'X';
                return currentPlayer;
            }
        }
        private static void initBoard(char[][] board)
        {

            for (int i = 0; i < 10; i++)
            {
                board[i] = new char[9];
                for (int j = 0; j < 9; j++)
                {
                    board[i][j] = ' ';

                }
            }
        }

        private static void updateBoard(char[][] board)
        {
            int big = 0;
            int small = 0;
            for (int x = 2; x <= 26; x += 12)
            {
                for (int y = 2; y <= 26; y += 12)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.SetCursorPosition(y + i * 4, x + j * 4);
                            Console.Write(board[big][small]);
                            small++;
                        }
                    }
                    small = 0;
                    big++;
                }
            }

        }
        private static void updateBigBoard(char[][] board)
        {
            int location = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(42 + j * 4, 14 + i * 4);
                    Console.Write(board[9][location]);

                }
            }

        }
        private static void buildBoard()
        {
            for (int i = 0; i <= 36; i += 4)
            {
                for (int j = 0; j <= 36; j++)
                {
                    if (i % 12 == 0 || j % 12 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(i, j);
                        Console.Write("#");
                        Console.SetCursorPosition(j, i);
                        Console.Write("#");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.SetCursorPosition(i, j);
                        Console.Write("#");
                        Console.SetCursorPosition(j, i);
                        Console.Write("#");
                    }
                }
            }
            for (int i = 40; i < 53; i += 4)
            {
                for (int j = 12; j < 25; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(i, j);
                    Console.Write("#");
                }
            }
            for (int i = 12; i < 25; i += 4)
            {
                for (int j = 40; j < 53; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(j, i);
                    Console.Write("#");
                }
            }
        }

        private static int humanTurn(char[][] board, char player1)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, 38);
            Console.Write("Human Turn");
            Console.SetCursorPosition(0, 39);
            Console.Write("Please enter sub board and location: ");
            string location = Console.ReadLine();
            string[] numbers = location.Split(new Char[] { ' ', ',', '.', '-', '\n', '\t' });
            string input = numbers[0];
            string input2 = numbers[1];
            int x, y;
            bool res = Int32.TryParse(input, out x);
            x--;
            bool res2 = Int32.TryParse(input2, out y);
            y--;
            while (!res || !res2 || board[x][y] != ' ' || x == 9)
            {
                Console.SetCursorPosition(0, 39);
                ClearCurrentConsoleLine();
                Console.WriteLine("Oops... Please enter valid input: ");
                location = Console.ReadLine();
                numbers = location.Split(new Char[] { ' ', ',', '.', '-', '\n', '\t' });
                input = numbers[0];
                input2 = numbers[1];
                res = Int32.TryParse(input, out x);
                x--;
                res2 = Int32.TryParse(input2, out y);
                y--;
            }
            Console.SetCursorPosition(0, 39);
            ClearCurrentConsoleLine();
            ClearCurrentConsoleLine();
            board[x][y] = player1;
            return x;
        }

        private static int computerTurn(char[][] board, char player2)
        {
            var rand = new Random();
            int x, y;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, 38);
            Console.Write("Computer Turn");
            do
            {
                x = rand.Next(9);
                y = rand.Next(9);
            }
            while (board[x][y] != ' ');
            board[x][y] = player2;
            Thread.Sleep(3000);
            Console.SetCursorPosition(0, 38);
            ClearCurrentConsoleLine();
            return x;
        }
        public static bool subGameOver(int z, char[][] board, char currentPlayer)
        {
            if (z == 9)
            {
                if ((board[z][0] == board[z][1] && board[z][0] == board[z][2] && board[z][0] != ' ') || (board[z][3] == board[z][4] && board[z][3] == board[z][5] && board[z][3] != ' ')
                || (board[z][6] == board[z][7] && board[z][6] == board[z][8] && board[z][6] != ' ') || (board[z][0] == board[z][3] && board[z][0] == board[z][6] && board[z][0] != ' ')
                || (board[z][1] == board[z][4] && board[z][7] == board[z][1] && board[z][6] != ' ') || (board[z][2] == board[z][5] && board[z][2] == board[z][8] && board[z][2] != ' ')
                || (board[z][0] == board[z][4] && board[z][0] == board[z][8] && board[z][0] != ' ') || (board[z][2] == board[z][4] && board[z][2] == board[z][6] && board[z][2] != ' '))
                {
                    return true;
                }
                else { return false; }
            }
            else
            {
                if ((board[z][0] == board[z][1] && board[z][0] == board[z][2] && board[z][0] != ' ') || (board[z][3] == board[z][4] && board[z][3] == board[z][5] && board[z][3] != ' ')
                || (board[z][6] == board[z][7] && board[z][6] == board[z][8] && board[z][6] != ' ') || (board[z][0] == board[z][3] && board[z][0] == board[z][6] && board[z][0] != ' ')
                || (board[z][1] == board[z][4] && board[z][7] == board[z][1] && board[z][6] != ' ') || (board[z][2] == board[z][5] && board[z][2] == board[z][8] && board[z][2] != ' ')
                || (board[z][0] == board[z][4] && board[z][0] == board[z][8] && board[z][0] != ' ') || (board[z][2] == board[z][4] && board[z][2] == board[z][6] && board[z][2] != ' '))
                {
                    board[9][z] = currentPlayer;
                    updateBigBoard(board);
                    return false;
                }
            }
            return false;
        }

        public static void humanWon()
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

        public static void humanLose()
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


        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}