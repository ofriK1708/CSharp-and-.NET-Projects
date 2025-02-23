namespace CheckersLogic
{
    public readonly struct BoardPosition
    {
        public int Row { get; }
        public int Column { get; }

        public BoardPosition(int i_Row, int i_Col)
        {
            Row = i_Row;
            Column = i_Col;
        }

        public override string ToString()
        {
            return Row.ToString() + " " + Column.ToString();
        }
    }
}