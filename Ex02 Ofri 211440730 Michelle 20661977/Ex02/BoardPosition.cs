using System;

namespace Ex02
{
    internal struct BoardPosition
    {
        internal uint Row { get; }
        internal uint Column { get; }

        internal BoardPosition(char row, char col)
        {
            this.Row = (uint)(row - 'A');
            this.Column = (uint)(col - 'a');
        }

        internal BoardPosition(uint row, uint col)
        {
            this.Row = row;
            this.Column = col;
        }
    }
}
