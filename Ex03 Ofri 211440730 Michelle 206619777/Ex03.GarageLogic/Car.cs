using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ex03
{
    public class Car : Vehicle
    {
        public const int k_CarNumOfWheels = 4;
        public const float k_CarMaxWheelAirPressure = 34f;
        public const float k_ElectricCarMaxEnergyCapacity = 5.4f;
        public const float k_FuelCarMaxEnergyCapacity = 52f;
        public const eFuelType k_ElectricCarFuelType = eFuelType.Battery;
        public const eFuelType k_FuelCarFuelType = eFuelType.Octan95;
        internal eCarColor m_Color;
        internal eCarDoorsNum m_DoorsNum;

        public Car(CustomerInfo i_CostumerAndVehicleInfo, string i_Model, string i_LicensePlate, eEnergySourceType i_EnergySourceType,
            float i_CurrentEnergy, eCarColor i_CarColor, eCarDoorsNum i_CarDoorNum) : base(i_CostumerAndVehicleInfo,i_Model,i_LicensePlate)
        {
            m_Color = i_CarColor;
            m_DoorsNum = i_CarDoorNum;
            base.Type = eVehicleType.Car;
            base.NumOfWheels = k_CarNumOfWheels;
            base.m_Wheels = new Wheel[k_CarNumOfWheels];
            base.MaxWheelAirPressure = k_CarMaxWheelAirPressure;
            base.EnergySourceType = i_EnergySourceType;
            base.EnergyMaxCapacity = (float)(i_EnergySourceType == eEnergySourceType.Electric ? k_ElectricCarMaxEnergyCapacity : k_FuelCarMaxEnergyCapacity);
            base.FuelType = i_EnergySourceType == eEnergySourceType.Electric ? k_ElectricCarFuelType : k_FuelCarFuelType;
            if(i_CurrentEnergy > base.EnergyMaxCapacity || i_CurrentEnergy < 0)
            {
                throw new ValueOutOfRangeException(0, base.EnergyMaxCapacity, "Current energy level is out of the valid range");
            }
            else
            {
                base.m_CurrentEnergyCapacity = i_CurrentEnergy;
            }
        }
        public static void ValidateEnergyAmount(eEnergySourceType i_EnergyType,float i_CurrentEnergy)
        {
            if(i_EnergyType == eEnergySourceType.Electric && (i_CurrentEnergy < 0 || i_CurrentEnergy > k_ElectricCarMaxEnergyCapacity))
            {
                throw new ValueOutOfRangeException(0, k_ElectricCarMaxEnergyCapacity, "Current electrical level is out of the valid range");
            }
            else if (i_EnergyType == eEnergySourceType.Fuel && (i_CurrentEnergy < 0 || i_CurrentEnergy > k_FuelCarMaxEnergyCapacity))
            {
                throw new ValueOutOfRangeException(0, k_FuelCarMaxEnergyCapacity, "Current fuel amount is out of the valid range");
            }
        }
    }
}
