
using System;
using System.Collections.Generic;

namespace Ex02
{
    internal class CheckersBoard
    {
        internal eCheckersBoardSize Size { get; private set; }
        internal eCheckersPieceType[,] Board { get; private set; }
        internal List<BoardPosition> XPositions { get; } = new List<BoardPosition>();
        internal List<BoardPosition> OPositions { get; } = new List<BoardPosition>();
        internal CheckersBoard(eCheckersBoardSize i_size)
        {
            Size = i_size;
            Board = new eCheckersPieceType[(int)Size, (int)Size];
            resetBoard((int)Size);
        }
        
        internal void resetBoard(int i_BoardSize)
        {
            OPositions.Clear();
            XPositions.Clear();
            for (int row = 0; row < i_BoardSize; row++)
            {
                for (int col = 0; col < i_BoardSize; col++)
                {
                    if ((row + col) % 2 == 1) 
                    {
                        if (row < (i_BoardSize - 2) / 2)
                        {
                            Board[row, col] = eCheckersPieceType.OPiece;
                            OPositions.Add(new BoardPosition(row, col));
                        }
                        else if (row > i_BoardSize / 2)
                        {
                            Board[row, col] = eCheckersPieceType.XPiece;
                            XPositions.Add(new BoardPosition(row,col));
                        }
                        else
                        {
                            Board[row, col] = eCheckersPieceType.EmptyPlace;
                        }
                    }
                    else
                    {
                        Board[row, col] = eCheckersPieceType.EmptyPlace;
                    }
                }
            }
        }

        internal bool isCellEmpty(int i_Row, int i_Col)
        {
            return Board[i_Row, i_Col] == eCheckersPieceType.EmptyPlace;
        }

        internal bool isCellInRange(int i_Row,int i_Col)
        {
            return ((i_Row >= 0) && (i_Row < (int)Size)) && ((i_Col >= 0) && (i_Col < (int)Size));
        }

        internal bool isPieceKing(int i_Row,int i_Col)
        {
            return Board[i_Row, i_Col] == eCheckersPieceType.XKingPiece || Board[i_Row, i_Col] == eCheckersPieceType.OKingPiece;
        }

        internal bool isOponentPiece(eCheckersPieceType i_OpponentPiece, int i_NewRow, int i_NewColRight)
        {
            eCheckersPieceType oponentKingPiece;

            if (i_OpponentPiece.Equals(eCheckersPieceType.XPiece))
            {
                oponentKingPiece = eCheckersPieceType.XKingPiece;
            }
            else
            {
                oponentKingPiece = eCheckersPieceType.OKingPiece;
            }

            return Board[i_NewRow, i_NewColRight] == i_OpponentPiece || Board[i_NewRow, i_NewColRight] == oponentKingPiece;
        }

        internal bool executeMove(CheckersBoardMove i_ValidMove)
        {
            BoardPosition startPosition = i_ValidMove.From;
            BoardPosition nextPosition = i_ValidMove.To;
            eCheckersPieceType pieceTypeInStartPos = Board[startPosition.Row, startPosition.Column];
            eCheckersPieceType pieceTypeInNextPos = getPieceTypeForNextPos(pieceTypeInStartPos, nextPosition.Row);
            bool isSkipMove = false;
            int rowDiff = (int)Math.Abs(nextPosition.Row - startPosition.Row);

            Board[nextPosition.Row, nextPosition.Column] = pieceTypeInNextPos;
            Board[startPosition.Row, startPosition.Column] = eCheckersPieceType.EmptyPlace;
            removePieceFromBoard(startPosition, pieceTypeInStartPos);
            addPieceToBoard(nextPosition, pieceTypeInNextPos);
            if (rowDiff == 2)
            {
                int skippedColl = (nextPosition.Column + startPosition.Column)/2;
                int skippedRow = (nextPosition.Row + startPosition.Row)  /2;
                eCheckersPieceType removedPieceType = Board[skippedRow, skippedColl];
                Board[skippedRow, skippedColl] = eCheckersPieceType.EmptyPlace;
                removePieceFromBoard(new BoardPosition(skippedRow, skippedColl), removedPieceType);
                isSkipMove = true;
            }

            return isSkipMove;
        }

        private void removePieceFromBoard(BoardPosition i_PositionToRemove, eCheckersPieceType i_PieceType)
        {
            switch (i_PieceType)
            {
                case eCheckersPieceType.XPiece:
                case eCheckersPieceType.XKingPiece:
                    XPositions.Remove(i_PositionToRemove);
                    break;
                case eCheckersPieceType.OPiece:
                case eCheckersPieceType.OKingPiece:
                    OPositions.Remove(i_PositionToRemove);
                    break;
            }
        }

        private void addPieceToBoard(BoardPosition i_PositionToAdd, eCheckersPieceType i_PieceType)
        {
            switch (i_PieceType)
            {
                case eCheckersPieceType.XPiece:
                case eCheckersPieceType.XKingPiece:
                    XPositions.Add(i_PositionToAdd);
                    break;
                case eCheckersPieceType.OPiece:
                case eCheckersPieceType.OKingPiece:
                    OPositions.Add(i_PositionToAdd);
                    break;
            }
        }

        private eCheckersPieceType getPieceTypeForNextPos(eCheckersPieceType i_PieceTypeFromStartPos, int i_NextMoveRowNum)
        {
           eCheckersPieceType pieceTypeForNextPos;

           if (i_PieceTypeFromStartPos.Equals(eCheckersPieceType.XPiece) && i_NextMoveRowNum == 0)
            {
              pieceTypeForNextPos = eCheckersPieceType.XKingPiece;   
            }
            else if (i_PieceTypeFromStartPos.Equals(eCheckersPieceType.OPiece) && (int)Size -1 == i_NextMoveRowNum )
            {
              pieceTypeForNextPos = eCheckersPieceType.OKingPiece;
            }
            else
            {
              pieceTypeForNextPos = i_PieceTypeFromStartPos;
            }

            return pieceTypeForNextPos;
        }

        internal bool isAllPiecesRemoved(eCheckersPieceType i_PieceType)
        {
            bool isAllPiecesRemoved = false;

            if (i_PieceType.Equals(eCheckersPieceType.XPiece))
            {
                isAllPiecesRemoved = XPositions.Count == 0;
            }
            else
            {
                isAllPiecesRemoved = OPositions.Count == 0;
            }

            return isAllPiecesRemoved;
        }
    }
}
