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
    }
}
