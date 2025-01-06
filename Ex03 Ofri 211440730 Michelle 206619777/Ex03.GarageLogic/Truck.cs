using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex03
{
    public class Truck : Vehicle
    {
        public const int k_TruckNumOfWheels = 14;
        public const float k_TruckMaxWheelAirPressure = 29;
        public const eEnergySourceType k_TruckEnergySourceType = eEnergySourceType.Fuel;
        public const eFuelType k_TruckFuelType = eFuelType.Soler;
        public const float k_TruckEnergyMaxCapacity = 125;
        internal bool m_TransportingRegrigeratedMaterials;
        internal float m_CargoVolume;
   
        public Truck(CustomerInfo i_costumerInfo,string i_Model,string i_LicensePlate, float i_CurrentEnergyCapacity, Wheel[] i_TruckWheels,
            bool i_TransportingRegrigeratedMaterials, float i_CargoVolume) : base(i_costumerInfo, i_Model, i_LicensePlate)
        {
            m_CargoVolume = i_CargoVolume;
            m_TransportingRegrigeratedMaterials = i_TransportingRegrigeratedMaterials;
            base.Type = eVehicleType.Truck;
            base.NumOfWheels = k_TruckNumOfWheels;
            base.MaxWheelAirPressure = k_TruckMaxWheelAirPressure;
            base.EnergySourceType = k_TruckEnergySourceType;
            base.FuelType = k_TruckFuelType;
            base.EnergyMaxCapacity = k_TruckEnergyMaxCapacity;
            if (i_CurrentEnergyCapacity > EnergyMaxCapacity || i_CurrentEnergyCapacity < 0)
            {
                throw new ValueOutOfRangeException(0, EnergyMaxCapacity.Value, "Current energy amount is out of the valid range");
            }
            else
            {
                base.m_CurrentEnergyCapacity = i_CurrentEnergyCapacity;
            }

        }
        

    }
}
