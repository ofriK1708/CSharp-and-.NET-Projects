using System;
using System.Reflection.Emit;

namespace CheckersLogic
{
    internal struct CheckersMove
    {
        internal Tuple<char, char> From { get; set; }
        internal Tuple<char, char> To { get; set; }

        internal CheckersMove(String i_MoveInput)
        {
            this.From = Tuple.Create(i_MoveInput[0], i_MoveInput[1]);
            this.To = Tuple.Create(i_MoveInput[3], i_MoveInput[4]);
        }
    }
}
