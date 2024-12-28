using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    internal class CheckersGame
    {
        private CheckersUI m_UserInterface;
        private CheckersBoard m_GameBoard;
        private Player m_Player1;
        private Player m_Player2;
        bool gameFinished = false;
        internal void StartGame()
        {
           initGame();
        }

        private void initGame()
        {
            CheckersUI.WelcomeMessage();
            m_Player1 = new Player(CheckersUI.GetPlayerName(), ePlayerType.Human);
            m_GameBoard = new CheckersBoard(CheckersUI.GetBoardSize());
            initSecondPlayer();
            playGame();
        }

        private void initSecondPlayer()
        {
            ePlayerType secondPlayerType = CheckersUI.GetSecondPlayerType();

            string secondPlayerName;
            if (secondPlayerType == ePlayerType.Human)
            {
                secondPlayerName = CheckersUI.GetPlayerName();
            }
            else
            {
                secondPlayerName = "Computer";
            }

            m_Player2 = new Player(secondPlayerName, secondPlayerType);
        }
        private void playGame()
        {
            //while (!gameFinished)
            //{
                CheckersUI.PrintBoard(m_GameBoard.Board, m_GameBoard.Size);
                //m_UserInterface.PrintPlayerTurn(m_Player1);
                //m_UserInterface.GetPlayerMove(m_Player1);
                //m_UserInterface.PrintBoard(m_GameBoard);
               // m_UserInterface.PrintPlayerTurn(m_Player2);
               // m_UserInterface.GetPlayerMove(m_Player2);
            //}
        }
    }
}
