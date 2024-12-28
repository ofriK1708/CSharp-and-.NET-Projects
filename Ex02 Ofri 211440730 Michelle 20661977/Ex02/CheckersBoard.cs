
using System;
using System.Collections.Generic;

namespace Ex02
{
    internal class CheckersBoard
    {
        internal eCheckersBoardSize Size { get; private set; }
        internal eCheckersBoardPiece[,] Board { get; private set; }

        private List<BoardPosition> m_XPositions = new List<BoardPosition>();

        private List<BoardPosition> m_OPositions = new List<BoardPosition>();

        internal CheckersBoard(eCheckersBoardSize i_size)
        {
            Size = i_size;
            Board = new eCheckersBoardPiece[(int)Size, (int)Size];
            resetBoard((int)Size);
        }
        
        internal void resetBoard(int i_BoardSize)
        {
            m_OPositions.Clear();
            m_XPositions.Clear();

            for (int row = 0; row < i_BoardSize; row++)
            {
                for (int col = 0; col < i_BoardSize; col++)
                {
                    if ((row + col) % 2 == 1) 
                    {
                        if (row < (i_BoardSize - 2) / 2)
                        {
                            Board[row, col] = eCheckersBoardPiece.OPiece;
                            m_OPositions.Add(new BoardPosition(row, col));
                        }
                        else if (row > i_BoardSize / 2)
                        {
                            Board[row, col] = eCheckersBoardPiece.XPiece;
                            m_XPositions.Add(new BoardPosition(row,col));
                        }
                        else
                        {
                            Board[row, col] = eCheckersBoardPiece.EmptyPlace;
                        }
                    }
                    else
                    {
                        Board[row, col] = eCheckersBoardPiece.EmptyPlace;
                    }
                }
            }
        }

        internal bool isCellEmpty(int i_Row, int i_Col)
        {
            return Board[i_Row, i_Col] == eCheckersBoardPiece.EmptyPlace;
        }

        internal bool isCellInRange(int i_Row,int i_Col)
        {
            return ((i_Row >= 0) && (i_Row < (int)Size)) && ((i_Col >= 0) && (i_Col < (int)Size));
        }

        internal bool isPieceKing(int i_Row,int i_Col)
        {
            return Board[i_Row, i_Col] == eCheckersBoardPiece.XKingPiece || Board[i_Row, i_Col] == eCheckersBoardPiece.OKingPiece;
        }

        internal List<CheckersBoardMove> getAllValidMoves(Player i_ActivePlayer)
        {
            List<CheckersBoardMove> validBoardPositions = new List<CheckersBoardMove>();
            List<BoardPosition> PositionsToCheck = i_ActivePlayer.CheckersBoardPiece == eCheckersBoardPiece.XPiece ? 
                m_XPositions : m_OPositions;
            eCheckersBoardPiece opponentPiece = i_ActivePlayer.CheckersBoardPiece == eCheckersBoardPiece.XPiece ? eCheckersBoardPiece.OPiece : eCheckersBoardPiece.XPiece;
            int directionToMoveInRow = i_ActivePlayer.CheckersBoardPiece == eCheckersBoardPiece.XPiece ? -1 : 1;

            foreach (BoardPosition position in PositionsToCheck)
            {
                int newRow = position.Row + directionToMoveInRow;
                int newRowDouble = position.Row + directionToMoveInRow * 2;
                int newColRight = position.Column + 1;
                int newColLeft = position.Column - 1;
                int newColRightDouble = position.Column + 2;
                int newColLeftDouble = position.Column - 2;
                int newKingRow = position.Row - directionToMoveInRow;
                int newKingRowDouble = position.Row - directionToMoveInRow * 2;

                if (isCellInRange(newRow, newColRight) && isCellEmpty(newRow, newColRight))
                {
                    validBoardPositions.Add(new CheckersBoardMove(position, new BoardPosition(newRow, newColRight)));
                }
                if (isCellInRange(newRow, newColLeft) && isCellEmpty(newRow, newColLeft))
                {
                    validBoardPositions.Add(new CheckersBoardMove(position, new BoardPosition(newRow, newColLeft)));
                }
                if (isCellInRange(newRowDouble, newColRightDouble) && isCellEmpty(newRowDouble, newColRightDouble) && isOponentPiece(opponentPiece, newRow, newColRight))
                {
                    validBoardPositions.Add(new CheckersBoardMove(position, new BoardPosition(newRowDouble, newColRightDouble)));
                }
                if (isCellInRange(newRowDouble, newColLeftDouble) && isCellEmpty(newRowDouble, newColLeftDouble) && isOponentPiece(opponentPiece, newRow, newColLeft))
                {
                    validBoardPositions.Add(new CheckersBoardMove(position, new BoardPosition(newRowDouble, newColLeftDouble)));
                }
                if (isPieceKing(position.Row, position.Column))
                {
                    if (isCellInRange(newKingRow, newColRight) && isCellEmpty(newKingRow, newColRight))
                    {
                        validBoardPositions.Add(new CheckersBoardMove(position, new BoardPosition(newKingRow, newColRight)));
                    }
                    if (isCellInRange(newKingRow, newColLeft) && isCellEmpty(newKingRow, newColLeft))
                    {
                        validBoardPositions.Add(new CheckersBoardMove(position, new BoardPosition(newKingRow, newColLeft)));
                    }
                    if (isCellInRange(newKingRowDouble, newColRightDouble) && isCellEmpty(newKingRowDouble, newColRightDouble) && isOponentPiece(opponentPiece, newKingRow, newColRight))
                    {
                        validBoardPositions.Add(new CheckersBoardMove(position, new BoardPosition(newKingRowDouble, newColRightDouble)));
                    }
                    if (isCellInRange(newKingRowDouble, newColLeftDouble) && isCellEmpty(newKingRowDouble, newColLeftDouble) && isOponentPiece(opponentPiece, newKingRow, newColLeft))
                    {
                        validBoardPositions.Add(new CheckersBoardMove(position, new BoardPosition(newKingRowDouble, newColLeftDouble)));
                    }
                }
            }

            return validBoardPositions;
        }

