
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
        private const string k_ComputerPlayerName = "Computer";
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
            m_Player1 = new Player(CheckersUI.GetPlayerName(), ePlayerType.Human, eCheckersPieceType.XPiece);
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
                secondPlayerName = k_ComputerPlayerName;
            }

            m_Player2 = new Player(secondPlayerName, secondPlayerType, eCheckersPieceType.OPiece);
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
                    validMoves = getAllValidMoves(m_ActivePlayer);
                    if (validMoves.Count <= 0)
                    {
                        Player opponent = getOpponent(m_ActivePlayer);
                        List<CheckersBoardMove> opponentsValidMoves = getAllValidMoves(opponent);
                        if (opponentsValidMoves.Count <= 0)
                        {
                            CheckersUI.PrintStalemateMessage(m_Player1, m_Player2);
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
                bool skippedOpponentsPiece = m_GameBoard.playMove(validMove);
                if (skippedOpponentsPiece)
                {
                    validMoves = getValidSkipsFromPosition(validMove.To, m_ActivePlayer);
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
                    if (checkIfPlayerWon(m_ActivePlayer))
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

        private Player getOpponent(Player i_ActivePlayer)
        {
            Player opponent;

            if (i_ActivePlayer.Equals(m_Player1))
            {
                opponent = m_Player2;
            }
            else
            {
                opponent = m_Player1;
            }

            return opponent;
        }

        private void handleActivePlayerWin()
        {
            uint addedScore = calcScore(m_ActivePlayer.PieceType);

            m_ActivePlayer.addToScore(addedScore);
            CheckersUI.PrintWinMessage(m_ActivePlayer, m_Player1, m_Player2, addedScore);
            m_GameFinished = true;
        }

        private bool checkIfPlayerWon(Player i_ActivePlayer)
        {
            bool isPlayerWon = false;
            eCheckersPieceType pieceType = i_ActivePlayer.PieceType;

            if(pieceType.Equals(eCheckersPieceType.XPiece) || pieceType.Equals(eCheckersPieceType.XKingPiece))
            {
               isPlayerWon =  m_GameBoard.isAllPiecesRemoved(eCheckersPieceType.OPiece);
            }
            else
            {
                isPlayerWon = m_GameBoard.isAllPiecesRemoved(eCheckersPieceType.XPiece);
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

                while (!m_GameQuitedByPlayer && !checkMovePartOfValidMoves(i_ValidMoves, move.GetValueOrDefault()))
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

        private bool checkMovePartOfValidMoves(List<CheckersBoardMove> i_ValideMoves, CheckersBoardMove i_Move)
        {
            return i_ValideMoves.Contains(i_Move);
        }

        private uint calcScore(eCheckersPieceType i_CheckersBoardPiece)
        {
            List<BoardPosition> winningPositions;
            List<BoardPosition> loosingPositions;
            uint winnerScore = 0;
            uint looserScore = 0;

            if (i_CheckersBoardPiece.Equals(eCheckersPieceType.XPiece))
            {
                winningPositions = m_GameBoard.XPositions;
                loosingPositions = m_GameBoard.OPositions;
            }
            else
            {
                winningPositions = m_GameBoard.OPositions;
                loosingPositions = m_GameBoard.XPositions;
            }

            foreach (BoardPosition position in winningPositions)
            {
                if (m_GameBoard.isPieceKing(position.Row, position.Column))
                {
                    winnerScore = winnerScore + 4;
                }
                else
                {
                    winnerScore++;
                }
            }

            foreach (BoardPosition position in loosingPositions)
            {
                if (m_GameBoard.isPieceKing(position.Row, position.Column))
                {
                    looserScore = looserScore + 4;
                }
                else
                {
                    looserScore++;
                }
            }

            return (uint)Math.Abs(winnerScore - looserScore);
        }

        private List<CheckersBoardMove> getAllValidMoves(Player i_ActivePlayer)
        {
            List<CheckersBoardMove> validBoardMoves = new List<CheckersBoardMove>();
            List<BoardPosition> positionsToCheck = i_ActivePlayer.PieceType == eCheckersPieceType.XPiece ? m_GameBoard.XPositions : m_GameBoard.OPositions;
            eCheckersPieceType opponentPiece = i_ActivePlayer.PieceType == eCheckersPieceType.XPiece ? eCheckersPieceType.OPiece : eCheckersPieceType.XPiece;
            int directionToMoveInRow = i_ActivePlayer.PieceType == eCheckersPieceType.XPiece ? -1 : 1;

            foreach (BoardPosition position in positionsToCheck)
            {
                validBoardMoves.AddRange(getValidSkipsFromPosition(position, i_ActivePlayer));
            }

            if (validBoardMoves.Count == 0)
            {
                foreach (BoardPosition position in positionsToCheck)
                {
                    int newRow = position.Row + directionToMoveInRow;
                    int newColRight = position.Column + 1;
                    int newColLeft = position.Column - 1;
                    int newKingRow = position.Row - directionToMoveInRow;

                    if (m_GameBoard.isCellInRange(newRow, newColRight) && m_GameBoard.isCellEmpty(newRow, newColRight))
                    {
                        validBoardMoves.Add(new CheckersBoardMove(position, new BoardPosition(newRow, newColRight)));
                    }

                    if (m_GameBoard.isCellInRange(newRow, newColLeft) && m_GameBoard.isCellEmpty(newRow, newColLeft))
                    {
                        validBoardMoves.Add(new CheckersBoardMove(position, new BoardPosition(newRow, newColLeft)));
                    }

                    if (m_GameBoard.isPieceKing(position.Row, position.Column))
                    {
                        if (m_GameBoard.isCellInRange(newKingRow, newColRight) && m_GameBoard.isCellEmpty(newKingRow, newColRight))
                        {
                            validBoardMoves.Add(new CheckersBoardMove(position, new BoardPosition(newKingRow, newColRight)));
                        }

                        if (m_GameBoard.isCellInRange(newKingRow, newColLeft) && m_GameBoard.isCellEmpty(newKingRow, newColLeft))
                        {
                            validBoardMoves.Add(new CheckersBoardMove(position, new BoardPosition(newKingRow, newColLeft)));
                        }
                    }
                }
            }

            return validBoardMoves;
        }

        private List<CheckersBoardMove> getValidSkipsFromPosition(BoardPosition i_Position, Player i_ActivePlayer)
        {
            List<CheckersBoardMove> validBoardPositions = new List<CheckersBoardMove>();
            eCheckersPieceType opponentPiece = i_ActivePlayer.PieceType == eCheckersPieceType.XPiece ? eCheckersPieceType.OPiece : eCheckersPieceType.XPiece;
            int directionToMoveInRow = i_ActivePlayer.PieceType == eCheckersPieceType.XPiece ? -1 : 1;
            int newRow = i_Position.Row + directionToMoveInRow;
            int newRowDouble = i_Position.Row + directionToMoveInRow * 2;
            int newColRight = i_Position.Column + 1;
            int newColLeft = i_Position.Column - 1;
            int newColRightDouble = i_Position.Column + 2;
            int newColLeftDouble = i_Position.Column - 2;
            int newKingRow = i_Position.Row - directionToMoveInRow;
            int newKingRowDouble = i_Position.Row - directionToMoveInRow * 2;

            if (m_GameBoard.isCellInRange(newRowDouble, newColRightDouble) && m_GameBoard.isCellEmpty(newRowDouble, newColRightDouble) && m_GameBoard.isOponentPiece(opponentPiece, newRow, newColRight))
            {
                validBoardPositions.Add(new CheckersBoardMove(i_Position, new BoardPosition(newRowDouble, newColRightDouble)));
            }

            if (m_GameBoard.isCellInRange(newRowDouble, newColLeftDouble) && m_GameBoard.isCellEmpty(newRowDouble, newColLeftDouble) && m_GameBoard.isOponentPiece(opponentPiece, newRow, newColLeft))
            {
                validBoardPositions.Add(new CheckersBoardMove(i_Position, new BoardPosition(newRowDouble, newColLeftDouble)));
            }

            if (m_GameBoard.isPieceKing(i_Position.Row, i_Position.Column))
            {
                if (m_GameBoard.isCellInRange(newKingRowDouble, newColRightDouble) && m_GameBoard.isCellEmpty(newKingRowDouble, newColRightDouble) && m_GameBoard.isOponentPiece(opponentPiece, newKingRow, newColRight))
                {
                    validBoardPositions.Add(new CheckersBoardMove(i_Position, new BoardPosition(newKingRowDouble, newColRightDouble)));
                }

                if (m_GameBoard.isCellInRange(newKingRowDouble, newColLeftDouble) && m_GameBoard.isCellEmpty(newKingRowDouble, newColLeftDouble) && m_GameBoard.isOponentPiece(opponentPiece, newKingRow, newColLeft))
                {
                    validBoardPositions.Add(new CheckersBoardMove(i_Position, new BoardPosition(newKingRowDouble, newColLeftDouble)));
                }
            }

            return validBoardPositions;
        }
    }
}
