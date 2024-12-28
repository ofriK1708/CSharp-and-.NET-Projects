
using System;

namespace Ex02
{
    internal class Player
    {
        internal string Name { get;}
        internal ePlayerType PlayerType { get; set; }
        internal uint Score { get; set; }
        internal eCheckersBoardPiece CheckersBoardPiece { get; }


        internal Player(string i_Name, ePlayerType i_PlayerType, eCheckersBoardPiece eCheckersBoardPiece)
        {
            this.Name = i_Name;
            this.PlayerType = i_PlayerType;
            this.CheckersBoardPiece = eCheckersBoardPiece;
        }

        internal void addToScore(uint newScore)
        {
            Score = Score + newScore;
        }
    }
}
