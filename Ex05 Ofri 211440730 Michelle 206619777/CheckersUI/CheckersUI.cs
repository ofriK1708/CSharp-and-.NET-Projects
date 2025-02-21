using System;
using System.Linq;
using System.Windows.Forms;
using CheckersLogic;

namespace CheckersUI
{
    internal class CheckersUI
    {
        private GameSettingsForm m_SettingsForm;
        private GameBoardForm m_GameBoardForm;
        private CheckersGame m_CheckersGame;
        private int m_GameNumber = 1;
        private bool m_GameQuittedByPlayer = false;
        private bool m_GameFinished = false;
        private readonly Random r_RandomGenerator = new Random();

        internal void StartGame()
        {
            m_SettingsForm = new GameSettingsForm();
            Player player1 = new Player(m_SettingsForm.PlayerOneName, ePlayerType.Human, eCheckersPieceType.XPiece);
            Player player2 = initSecondPlayer(m_SettingsForm.IsPlayerTwoActive, m_SettingsForm.PlayerTwoName);
            m_CheckersGame = new CheckersGame(player1, player2, m_SettingsForm.BoardSize);
            m_GameBoardForm = new GameBoardForm(player1.Name,player2.Name, m_SettingsForm.BoardSize);

            if (m_GameBoardForm.DialogResult != DialogResult.Cancel)
            {
                m_CheckersGame.AddBoardResetListener(m_GameBoardForm.Game_BoardReset);
                m_CheckersGame.ResetGame();

                //todo - initialize events

                //m_GameNumber++;
                m_GameBoardForm.ShowDialog();
            }

            // bool anotherGame = getFromUserIsContinueToAnotherGame();
            // while (anotherGame)
            // {
            //     playGame();
            //     anotherGame = getFromUserIsContinueToAnotherGame();
            // }
        }

        private Player initSecondPlayer(bool i_SettingsFormIsPlayerTwoActive, string i_SettingsFormPlayerTwoName)
        {
            ePlayerType secondPlayerType = i_SettingsFormIsPlayerTwoActive ? ePlayerType.Human : ePlayerType.Computer;
            return new Player(i_SettingsFormPlayerTwoName, secondPlayerType, eCheckersPieceType.OPiece);
        }

        // private void playGame()
        // {
        //     m_GameQuittedByPlayer = false;
        //     m_GameFinished = false;
        //
        //     while (!m_GameFinished && !m_GameQuittedByPlayer)
        //     {
        //         m_CheckersGame.handleGameStateBeforeNextMove();
        //         if (m_CheckersGame.IsStalemate)
        //         {
        //             printStalemateMessage(m_CheckersGame.Player1, m_CheckersGame.Player2);
        //             m_GameFinished = true;
        //             break;
        //         }
        //
        //         if (m_CheckersGame.IsActivePlayerWon)
        //         {
        //             printWinMessage(m_CheckersGame.ActivePlayer, m_CheckersGame.Player1, m_CheckersGame.Player2);
        //             m_GameFinished = true;
        //             break;
        //         }
        //
        //         printPlayerTurn(m_CheckersGame.ActivePlayer);
        //         CheckersBoardMove move = getNextValidMoveOrQuitGame();
        //         if (m_GameQuittedByPlayer)
        //         {
        //             m_CheckersGame.HandleOpponentWin();
        //             printWinMessage(m_CheckersGame.ActivePlayer, m_CheckersGame.Player1, m_CheckersGame.Player2);
        //             m_GameFinished = true;
        //             break;
        //         }
        //
        //         m_CheckersGame.playMove(move);
        //         // printBoard(m_CheckersGame.GameBoard);
        //         printPlayedMove(move, m_CheckersGame.ActivePlayer);
        //         m_CheckersGame.handleGameStateAfterMove();
        //         if (m_CheckersGame.IsActivePlayerWon)
        //         {
        //             printWinMessage(m_CheckersGame.ActivePlayer, m_CheckersGame.Player1, m_CheckersGame.Player2);
        //             m_GameFinished = true;
        //         }
        //     }
        // }

