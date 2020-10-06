namespace TicTacToe.Logic
{
    public class ComputerPlayer : Player
    {
        public ComputerPlayer()
        {
            _idPlayer = PlayerMarker.O;
            _playerName = "Computer";
        }

        public PlayerMove ChooseMove(MainBoard mainBoard)
        {
            var miniMax = new MinMax();
            return miniMax.FindBestMove(mainBoard, _idPlayer);
        }
    }
}
