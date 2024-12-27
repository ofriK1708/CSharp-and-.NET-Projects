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

        internal void StartGame()
        {
           initGame();
        
        }

        private void initGame()
        {
            CheckersUI.WelcomeMessage();
            m_Player1 = new Player(CheckersUI.GetPlayerName(), ePlayerType.Human);
            eCheckersBoardSize size = CheckersUI.GetBoardSize(); //to do - create board
            initSecondPlayer();
            
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
    }
}
