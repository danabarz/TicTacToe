
using TicTacToe.Presentation;

namespace TicTacToe
{
    public class Program
    {
        static void Main(string[] args)
        {
            var gameView = new GameView();
            gameView.RunGame();
        }
    }
}