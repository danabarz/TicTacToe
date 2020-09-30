
namespace TicTacToe
{
    public class SummaryBoard : Board
    {
        public SummaryBoard() : base()
        {
            Column = Dimensions;
        }

        public void OnSubBoardWinnerUpdated(object source, PlayerMoveEventArgs args)
        {
            UpdateBoard(args.PlayerMove);
        }
    }
}

