
using System;
using System.Collections.Generic;

namespace Ex02
{
    internal class CheckersGame
    {
        internal CheckersBoard GameBoard { get; private set; }
        internal Player Player1 { get; set; }
        internal Player Player2 { get; set; }
        internal Player ActivePlayer { get; private set; }
        internal List<CheckersBoardMove> ValidMoves { get; private set; } = new List<CheckersBoardMove>();
        private bool m_ContinueTurnForCurrentPlayer = false;
        internal bool IsActivePlayerWon { get; private set; } = false;
        internal bool IsStalemate { get; private set; } = false;

        internal CheckersGame(Player i_Player1, Player i_Player2, eCheckersBoardSize i_CheckersBoardSize)
        {
            Player1 = i_Player1;
            Player2 = i_Player2;
            GameBoard = new CheckersBoard(i_CheckersBoardSize);
            ActivePlayer = Player1;
        }

        internal void ResetGame()
        {
            GameBoard.resetBoard((int)GameBoard.Size);
            ActivePlayer = Player1;
            IsActivePlayerWon = false;
            IsStalemate = false;
            m_ContinueTurnForCurrentPlayer = false;
        }

        internal void handleGameStateBeforeNextMove()
        {
            if (!m_ContinueTurnForCurrentPlayer)
            {
                ValidMoves = getAllValidMoves(ActivePlayer);
                if (ValidMoves.Count <= 0)
                {
                    Player opponent = getOpponent(ActivePlayer);
                    List<CheckersBoardMove> opponentsValidMoves = getAllValidMoves(opponent);

                    if (opponentsValidMoves.Count <= 0)
                    {
                        IsStalemate = true;
                    }
                    else
                    {
                        HandleOpponentWin(); 
                    }
                }
            }
        }

        internal void playMove(CheckersBoardMove i_ValidMove)
        {
            bool skippedOpponentsPiece = GameBoard.executeMove(i_ValidMove);

            if (skippedOpponentsPiece)
            {
                ValidMoves = getValidSkipsFromPosition(i_ValidMove.To, ActivePlayer);
                m_ContinueTurnForCurrentPlayer = ValidMoves.Count > 0 ? true : false;
            }
            else
            {
                m_ContinueTurnForCurrentPlayer = false;
            }
        }

        internal void handleGameStateAfterMove() 
        { 
           if (!m_ContinueTurnForCurrentPlayer)
           {
                if (checkIfPlayerWon(ActivePlayer))
                {
                    handleActivePlayerWin();
                }
                else
                {
                    switchActivePlayer();
                }
            }
        }

        private Player getOpponent(Player i_ActivePlayer)
        {
            Player opponent;

            if (i_ActivePlayer.Equals(Player1))
            {
                opponent = Player2;
            }
            else
            {
                opponent = Player1;
            }

            return opponent;
        }

        private void handleActivePlayerWin()
        {
            uint addedScore = calcScore(ActivePlayer.PieceType);

            ActivePlayer.addToScore(addedScore);
            IsActivePlayerWon = true;
        }

        internal void HandleOpponentWin()
        {
            switchActivePlayer();
            handleActivePlayerWin();
        }

        private bool checkIfPlayerWon(Player i_ActivePlayer)
        {
            bool isPlayerWon = false;
            eCheckersPieceType pieceType = i_ActivePlayer.PieceType;

            if(pieceType.Equals(eCheckersPieceType.XPiece) || pieceType.Equals(eCheckersPieceType.XKingPiece))
            {
               isPlayerWon =  GameBoard.isAllPiecesRemoved(eCheckersPieceType.OPiece);
            }
            else
            {
                isPlayerWon = GameBoard.isAllPiecesRemoved(eCheckersPieceType.XPiece);
            }

            return isPlayerWon;
        }

        private void switchActivePlayer()
        {
            if (ActivePlayer.Equals(Player1))
            {
                ActivePlayer = Player2;
            }
            else
            {
                ActivePlayer = Player1;
            }
        }

        internal bool CheckMovePartOfValidMoves(CheckersBoardMove i_Move)
        {
            return ValidMoves.Contains(i_Move);
        }

