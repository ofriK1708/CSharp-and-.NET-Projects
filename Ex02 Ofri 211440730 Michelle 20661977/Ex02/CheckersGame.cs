
using System;
using System.Collections.Generic;

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
        bool m_GameQuitedByPlayer = false;

        internal void StartGame()
        {
           initGame();
           playGame();
        }

        private void initGame()
        {
            CheckersUI.PrintWelcomeMessage();
            m_Player1 = new Player(CheckersUI.GetPlayerName(), ePlayerType.Human, eCheckersBoardPiece.XPiece);
            m_GameBoard = new CheckersBoard(CheckersUI.GetBoardSize());
            initSecondPlayer();
            m_ActivePlayer = m_Player1; 
            CheckersUI.PrintStartGameMessage(m_GameNumber, m_Player1.Name, m_Player2.Name);
            CheckersUI.PrintBoard(m_GameBoard.Board, m_GameBoard.Size);
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

            m_Player2 = new Player(secondPlayerName, secondPlayerType, eCheckersBoardPiece.OPiece);
        }

        private void playGame()
        {
            m_GameQuitedByPlayer = false;
            m_GameFinished = false;
            List<CheckersBoardMove> validMoves = new List<CheckersBoardMove>();
            bool continueTurnForCurrentPlayer = false;

            while (!m_GameFinished && !m_GameQuitedByPlayer)
            {
                if (!continueTurnForCurrentPlayer)
                {
                    validMoves = m_GameBoard.getAllValidMoves(m_ActivePlayer);
                    if (validMoves.Count <= 0)
                    {
                        //player loose 
                        m_GameFinished = true;
                        break;
                    }
                }

                CheckersUI.PrintPlayerTurn(m_ActivePlayer);
                CheckersBoardMove? move = getNextMove(validMoves, out m_GameQuitedByPlayer);
                if (m_GameQuitedByPlayer)
                {
                    //player loose 
                    break;
                }

                CheckersBoardMove validMove = move.GetValueOrDefault();
                bool eatOpenetsPiece = m_GameBoard.playMove(validMove);
                if (eatOpenetsPiece)
                {
                    validMoves = m_GameBoard.getValidMovesToEatFromPostions(validMove.To);
                    continueTurnForCurrentPlayer = validMoves.Count > 0 ? true : false;
                }
                else
                {
                    continueTurnForCurrentPlayer = false;
                }

                CheckersUI.PrintBoard(m_GameBoard.Board, m_GameBoard.Size);
                CheckersUI.PrintPlayedMove(validMove, m_ActivePlayer);

                if (!continueTurnForCurrentPlayer)
                {
                    if (isPlayerWon(m_ActivePlayer))
                    {

                    }
                    else
                    {
                        switchActivePlayer();
                    }
                }
            }
        }

        private CheckersBoardMove? getNextMove(List<CheckersBoardMove> validMoves, out bool m_GameQuitedByPlayer)
        {
            CheckersBoardMove? move = CheckersUI.GetMoveFromPlayer(out m_GameQuitedByPlayer);

            while (!m_GameQuitedByPlayer && !isValidMove(validMoves, move.GetValueOrDefault()))
            {
                CheckersUI.PrintMoveInvalid();
                move = CheckersUI.GetMoveFromPlayer(out m_GameQuitedByPlayer);
            }

            return move;
        }

        private void switchActivePlayer()
        {
            if (m_ActivePlayer.Equals(m_Player1))
            {
                m_ActivePlayer = m_Player2;
            }
            else
            {
                m_ActivePlayer = m_Player1;
            }
        }

        private bool isValidMove(List<CheckersBoardMove> valideMoves, CheckersBoardMove move)
        {
            return valideMoves.Contains(move);
        }
    }
}
