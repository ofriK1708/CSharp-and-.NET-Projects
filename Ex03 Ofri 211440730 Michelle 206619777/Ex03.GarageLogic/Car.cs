using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ex03
{
    internal class Car : Vehicle
    {
        internal eCarColor m_Color;
        internal eCarDoorNum m_DoorsNum;

        public Car(eEnergySourceType i_EnergySourceType, eCarColor i_CarColor, eCarDoorNum i_CarDoorNum)
        {
            m_Color = i_CarColor;
            m_DoorsNum = i_CarDoorNum;
            base.Type = eVehicleType.Car;
            base.NumOfWheels = 5;
            base.MaxWheelAirPressure = 34;
            base.EnergySourceType = i_EnergySourceType;
            base.EnergyMaxCapacity = (float)(i_EnergySourceType == eEnergySourceType.Electric ? 5.4 : 52.0);
            base.FuelType = i_EnergySourceType == i_eEnergySourceType.Electric ? eFuelType.Battery : eFuelType.Octan95;
        }
    }
}