        // private CheckersBoardMove getNextValidMoveOrQuitGame()
        // {
        //     CheckersBoardMove move;
        //
        //     if (m_CheckersGame.ActivePlayer.PlayerType == ePlayerType.Computer)
        //     {
        //         printComputerMessage();
        //         uint randomIndex = (uint)r_RandomGenerator.Next(m_CheckersGame.ValidMoves.Count);
        //         move = m_CheckersGame.ValidMoves[(int)randomIndex];
        //     }
        //     else
        //     {
        //         getMoveOrQuitGameByPlayer(out move);
        //         while (!m_GameQuittedByPlayer && !m_CheckersGame.CheckMovePartOfValidMoves(move))
        //         {
        //             printMoveInvalid();
        //             getMoveOrQuitGameByPlayer(out move);
        //         }
        //     }
        //
        //     return move;
        // }

        private void printPlayerTurn(Player i_Player)
        {
            Console.WriteLine("{0}'s turn ({1}):", i_Player.Name, i_Player.PieceType);
        }

        // private void getMoveOrQuitGameByPlayer(out CheckersBoardMove o_Move)
        // {
        //     Console.WriteLine("Enter move");
        //     string moveInput = getUserInput();
        //     o_Move = new CheckersBoardMove();
        //     while (moveInput != k_QuitChar && !isValidMoveInput(moveInput))
        //     {
        //         Console.WriteLine(
        //             "Invalid move input!, move should have the be in the format ROWCol>ROWCol, for example Fc>Fb");
        //         moveInput = getUserInput();
        //     }
        //
        //     if (moveInput == k_QuitChar)
        //     {
        //         m_GameQuittedByPlayer = true;
        //     }
        //     else
        //     {
        //         o_Move.SetMove(moveInput);
        //     }
        // }

        private void printComputerMessage()
        {
            Console.WriteLine("Computer’s Turn (press ‘enter’ to see it’s move)");
            Console.ReadLine();
        }

        private void printPlayedMove(CheckersBoardMove i_Move, Player i_Player)
        {
            Console.WriteLine("{0}'s move was ({1}): {2}{3}>{4}{5}",
                i_Player.Name, i_Player.PieceType, (char)(i_Move.From.Row + 'A'), (char)(i_Move.From.Column + 'a'),
                (char)(i_Move.To.Row + 'A'), (char)(i_Move.To.Column + 'a'));
        }

        private void printMoveInvalid()
        {
            Console.WriteLine("Move is not valid! please try different move");
        }

        private string getUserInput()
        {
            string userInput = Console.ReadLine();

            while (String.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Input must not be empty, please try again");
                userInput = Console.ReadLine();
            }

            return userInput.Trim();
        }
        

        private void printWinMessage(Player i_ActivePlayer, Player i_Player1, Player i_Player2)
        {
            Console.WriteLine("{0} Won!!", i_ActivePlayer.Name);
            printScore(i_Player1, i_Player2);
        }

        private void printScore(Player i_Player1, Player i_Player2)
        {
            Console.WriteLine("{0}'s score is {1}, {2}'s score is {3}", i_Player1.Name, i_Player1.Score, i_Player2.Name,
                i_Player2.Score);
        }

        private void printStalemateMessage(Player i_Player1, Player i_Player2)
        {
            Console.WriteLine("Stalemate! No one won :(");
            printScore(i_Player1, i_Player2);
        }

        private bool getFromUserIsContinueToAnotherGame()
        {
            Console.WriteLine("Would you like to play another game? yes - press 1, no - press 0");
            string inputFromUser = getUserInput();
            bool anotherGame = false;

            while (inputFromUser != "1" && inputFromUser != "0")
            {
                Console.WriteLine("Invalid input, Would you like to play another game? yes - press 1, no - press 0");
                inputFromUser = getUserInput();
            }

            if (inputFromUser == "1")
            {
                anotherGame = true;
            }

            return anotherGame;
        }
    }
}