        private bool isOponentPiece(eCheckersBoardPiece opponentPiece, int newRow, int newColRight)
        {
            eCheckersBoardPiece oponentKingPiece;

            if (opponentPiece.Equals(eCheckersBoardPiece.XPiece))
            {
                oponentKingPiece = eCheckersBoardPiece.XKingPiece;
            }
            else
            {
                oponentKingPiece = eCheckersBoardPiece.OKingPiece;
            }

            return Board[newRow, newColRight] == opponentPiece || Board[newRow, newColRight] == oponentKingPiece;
        }

        internal bool playMove(CheckersBoardMove i_ValidMove)
        {
            BoardPosition from = i_ValidMove.From;
            BoardPosition to = i_ValidMove.To;

            eCheckersBoardPiece fromBoardPiece = Board[from.Row, from.Column];
            eCheckersBoardPiece toBoardPiece = getToBoardPiece(fromBoardPiece, to.Row);

            Board[to.Row, to.Column] = toBoardPiece;
            Board[from.Row, from.Column] = eCheckersBoardPiece.EmptyPlace;

            removePieceFromBoard(from, fromBoardPiece);
            addPieceToBoard(to, toBoardPiece);

            bool isEatOponent = false;
            int rowDiff = (int)Math.Abs(to.Row - from.Row);
            if (rowDiff == 2)
            {
                int eatenColl = (to.Column + from.Column)/2;
                int eatenRow = (to.Row + from.Row)  /2;
                eCheckersBoardPiece removedBoardPiece = Board[eatenRow, eatenColl];
                Board[eatenRow, eatenColl] = eCheckersBoardPiece.EmptyPlace;
                removePieceFromBoard(new BoardPosition(eatenRow, eatenColl), removedBoardPiece);
                isEatOponent = true;
            }

            return isEatOponent;
        }

        private void removePieceFromBoard(BoardPosition from, eCheckersBoardPiece boardPiece)
        {
            switch (boardPiece)
            {
                case eCheckersBoardPiece.XPiece:
                case eCheckersBoardPiece.XKingPiece:
                    m_XPositions.Remove(from);
                    break;
                case eCheckersBoardPiece.OPiece:
                case eCheckersBoardPiece.OKingPiece:
                    m_OPositions.Remove(from);
                    break;
            }
        }

        private void addPieceToBoard(BoardPosition to, eCheckersBoardPiece boardPiece)
        {
            switch (boardPiece)
            {
                case eCheckersBoardPiece.XPiece:
                case eCheckersBoardPiece.XKingPiece:
                    m_XPositions.Add(to);
                    break;
                case eCheckersBoardPiece.OPiece:
                case eCheckersBoardPiece.OKingPiece:
                    m_OPositions.Add(to);
                    break;
            }
        }