        private uint calcScore(eCheckersPieceType i_CheckersBoardPiece)
        {
            List<BoardPosition> winningPositions;
            List<BoardPosition> loosingPositions;
            uint winnerScore = 0;
            uint looserScore = 0;

            if (i_CheckersBoardPiece.Equals(eCheckersPieceType.XPiece))
            {
                winningPositions = GameBoard.XPositions;
                loosingPositions = GameBoard.OPositions;
            }
            else
            {
                winningPositions = GameBoard.OPositions;
                loosingPositions = GameBoard.XPositions;
            }

            foreach (BoardPosition position in winningPositions)
            {
                if (GameBoard.isPieceKing(position.Row, position.Column))
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
                if (GameBoard.isPieceKing(position.Row, position.Column))
                {
                    looserScore = looserScore + 4;
                }
                else
                {
                    looserScore++;
                }
            }

            return winnerScore >= looserScore ? winnerScore - looserScore : looserScore - winnerScore;
        }

        private List<CheckersBoardMove> getAllValidMoves(Player i_ActivePlayer)
        {
            List<CheckersBoardMove> validBoardMoves = new List<CheckersBoardMove>();
            List<BoardPosition> positionsToCheck = i_ActivePlayer.PieceType == eCheckersPieceType.XPiece ? GameBoard.XPositions : GameBoard.OPositions;
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

                    if (GameBoard.isCellInRange(newRow, newColRight) && GameBoard.isCellEmpty(newRow, newColRight))
                    {
                        validBoardMoves.Add(new CheckersBoardMove(position, new BoardPosition(newRow, newColRight)));
                    }

                    if (GameBoard.isCellInRange(newRow, newColLeft) && GameBoard.isCellEmpty(newRow, newColLeft))
                    {
                        validBoardMoves.Add(new CheckersBoardMove(position, new BoardPosition(newRow, newColLeft)));
                    }

                    if (GameBoard.isPieceKing(position.Row, position.Column))
                    {
                        if (GameBoard.isCellInRange(newKingRow, newColRight) && GameBoard.isCellEmpty(newKingRow, newColRight))
                        {
                            validBoardMoves.Add(new CheckersBoardMove(position, new BoardPosition(newKingRow, newColRight)));
                        }

                        if (GameBoard.isCellInRange(newKingRow, newColLeft) && GameBoard.isCellEmpty(newKingRow, newColLeft))
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

            if (GameBoard.isCellInRange(newRowDouble, newColRightDouble) && GameBoard.isCellEmpty(newRowDouble, newColRightDouble) && GameBoard.isOponentPiece(opponentPiece, newRow, newColRight))
            {
                validBoardPositions.Add(new CheckersBoardMove(i_Position, new BoardPosition(newRowDouble, newColRightDouble)));
            }

            if (GameBoard.isCellInRange(newRowDouble, newColLeftDouble) && GameBoard.isCellEmpty(newRowDouble, newColLeftDouble) && GameBoard.isOponentPiece(opponentPiece, newRow, newColLeft))
            {
                validBoardPositions.Add(new CheckersBoardMove(i_Position, new BoardPosition(newRowDouble, newColLeftDouble)));
            }

            if (GameBoard.isPieceKing(i_Position.Row, i_Position.Column))
            {
                if (GameBoard.isCellInRange(newKingRowDouble, newColRightDouble) && GameBoard.isCellEmpty(newKingRowDouble, newColRightDouble) && GameBoard.isOponentPiece(opponentPiece, newKingRow, newColRight))
                {
                    validBoardPositions.Add(new CheckersBoardMove(i_Position, new BoardPosition(newKingRowDouble, newColRightDouble)));
                }

                if (GameBoard.isCellInRange(newKingRowDouble, newColLeftDouble) && GameBoard.isCellEmpty(newKingRowDouble, newColLeftDouble) && GameBoard.isOponentPiece(opponentPiece, newKingRow, newColLeft))
                {
                    validBoardPositions.Add(new CheckersBoardMove(i_Position, new BoardPosition(newKingRowDouble, newColLeftDouble)));
                }
            }

            return validBoardPositions;
        }
    }
}
