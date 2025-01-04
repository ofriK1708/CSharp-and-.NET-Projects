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
        const int k_CarNumOfWheels = 4;
        const float k_CarMaxWheelAirPressure = 34f;
        const float k_ElectricCarMaxEnergyCapacity = 5.4f;
        const float k_FuelCarMaxEnergyCapacity = 52f;
        const eFuelType k_ElectricCarFuelType = eFuelType.Battery;
        const eFuelType k_FuelCarFuelType = eFuelType.Octan95;
        internal eCarColor m_Color;
        internal eCarDoorsNum m_DoorsNum;

        public Car(eEnergySourceType i_EnergySourceType, eCarColor i_CarColor, eCarDoorsNum i_CarDoorNum)
        {
            m_Color = i_CarColor;
            m_DoorsNum = i_CarDoorNum;
            base.Type = eVehicleType.Car;
            base.NumOfWheels = k_CarNumOfWheels;
            base.MaxWheelAirPressure = k_CarMaxWheelAirPressure;
            base.EnergySourceType = i_EnergySourceType;
            base.EnergyMaxCapacity = (float)(i_EnergySourceType == eEnergySourceType.Electric ? k_ElectricCarMaxEnergyCapacity : k_FuelCarMaxEnergyCapacity);
            base.FuelType = i_EnergySourceType == eEnergySourceType.Electric ? k_ElectricCarFuelType : k_FuelCarFuelType;
        }
    }
}
