using System;

namespace TicTacToe
{
    public class GameView
    {
        public const int TextOriginTop = 38;

        public void OnPlayerPlayed(object source, PlayerEventArgs args)
        {
            if (args.CountTimes != 0)
            {
                ClearCurrentConsoleLine(Console.CursorLeft, TextOriginTop);
            }
            SetBaseTextPosition();
            SetBaseColor();
            Console.WriteLine(args.Player.PlayerName + " Turn");
        }

        public void OnHumanPlaying(object source, IntEventArgs args)
        {
            Console.SetCursorPosition(Console.CursorLeft, TextOriginTop + 1);
            if (args.CountTimes == 1)
            {
                Console.WriteLine("Please enter sub board and cell: ");
            }
            else
            {
                Console.WriteLine("Oops... Something went wrong, please try again: ");
            }
        }

        public void OnLocationEntered(object source, EventArgs args)
        {
            ClearCurrentConsoleLine(Console.CursorLeft, Console.CursorTop - 1);
            ClearCurrentConsoleLine(Console.CursorLeft, TextOriginTop + 1);
        }

        public void OnSelectingGameType(object source, EventArgs args)
        {
            Console.WriteLine("\nPlease select game type: \n1) Human Vs Human \n2) Human Vs Computer ");
        }

        public void OnHumanVsHumanTypeSelected(object source, IntEventArgs args)
        {
            Console.WriteLine("\nPlayer {0} - Please write your name: ", args.CountTimes);
        }

        public void OnPrintedBoardView(object source, EventArgs args)
        {
            SetBaseColor();
        }

        public bool PrintTheResult(PlayerMarker? playerMarker)
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

            return ResetGame();
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

        public void PrintHomePage()
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


        private bool ResetGame()
        {
            SetBaseTextPosition();
            Console.WriteLine("\nPlease write \"start\" to reset the game or \"exit\" to end it");
            return (Console.ReadLine() == "start");
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
