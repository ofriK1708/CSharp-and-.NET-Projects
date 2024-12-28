
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Cryptography;

namespace Ex02
{
    internal class CheckersGame
    {
        private CheckersBoard m_GameBoard;
        private Player m_Player1;
        private Player m_Player2;
        private bool m_GameFinished = false;
        private const string m_ComputerPlayerName = "Computer";
        private Player m_ActivePlayer;
        private int m_GameNumber = 1;
        bool m_isGameQuitedByPlayer = false;

        internal void StartGame()
        {
           initGame();
           playGame();
        }

        private void initGame()
        {
            CheckersUI.PrintWelcomeMessage();
            m_Player1 = new Player(CheckersUI.GetPlayerName(), ePlayerType.Human, eCheckersBoardPiece.FirstPlayerRegularPiece);
            m_GameBoard = new CheckersBoard(CheckersUI.GetBoardSize());
            initSecondPlayer();
            m_ActivePlayer = m_Player1; 
            CheckersUI.PrintStartGameMessage(m_GameNumber, m_Player1.Name, m_Player2.Name);
            CheckersUI.PrintBoard(m_GameBoard.Board, m_GameBoard.Size);
            CheckersUI.PrintPlayerTurn(m_ActivePlayer);
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
                secondPlayerName = m_ComputerPlayerName;
            }

            m_Player2 = new Player(secondPlayerName, secondPlayerType, eCheckersBoardPiece.SecondPlayerRegularPiece);
        }

        private void playGame()
        {
            m_isGameQuitedByPlayer = false;
            m_GameFinished = false;
            List<CheckersBoardPosition> validMoves;
            bool mustEat = false;

            while (!m_GameFinished && !m_isGameQuitedByPlayer)
            {
                if (!mustEat)
                {
                    validMoves = getAllValidMoves(m_ActivePlayer);

                    if (validMoves.Count <= 0)
                    {
                        //oponent wins
                    }
                }

                CheckersBoardPosition? move = CheckersUI.GetMoveFromPlayer(out m_isGameQuitedByPlayer);
                if (m_isGameQuitedByPlayer)
                {
                    // user that quited lost
                    return;
                }

                // validate move part of valid moves 

                //play turn - refactor board
                bool eatOpenetsPiece = m_GameBoard.playMove();
                if (eatOpenetsPiece)
                {
                    // validate there is more to eat
                    validMoves = getValidMoveToEatFromPostions();

                    if(validMoves.Count > 0)
                    {
                        mustEat = true;
                    } else
                    {
                        mustEat = false;
                    }
                }

                CheckersUI.PrintBoard(m_GameBoard.Board, m_GameBoard.Size);
                CheckersUI.PrintPlayedMove(move.GetValueOrDefault(), m_ActivePlayer);

                if (!mustEat)
                {
                    //switch active player
                }
               
                CheckersUI.PrintPlayerTurn(m_ActivePlayer);
            }
        }
        private bool validateMove(CheckersBoardPosition i_Move, Player i_Player)
        {
            Array<checkersMove> 
        }
    }
}
