﻿using System;

namespace Ex02
{
    internal struct CheckersBoardMove
    {
        internal BoardPosition From { get; set; }
        internal BoardPosition To { get; set; }

        internal CheckersBoardMove(String i_MoveInput)
        {
            this.From = new BoardPosition(i_MoveInput[0], i_MoveInput[1]);
            this.To = new BoardPosition(i_MoveInput[3], i_MoveInput[4]);
        }

        internal CheckersBoardMove(BoardPosition i_From, BoardPosition i_To)
        {
            this.From = i_From;
            this.To = i_To;
        }
    }
}