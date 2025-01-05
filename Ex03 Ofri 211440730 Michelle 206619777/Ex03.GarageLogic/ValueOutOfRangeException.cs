using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex03
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;
        
        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue,string i_Massage) : 
            base(string.Format("{0}, it should be between {1} and {2}",i_Massage ,i_MinValue, i_MaxValue))
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }
    }
}
