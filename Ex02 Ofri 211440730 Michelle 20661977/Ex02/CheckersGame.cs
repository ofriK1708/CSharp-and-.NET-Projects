
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
           bool anotherGame = CheckersUI.GetFromUserIsContinueToAnotherGame();
           while (anotherGame)
           {
             m_GameNumber++;
             CheckersUI.PrintStartGameMessage(m_GameNumber, m_Player1.Name, m_Player2.Name);
             m_GameBoard.resetBoard((int)m_GameBoard.Size);
             m_ActivePlayer = m_Player1;
             CheckersUI.PrintBoard(m_GameBoard.Board, m_GameBoard.Size);
             playGame();
             anotherGame = CheckersUI.GetFromUserIsContinueToAnotherGame();
            }
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
                        Player oponent = getOponent(m_ActivePlayer);
                        List<CheckersBoardMove> oponentsValidMoves = m_GameBoard.getAllValidMoves(oponent);
                        if (oponentsValidMoves.Count <= 0)
                        {
                            CheckersUI.printStalemateMessage(m_Player1, m_Player2);
                            m_GameFinished = true;
                        }
                        else
                        {
                            switchActivePlayer();
                            handleActivePlayerWin();
                        }
                        break;
                    }
                }

                CheckersUI.PrintPlayerTurn(m_ActivePlayer);
                CheckersBoardMove? move = getNextMove(validMoves, out m_GameQuitedByPlayer);
                if (m_GameQuitedByPlayer)
                {
                    switchActivePlayer();
                    handleActivePlayerWin();
                    break;
                }

                CheckersBoardMove validMove = move.GetValueOrDefault();
                bool eatOpenetsPiece = m_GameBoard.playMove(validMove);
                if (eatOpenetsPiece)
                {
                    validMoves = m_GameBoard.getValidMovesEatsFromPosition(validMove.To, m_ActivePlayer);
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
                        handleActivePlayerWin();
                    }
                    else
                    {
                        switchActivePlayer();
                    }
                }
            }
        }

        private Player getOponent(Player m_ActivePlayer)
        {
            Player oponent;
            if (m_ActivePlayer.Equals(m_Player1))
            {
                oponent = m_Player2;
            }
            else
            {
                oponent = m_Player1;
            }

            return oponent;
        }

        private void handleActivePlayerWin()
        {
            uint addedScore = m_GameBoard.calcScore(m_ActivePlayer.CheckersBoardPiece);
            m_ActivePlayer.addToScore(addedScore);
            CheckersUI.printWinMessage(m_ActivePlayer, m_Player1, m_Player2, addedScore);
            m_GameFinished = true;
        }

        private bool isPlayerWon(Player m_ActivePlayer)
        {
            bool isPlayerWon = false;
            eCheckersBoardPiece checkersBoardPiece = m_ActivePlayer.CheckersBoardPiece;
            if(checkersBoardPiece.Equals(eCheckersBoardPiece.XPiece) || checkersBoardPiece.Equals(eCheckersBoardPiece.XKingPiece))
            {
               isPlayerWon =  m_GameBoard.isAllPiecesRemoved(eCheckersBoardPiece.OPiece);
            }
            else
            {
                isPlayerWon = m_GameBoard.isAllPiecesRemoved(eCheckersBoardPiece.XPiece);
            }

            return isPlayerWon;
        }

        private CheckersBoardMove? getNextMove(List<CheckersBoardMove> i_ValidMoves, out bool o_GameQuitedByPlayer)
        {
            CheckersBoardMove? move = null;
            if (m_ActivePlayer.PlayerType == ePlayerType.Computer)
            {
                CheckersUI.PrintComputerMessage();
                o_GameQuitedByPlayer = false;
                uint randomIndex = (uint)new Random().Next(i_ValidMoves.Count);
                move = i_ValidMoves[(int)randomIndex];
            }
            else
            {
                move = CheckersUI.GetMoveFromPlayer(out o_GameQuitedByPlayer);

                while (!m_GameQuitedByPlayer && !isValidMove(i_ValidMoves, move.GetValueOrDefault()))
                {
                    CheckersUI.PrintMoveInvalid();
                    move = CheckersUI.GetMoveFromPlayer(out m_GameQuitedByPlayer);
                }
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

        private bool isValidMove(List<CheckersBoardMove> i_ValideMoves, CheckersBoardMove i_Move)
        {
            return i_ValideMoves.Contains(i_Move);
        }
    }
}
