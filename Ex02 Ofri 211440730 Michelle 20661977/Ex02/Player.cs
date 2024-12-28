
namespace Ex02
{
    internal class Player
    {
        internal string Name { get;}
        private ePlayerType m_PlayerType;
        internal uint Score { get; set; }
        internal eCheckersBoardPiece CheckersBoardPiece { get; }

        internal Player(string i_Name, ePlayerType i_PlayerType, eCheckersBoardPiece eCheckersBoardPiece)
        {
            this.Name = i_Name;
            this.m_PlayerType = i_PlayerType;
            this.CheckersBoardPiece = eCheckersBoardPiece;
        }
    }
}
