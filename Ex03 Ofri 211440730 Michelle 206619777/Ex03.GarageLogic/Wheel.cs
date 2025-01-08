using System;
using System.Text;

namespace ex03
{
    public class Wheel
    {
        internal string ManufacturerName { get; }

        internal float MaxAirPressure { get; }

        internal float CurrentAirPressure { get; private set; }


        public Wheel(string i_ManufacturerName, float i_MaxAirPressure, float i_CurentAirPressure)
        {
            ManufacturerName = i_ManufacturerName;

            if(i_MaxAirPressure <= 0)
            {
                throw new ArgumentException("Max air pressure must be positive");
            }

            MaxAirPressure = i_MaxAirPressure;

            if(i_CurentAirPressure > i_MaxAirPressure || i_CurentAirPressure < 0)
            {
                throw new ValueOutOfRangeException(0, i_MaxAirPressure, "Wheel Air Pressure is out of range");
            }

            CurrentAirPressure = i_CurentAirPressure;
        }

        internal void fillAirPressureToMax()
        {
            CurrentAirPressure = MaxAirPressure;
        }

        public override string ToString()
        {
            StringBuilder wheelDetails = new StringBuilder();

            wheelDetails.AppendLine(string.Format("Manufacturer: {0}", ManufacturerName));
            wheelDetails.AppendLine(string.Format("Current Air Pressure: {0}", CurrentAirPressure));
            wheelDetails.AppendLine(string.Format("Max Air Pressure: {0}", MaxAirPressure));

            return wheelDetails.ToString();
        }
    }
}