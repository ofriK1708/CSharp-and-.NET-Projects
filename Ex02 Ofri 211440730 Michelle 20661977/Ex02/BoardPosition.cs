using System;

namespace Ex02
{
    internal struct BoardPosition
    {
        internal int Row { get; }
        internal int Column { get; }

        internal BoardPosition(char row, char col)
        {
            this.Row = (row - 'A');
            this.Column = (col - 'a');
        }

        internal BoardPosition(int row, int col)
        {
            this.Row = row;
            this.Column = col;
        }
    }
}
