namespace CheckersLogic
{
    public class Player
    {
        public string Name { get;}
        public ePlayerType PlayerType { get; set; }
        public uint Score { get; private set; }
        public eCheckersPieceType PieceType { get; }
        public eCheckersPieceType KingPieceType { get;}
        public Player(string i_Name, ePlayerType i_PlayerType, eCheckersPieceType i_PieceType, eCheckersPieceType i_KingPieceType)
        {
            Name = i_Name;
            PlayerType = i_PlayerType;
            PieceType = i_PieceType;
            KingPieceType = i_KingPieceType;
            Score = 0;
        }

        internal void addToScore(uint i_NewScore)
        {
            Score += i_NewScore;
        }
    }
}
