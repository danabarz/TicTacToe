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
    public class Program
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

                game.AskingGameType += gameView.OnSelectingGameType;
                game.AskingPlayersName += gameView.OnHumanVsHumanTypeSelected;
                game.PrintingBoards += boardView.OnBoardsInitialized;
                game.SummaryBoard.UpdatedBoardPieces += boardView.OnBoardUpdated;
                boardView.ChangedBoardColor += gameView.OnPrintedBoardView;

                gameView.PrintHomePage();

                GameType gameType = game.InitGameType();

                gameType.MangedPlayerTurns += gameView.OnPlayerPlayed;

                game.InitBoards();

                while (game.SummaryBoard.Winner == null)
                {
                    gameType.TurnManager(numberOfTurns);

                    gameType.CurrentPlayer.PrintingHumanOutput += gameView.OnHumanPlaying;
                    gameType.CurrentPlayer.ClearedSpecificLine += gameView.OnLocationEntered;

                    playerMove = gameType.CurrentPlayer.ChooseMove(game.SubBoards);

                    playerMove.Board.UpdatedBoardPieces += boardView.OnBoardUpdated;
                    playerMove.Board.UpdatedSubBoardWinner += game.SummaryBoard.OnSubBoardWinnerUpdated;

                    playerMove.Board.UpdateBoard(playerMove);
                    playerMove.Board.HaveWinner(game.SummaryBoard);
                    game.SummaryBoard.HaveWinner(game.SummaryBoard);

                    numberOfTurns++;
                }

                resetGame = gameView.PrintTheResult(game.SummaryBoard.Winner);
            }
;
        }
    }
}