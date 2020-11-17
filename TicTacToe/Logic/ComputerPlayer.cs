namespace TicTacToe.Logic
{
    public class ComputerPlayer : Player
    {
        private readonly MinMax _miniMax;

        public ComputerPlayer() : base("Computer", PlayerMarker.O) 
        {
            _miniMax = new MinMax(Marker);
        }

        public PlayerMove ChooseMove(MainBoard mainBoard)
        {
            return _miniMax.FindBestMove(mainBoard);
        }
    }
}
