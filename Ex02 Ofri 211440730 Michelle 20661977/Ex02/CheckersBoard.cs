using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    internal class CheckersBoard
    {
        private eCheckersBoardSize m_Size;
        private eCheckersBoardPiece[,] board;
        internal CheckersBoard(eCheckersBoardSize i_size)
        {
            m_Size = i_size;
            board = new eCheckersBoardPiece[(int)m_Size, (int)m_Size];
            resetBoard((int)m_Size);
        }
        
        internal void resetBoard(int i_BoardSize)
        {
            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    if ((i + j) % 2 == 0) 
                    {
                        if (i < i_BoardSize)
                            board[i, j] = eCheckersBoardPiece.FirstPlayerRegularPiece;
                        else if (i > i_BoardSize / 2) 
                            board[i, j] = eCheckersBoardPiece.SecondPlayerRegularPiece;
                        else
                            board[i, j] = eCheckersBoardPiece.EmptyPlace;
                    }
                    else
                    {
                        board[i, j] = eCheckersBoardPiece.EmptyPlace;
                    }
                }
            }
        }
        
    }
}
