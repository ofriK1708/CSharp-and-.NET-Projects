
using System.Collections.Generic;

namespace Ex02
{
    internal class CheckersBoard
    {
        internal eCheckersBoardSize Size { get; private set; }
        internal eCheckersBoardPiece[,] Board { get; private set; }

        private List<CheckersBoardPosition> m_XPositions = new List<CheckersBoardPosition>();

        private List<CheckersBoardPosition> m_OPositions = new List<CheckersBoardPosition>();

        private List<CheckersBoardPosition> m_KPositions = new List<CheckersBoardPosition>();

        private List<CheckersBoardPosition> m_UPositions = new List<CheckersBoardPosition>();

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
                            Board[i, j] = eCheckersBoardPiece.SecondPlayerRegularPiece;
                        }
                        else if (j > i_BoardSize / 2)
                        {
                            Board[i, j] = eCheckersBoardPiece.FirstPlayerRegularPiece;
                            m_XPositions.Add(new CheckersBoardPosition());
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
        public array<checkersMove> validateMove(CheckersBoardPosition i_Move, Player i_Player)
        {
            Array<checkersMove> validMoves = new Array<checkersMove>();
            
        }
        
    }
}
