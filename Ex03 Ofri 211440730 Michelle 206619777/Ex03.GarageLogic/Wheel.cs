using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex03
{
    public struct Wheel
    {
        internal string ManufacturerName { get;private set; }
        internal float? MaxAirPressure { get; private set; }
        
        internal float CurrentAirPressure { get;private set; }


        public Wheel(string i_ManufacturerName,float? i_MaxAirPressure,float i_CurentAirPressure) 
        {
            ManufacturerName = i_ManufacturerName;
            if(i_MaxAirPressure.Value <= 0)
            {
                throw new ArgumentException("Max air pressure must be positive");
            }
            MaxAirPressure = i_MaxAirPressure;
            if(i_CurentAirPressure <= i_MaxAirPressure)
            {
                CurrentAirPressure = i_CurentAirPressure;
            }
            else
            {
                throw new ValueOutOfRangeException(0, i_MaxAirPressure.Value, "Current wheel Air Pressure is higher than the max air pressure");
            }
        }
        internal void fillAirPressureToMax()
        {
            if(!MaxAirPressure.HasValue)
            {
                throw new InvalidOperationException("Max air pressure was not set");
            }
            CurrentAirPressure = MaxAirPressure.Value;
        }
        public void ValidateWheel()
        {
            if (!MaxAirPressure.HasValue)
            {
                throw new InvalidOperationException("Max air pressure was not set");
            }


            if (CurrentAirPressure > MaxAirPressure.Value || CurrentAirPressure < 0)
            {
                throw new ValueOutOfRangeException(0, MaxAirPressure.Value, "Current wheel Air Pressure is out of the valid range");
            }
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
