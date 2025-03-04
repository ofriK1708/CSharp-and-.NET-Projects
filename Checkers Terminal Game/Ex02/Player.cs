﻿namespace Ex02
{
    internal class Player
    {
        internal string Name { get;}
        internal ePlayerType PlayerType { get; set; }
        internal uint Score { get; set; }
        internal eCheckersPieceType PieceType { get; }

        internal Player(string i_Name, ePlayerType i_PlayerType, eCheckersPieceType i_PieceType)
        {
            this.Name = i_Name;
            this.PlayerType = i_PlayerType;
            this.PieceType = i_PieceType;
            this.Score = 0;
        }

        internal void addToScore(uint i_NewScore)
        {
            Score = Score + i_NewScore;
        }
    }
}
