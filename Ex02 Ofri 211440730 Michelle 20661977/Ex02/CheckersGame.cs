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
            CheckersUI.WelcomeMessage();
            m_Player1 = new Player(CheckersUI.GetPlayerName());
            eCheckersBoardSize size= CheckersUI.GetBoardSize();
        }
    }
}
