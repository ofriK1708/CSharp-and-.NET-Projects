using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    internal class Player
    {
        private string Name { get; set;}
        private ePlayerType m_PlayerType;

        public Player(string i_Name, ePlayerType i_PlayerType){
            this.Name = i_Name;
            this.m_PlayerType = i_PlayerType;
        }
    }
}
