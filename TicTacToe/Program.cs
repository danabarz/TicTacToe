using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography.X509Certificates;
using System.Threading;


namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            bool resetGame = true;
            while (resetGame)
            {
                int numberOfTurns = 0;
                GameView gameView = new GameView();
                Game game = new Game();
                BoardView boardView = new BoardView();
                PlayerMove playerMove;

                game.AskGameType += gameView.OnSelectingGameType;
                game.AskPlayersName += gameView.OnHumanVsHumanTypeSelected;
                game.PrintBoards += boardView.OnBoardsInitialized;
                game.SummaryBoard.UpdateBoardPieces += boardView.OnBoardUpdated;
                boardView.SetColorBoard += gameView.OnPrintBoardView;

                gameView.PrintHomePage();

                GameType gameType = game.InitGameType();

                gameType.MangePlayerTurns += gameView.OnPlayerPlayed;

                game.InitBoards();

                while (game.SummaryBoard.Owner == null)
                {
                    gameType.TurnManager(numberOfTurns);

                    gameType.CurrentPlayer.PrintHumanOutput += gameView.OnHumanPlaying;
                    gameType.CurrentPlayer.ClearSpecificLine += gameView.OnLocationEntered;

                    playerMove = gameType.CurrentPlayer.ChooseMove(game.SubBoards);

                    playerMove.Board.UpdateBoardPieces += boardView.OnBoardUpdated;
                    playerMove.Board.SubBoardHaveOwner += game.SummaryBoard.OnSubBoardHaveOwner;

                    playerMove.Board.UpdateBoard(playerMove);
                    playerMove.Board.HaveOwner(game.SummaryBoard);
                    game.SummaryBoard.HaveOwner(game.SummaryBoard);

                    numberOfTurns++;
                }

                resetGame = gameView.PrintTheResult(game.SummaryBoard.Owner);
            }
;
        }
    }
}