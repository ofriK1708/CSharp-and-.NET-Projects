using System;

namespace Ex02
{
    internal struct BoardPosition
    {
        internal Tuple<char, char> Position { get; }

        internal BoardPosition(char row, char col)
        {
            this.Position = Tuple.Create(row, col);
        }

        internal BoardPosition(int row, int col)
        {
            this.Position = Tuple.Create((char)('A' + row), (char) ('a' + col));
        }
    }
}
