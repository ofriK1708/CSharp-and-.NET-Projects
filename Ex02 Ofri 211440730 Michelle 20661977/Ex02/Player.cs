using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    internal class Player
    {
        internal string Name { get;}
        private ePlayerType m_PlayerType;
        internal uint Score { get; set; }
        internal Player(string i_Name, ePlayerType i_PlayerType){
            this.Name = i_Name;
            this.m_PlayerType = i_PlayerType;
        }
    }
}
