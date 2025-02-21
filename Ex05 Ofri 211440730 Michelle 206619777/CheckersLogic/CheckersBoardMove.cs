using System;

namespace CheckersLogic
{
    public struct CheckersBoardMove
    {
        public BoardPosition From { get; set; }
        public BoardPosition To { get; set; }

        public CheckersBoardMove(BoardPosition i_From, BoardPosition i_To)
        {
            this.From = i_From;
            this.To = i_To;
        }

        public void SetMove(String i_MoveInput)
        {
            this.From = new BoardPosition(i_MoveInput[0], i_MoveInput[1]);
            this.To = new BoardPosition(i_MoveInput[3], i_MoveInput[4]);
        }
    }
}
