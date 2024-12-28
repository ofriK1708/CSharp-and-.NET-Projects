using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            
          
        }
    }
}
