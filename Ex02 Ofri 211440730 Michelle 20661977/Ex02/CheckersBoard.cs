
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
            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    if ((i + j) % 2 == 1) 
                    {
                        if (j < (i_BoardSize - 2) / 2)
                        {
                            Board[i, j] = eCheckersBoardPiece.OPiece;
                            m_OPositions.Add(new BoardPosition(i, j));
                        }
                        else if (j > i_BoardSize / 2)
                        {
                            Board[i, j] = eCheckersBoardPiece.XPiece;
                            m_XPositions.Add(new BoardPosition(i,j));
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


        internal List<CheckersBoardMove> getAllValidMoves(Player i_ActivePlayer)
        {
            List<CheckersBoardMove> validBoardPositions = new List<CheckersBoardMove>();


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

            switch (fromBoardPiece)
            {
                case eCheckersBoardPiece.XPiece:
                case eCheckersBoardPiece.XKingPiece:
                    m_XPositions.Remove(from);
                    m_XPositions.Add(to);
                    break;
                case eCheckersBoardPiece.OPiece:
                case eCheckersBoardPiece.OKingPiece:
                    m_OPositions.Remove(from);
                    m_OPositions.Add(to);
                    break;

            }

            bool isEatOponent = false;
            int rowDiff = (int)Math.Abs(to.Row - from.Row);
            if (rowDiff == 2)
            {
                int colDiff = (int)Math.Abs(to.Column - from.Column) / 2;
                rowDiff = rowDiff / 2;
                Board[rowDiff, colDiff] = eCheckersBoardPiece.EmptyPlace;
                isEatOponent = true;
            }

            return isEatOponent;
        }

        private eCheckersBoardPiece getToBoardPiece(eCheckersBoardPiece i_FromBoardPiece, uint i_ToRow)
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
    }
}
