namespace TicTacToe.Logic
{
    public abstract class Player
    {
        public PlayerMarker IdPlayer { get; protected set; }
        public string? PlayerName { get; protected set; }

        public PlayerMove ChooseMove(MainBoard mainBoard)
        {
            var miniMax = new MinMax();
            return miniMax.FindBestMove(mainBoard, IdPlayer);
        }
    }
}
