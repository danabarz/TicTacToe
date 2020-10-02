
namespace TicTacToe
{
    public class SummaryBoard : Board
    {
        public void OnSubBoardWinnerUpdated(object source, PlayerMoveEventArgs args)
        {
            UpdateBoard(args.BoardRow, args.BoardColumn, args.PlayerMarker);
        }
        public override void SetWinnerIfNeeded()
        {
            var winnerMarker = CheckIfGameOver();
            if (winnerMarker != null)
            {
                Winner = winnerMarker;
            }
        }
    }
}

