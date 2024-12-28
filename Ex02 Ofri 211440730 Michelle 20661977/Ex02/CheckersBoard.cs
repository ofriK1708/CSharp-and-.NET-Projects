
using System;
using System.Collections.Generic;

namespace Ex02
{
    internal class CheckersBoard
    {
        internal eCheckersBoardSize Size { get; private set; }
        internal eCheckersBoardPiece[,] Board { get; private set; }

        private List<BoardPosition> m_Player1PiecePositions = new List<BoardPosition>();

        private List<BoardPosition> m_Player2PiecePositions = new List<BoardPosition>();

        internal CheckersBoard(eCheckersBoardSize i_size)
        {
            Size = i_size;
            Board = new eCheckersBoardPiece[(int)Size, (int)Size];
            resetBoard((int)Size);
        }
        
        internal void resetBoard(int i_BoardSize)
        {
            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    if ((i + j) % 2 == 1) 
                    {
                        if (j < (i_BoardSize - 2) / 2)
                        {
                            Board[i, j] = eCheckersBoardPiece.OPiece;
                            m_Player2PiecePositions.Add(new BoardPosition(i, j));
                        }
                        else if (j > i_BoardSize / 2)
                        {
                            Board[i, j] = eCheckersBoardPiece.XPiece;
                            m_Player1PiecePositions.Add(new BoardPosition(i,j));
                        }
                        else
                        {
                            Board[i, j] = eCheckersBoardPiece.EmptyPlace;
                        }
                    }
                    else
                    {
                        Board[i, j] = eCheckersBoardPiece.EmptyPlace;
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
                m_Player1PiecePositions : m_Player2PiecePositions;
            eCheckersBoardPiece opponentPiece = i_ActivePlayer.CheckersBoardPiece == eCheckersBoardPiece.XPiece ? eCheckersBoardPiece.OPiece : eCheckersBoardPiece.XPiece;
            int directionToMoveInRow = i_ActivePlayer.CheckersBoardPiece == eCheckersBoardPiece.XPiece ? 1 : -1;

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

                if(isCellInRange(newRow, newColRight) && isCellEmpty(newRow, newColRight))
                {
                    validBoardPositions.Add(new CheckersBoardMove(position, new BoardPosition(newRow, newColRight)));
                }
                if(isCellInRange(newRow, newColLeft) && isCellEmpty(newRow, newColLeft))
                {
                    validBoardPositions.Add(new CheckersBoardMove(position, new BoardPosition(newRow, newColLeft)));
                }
                if(isCellInRange(newRowDouble, newColRightDouble) && isCellEmpty(newRowDouble, newColRightDouble) && Board[newRow, newColRight] == opponentPiece)
                {
                    validBoardPositions.Add(new CheckersBoardMove(position, new BoardPosition(newRowDouble, newColRightDouble)));
                }
                if(isCellInRange(newRowDouble, newColLeftDouble) && isCellEmpty(newRowDouble, newColLeftDouble) && Board[newRow, newColLeft] == opponentPiece)
                {
                    validBoardPositions.Add(new CheckersBoardMove(position, new BoardPosition(newRowDouble, newColLeftDouble)));
                }
                if(isPieceKing(position.Row, position.Column))
                {
                    if (isCellInRange(newKingRow, newColRight) && isCellEmpty(newKingRow, newColRight))
                    {
                        validBoardPositions.Add(new CheckersBoardMove(position, new BoardPosition(newKingRow, newColRight)));
                    }
                    if (isCellInRange(newKingRow, newColLeft) && isCellEmpty(newKingRow, newColLeft))
                    {
                        validBoardPositions.Add(new CheckersBoardMove(position, new BoardPosition(newKingRow, newColLeft)));
                    }
                    if (isCellInRange(newKingRowDouble, newColRightDouble) && isCellEmpty(newKingRowDouble, newColRightDouble) && Board[newKingRow, newColRight] == opponentPiece)
                    {
                        validBoardPositions.Add(new CheckersBoardMove(position, new BoardPosition(newKingRowDouble, newColRightDouble)));
                    }
                    if (isCellInRange(newKingRowDouble, newColLeftDouble) && isCellEmpty(newKingRowDouble, newColLeftDouble) && Board[newKingRow, newColLeft] == opponentPiece)
                    {
                        validBoardPositions.Add(new CheckersBoardMove(position, new BoardPosition(newKingRowDouble, newColLeftDouble)));
                    }
                }
            }
   
            return validBoardPositions;
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
                int colDiff = (int)Math.Abs(to.Column - from.Column) / 2;
                rowDiff = rowDiff / 2;
                eCheckersBoardPiece removedBoardPiece = Board[rowDiff, colDiff];
                Board[rowDiff, colDiff] = eCheckersBoardPiece.EmptyPlace;
                removePieceFromBoard(new BoardPosition(rowDiff, colDiff), removedBoardPiece);
                isEatOponent = true;
            }

            return isEatOponent;
        }

        private void removePieceFromBoard(BoardPosition from,eCheckersBoardPiece boardPiece)
        {
            switch (boardPiece)
            {
                case eCheckersBoardPiece.XPiece:
                    m_Player1PiecePositions.Remove(from);
                    m_Player1PiecePositions.Add(to);
                    break;
                case eCheckersBoardPiece.OPiece:
                    m_Player2PiecePositions.Remove(from);
                    m_Player2PiecePositions.Add(to);
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

        internal List<CheckersBoardMove> getValidMovesToEatFromPostions(BoardPosition position)
        {
            //implement
           return new List<CheckersBoardMove>();
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
    }
}
