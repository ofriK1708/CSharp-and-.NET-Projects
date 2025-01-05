using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex03
{
    public struct Wheel
    {
        internal string ManufacturerName { get; set; }
        internal float?  MaxAirPressure { get; set; }
        private float m_CurrentAirPressure;
        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                if (value < 0 || value > MaxAirPressure.Value)
                {
                    throw new ValueOutOfRangeException(0, MaxAirPressure.Value, "Current air pressure is out of the valid range");
                }
                else
                {
                    m_CurrentAirPressure = value;
                }
            }
        }

        public Wheel(string i_ManufacturerName,float i_MaxMaxAirPressure,float i_CurentAirPressure) 
        {
            ManufacturerName = i_ManufacturerName;
            MaxAirPressure = i_MaxMaxAirPressure;
            CurrentAirPressure = i_CurentAirPressure;

        }
        
        
    }
}
