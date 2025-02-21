﻿namespace CheckersLogic
{
    public class Player
    {
        public string Name { get;}
        public ePlayerType PlayerType { get; set; }
        public uint Score { get; set; }
        public eCheckersPieceType PieceType { get; }
        public eCheckersPieceType KingPieceType { get;}
        public Player(string i_Name, ePlayerType i_PlayerType, eCheckersPieceType i_PieceType, eCheckersPieceType i_KingPieceType)
        {
            this.Name = i_Name;
            this.PlayerType = i_PlayerType;
            this.PieceType = i_PieceType;
            this.KingPieceType = i_KingPieceType;
            this.Score = 0;
        }

        internal void addToScore(uint i_NewScore)
        {
            Score += i_NewScore;
        }
    }
}
