using System;

namespace Ex02
{
    internal struct BoardPosition
    {
        internal Tuple<uint, uint> Position { get; }

        internal BoardPosition(char row, char col)
        {
            this.Position = Tuple.Create((uint)(row - 'A'),(uint)(col - 'a'));
        }

        internal BoardPosition(uint row, uint col)
        {
            this.Position = Tuple.Create(row, col);
        }
    }
}