        private eCheckersBoardPiece getToBoardPiece(eCheckersBoardPiece i_FromBoardPiece, int i_ToRow)
        {
           eCheckersBoardPiece toBoardPiece;

           if (i_FromBoardPiece.Equals(eCheckersBoardPiece.XPiece) && i_ToRow == 0)
            {
              toBoardPiece = eCheckersBoardPiece.XKingPiece;   
            }
            else if (i_FromBoardPiece.Equals(eCheckersBoardPiece.OPiece) && Size.Equals(i_ToRow))
            {
              toBoardPiece = eCheckersBoardPiece.OKingPiece;
            }
            else
            {
              toBoardPiece = i_FromBoardPiece;
            }

            return toBoardPiece;
        }

        internal List<CheckersBoardMove> getValidMovesFromPosition(BoardPosition i_Position, Player i_ActivePlayer)
        { 
            List<CheckersBoardMove> validBoardPositions = new List<CheckersBoardMove>();
            eCheckersBoardPiece opponentPiece = i_ActivePlayer.CheckersBoardPiece == eCheckersBoardPiece.XPiece ? eCheckersBoardPiece.OPiece : eCheckersBoardPiece.XPiece;
            int directionToMoveInRow = i_ActivePlayer.CheckersBoardPiece == eCheckersBoardPiece.XPiece ? -1 : 1;
            int newRow = i_Position.Row + directionToMoveInRow;
            int newRowDouble = i_Position.Row + directionToMoveInRow * 2;
            int newColRight = i_Position.Column + 1;
            int newColLeft = i_Position.Column - 1;
            int newColRightDouble = i_Position.Column + 2;
            int newColLeftDouble = i_Position.Column - 2;
            int newKingRow = i_Position.Row - directionToMoveInRow;
            int newKingRowDouble = i_Position.Row - directionToMoveInRow * 2;

            if (isCellInRange(newRowDouble, newColRightDouble) && isCellEmpty(newRowDouble, newColRightDouble) && isOponentPiece(opponentPiece, newRow, newColRight))
            {
                validBoardPositions.Add(new CheckersBoardMove(i_Position, new BoardPosition(newRowDouble, newColRightDouble)));
            }
            if (isCellInRange(newRowDouble, newColLeftDouble) && isCellEmpty(newRowDouble, newColLeftDouble) && isOponentPiece(opponentPiece, newRow, newColLeft))
            {
                validBoardPositions.Add(new CheckersBoardMove(i_Position, new BoardPosition(newRowDouble, newColLeftDouble)));
            }
            if (isPieceKing(i_Position.Row, i_Position.Column))
            {
                if (isCellInRange(newKingRowDouble, newColRightDouble) && isCellEmpty(newKingRowDouble, newColRightDouble) && isOponentPiece(opponentPiece, newKingRow, newColRight))
                {
                    validBoardPositions.Add(new CheckersBoardMove(i_Position, new BoardPosition(newKingRowDouble, newColRightDouble)));
                }
                if (isCellInRange(newKingRowDouble, newColLeftDouble) && isCellEmpty(newKingRowDouble, newColLeftDouble) && isOponentPiece(opponentPiece, newKingRow, newColLeft))
                {
                    validBoardPositions.Add(new CheckersBoardMove(i_Position, new BoardPosition(newKingRowDouble, newColLeftDouble)));
                }
            }

            return validBoardPositions;
        }

        internal bool isAllPiecesRemoved(eCheckersBoardPiece boardPiece)
        {
            bool isAllPiecesRemoved = false;
            if (boardPiece.Equals(eCheckersBoardPiece.XPiece))
            {
                isAllPiecesRemoved = m_XPositions.Count == 0;
            }
            else
            {
                isAllPiecesRemoved = m_OPositions.Count == 0;
            }

            return isAllPiecesRemoved;
        }

        internal uint calcScore(eCheckersBoardPiece checkersBoardPiece)
        {
            List<BoardPosition> winningPositions;
            List<BoardPosition> loosingPositions;
            uint winnerScore = 0;
            uint looserScore= 0;

            if (checkersBoardPiece.Equals(eCheckersBoardPiece.XPiece))
            {
                winningPositions = m_XPositions;
                loosingPositions = m_OPositions;
            }
            else
            {
                winningPositions = m_OPositions;
                loosingPositions = m_XPositions;
            }

            foreach (BoardPosition position in winningPositions)
            {
                if(isPieceKing(position.Row, position.Column))
                {
                    winnerScore = winnerScore + 4;
                }
                else
                {
                    winnerScore++;
                }
            }

            foreach(BoardPosition position in loosingPositions)
            {
                if (isPieceKing(position.Row, position.Column))
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
    }
}
