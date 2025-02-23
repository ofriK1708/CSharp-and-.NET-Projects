using System;
using System.Collections.Generic;

namespace CheckersLogic
{
    public class CheckersBoard
    {
        public int Size { get; }
        private eCheckersPieceType[,] Board { get; }
        internal List<BoardPosition> XPositions { get; } = new List<BoardPosition>();
        internal List<BoardPosition> OPositions { get; } = new List<BoardPosition>();
        public event Action<List<BoardPosition>,List<BoardPosition>> BoardReset;
        public event Action<BoardPosition, bool> PieceRemoved;
        public event Action<BoardPosition, eCheckersPieceType> PieceAdded;

        internal CheckersBoard(int i_Size)
        {
            Size = i_Size;
            Board = new eCheckersPieceType[Size, Size];
        }

        public eCheckersPieceType GetPieceType(BoardPosition i_Position)
        {
            return Board[i_Position.Row,i_Position.Column];
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
                            XPositions.Add(new BoardPosition(row, col));
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

            OnBoardReset();
        }

        private void OnBoardReset()
        {
            BoardReset?.Invoke(XPositions, OPositions);
        }

        internal bool IsCellEmpty(int i_Row, int i_Col)
        {
            return Board[i_Row, i_Col] == eCheckersPieceType.EmptyPlace;
        }

        internal bool IsCellInRange(int i_Row, int i_Col)
        {
            return i_Row >= 0 && i_Row < Size && i_Col >= 0 && i_Col < Size;
        }

        internal bool IsPieceKing(int i_Row, int i_Col)
        {
            return Board[i_Row, i_Col] == eCheckersPieceType.XKingPiece ||
                   Board[i_Row, i_Col] == eCheckersPieceType.OKingPiece;
        }

        internal bool IsOpponentPiece(eCheckersPieceType i_OpponentPiece, int i_NewRow, int i_NewColRight)
        {
            eCheckersPieceType  opponentKingPiece = i_OpponentPiece.Equals(eCheckersPieceType.XPiece) ? eCheckersPieceType.XKingPiece : eCheckersPieceType.OKingPiece;

            return Board[i_NewRow, i_NewColRight] == i_OpponentPiece ||
                   Board[i_NewRow, i_NewColRight] == opponentKingPiece;
        }

        internal bool executeMove(CheckersBoardMove i_ValidMove)
        {
            BoardPosition startPosition = i_ValidMove.From;
            BoardPosition nextPosition = i_ValidMove.To;
            eCheckersPieceType pieceTypeInStartPos = Board[startPosition.Row, startPosition.Column];
            eCheckersPieceType pieceTypeInNextPos = getPieceTypeForNextPos(pieceTypeInStartPos, nextPosition.Row);
            bool isSkipMove = false;
            int rowDiff = Math.Abs(nextPosition.Row - startPosition.Row);

            Board[nextPosition.Row, nextPosition.Column] = pieceTypeInNextPos;
            Board[startPosition.Row, startPosition.Column] = eCheckersPieceType.EmptyPlace;
            removePieceFromBoard(startPosition, pieceTypeInStartPos);
            addPieceToBoard(nextPosition, pieceTypeInNextPos);
            
            if (rowDiff == 2)
            {
                int skippedColl = (nextPosition.Column + startPosition.Column) / 2;
                int skippedRow = (nextPosition.Row + startPosition.Row) / 2;
                eCheckersPieceType removedPieceType = Board[skippedRow, skippedColl];
                Board[skippedRow, skippedColl] = eCheckersPieceType.EmptyPlace;
                removePieceFromBoard(new BoardPosition(skippedRow, skippedColl), removedPieceType,true);
                isSkipMove = true;
            }

            return isSkipMove;
        }

        private void removePieceFromBoard(BoardPosition i_PositionToRemove, eCheckersPieceType i_PieceType, bool i_IsSkipped = false)
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
            
            OnPieceRemoved(i_PositionToRemove, i_IsSkipped);
        }

        private void OnPieceRemoved(BoardPosition i_PositionToRemove, bool i_IsSkipped)
        {
            PieceRemoved?.Invoke(i_PositionToRemove, i_IsSkipped);
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
            
            OnPieceAdded(i_PositionToAdd, i_PieceType);
        }

        private void OnPieceAdded(BoardPosition i_PositionToAdd, eCheckersPieceType i_PieceType)
        {
            PieceAdded?.Invoke(i_PositionToAdd, i_PieceType);
        }

        private eCheckersPieceType getPieceTypeForNextPos(eCheckersPieceType i_PieceTypeFromStartPos,
            int i_NextMoveRowNum)
        {
            eCheckersPieceType pieceTypeForNextPos;

            if (i_PieceTypeFromStartPos.Equals(eCheckersPieceType.XPiece) && i_NextMoveRowNum == 0)
            {
                pieceTypeForNextPos = eCheckersPieceType.XKingPiece;
            }
            else if (i_PieceTypeFromStartPos.Equals(eCheckersPieceType.OPiece) && Size - 1 == i_NextMoveRowNum)
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
            bool isAllPiecesRemoved;

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