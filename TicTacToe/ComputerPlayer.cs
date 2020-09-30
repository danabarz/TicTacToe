
namespace TicTacToe
{
    public class ComputerPlayer : Player
    {
        public ComputerPlayer()
        {
            IdPlayer = PlayerMarker.O;
            PlayerName = "Computer";
        }

        public override PlayerMove ChooseMove(Game game)
        {
            var miniMax = new MiniMax();
            return miniMax.FindBestMove(game, IdPlayer);
        }
    }
}
