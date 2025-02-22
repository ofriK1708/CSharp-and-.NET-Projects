using System;

namespace CheckersLogic
{
    public struct CheckersBoardMove
    {
        public BoardPosition From { get; private set; }
        public BoardPosition To { get; private set; }

        public CheckersBoardMove(BoardPosition i_From, BoardPosition i_To)
        {
            From = i_From;
            To = i_To;
        }

        public void SetMove(String i_MoveInput) //todo-delete
        {
            From = new BoardPosition(i_MoveInput[0], i_MoveInput[1]);
            To = new BoardPosition(i_MoveInput[3], i_MoveInput[4]);
        }
    }
}
