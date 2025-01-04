using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex03
{
    internal class Truck : Vehicle
    {
        const int k_TruckNumOfWheels = 14;
        const float k_TruckMaxWheelAirPressure = 29;
        const eEnergySourceType k_TruckEnergySourceType = eEnergySourceType.Fuel;
        const eFuelType k_TruckFuelType = eFuelType.Soler;
        const float k_TruckEnergyMaxCapacity = 125;
        internal bool m_TransportingRegrigeratedMaterials;
        internal float m_CargoVolume;
   
        public Truck(bool i_TransportingRegrigeratedMaterials, float i_CargoVolume)
        {
            m_CargoVolume = i_CargoVolume;
            m_TransportingRegrigeratedMaterials = i_TransportingRegrigeratedMaterials;
            base.Type = eVehicleType.Truck;
            base.NumOfWheels = k_TruckNumOfWheels;
            base.MaxWheelAirPressure = k_TruckMaxWheelAirPressure;
            base.EnergySourceType = k_TruckEnergySourceType;
            base.FuelType = k_TruckFuelType;
            base.EnergyMaxCapacity = k_TruckEnergyMaxCapacity;
        }
        

    }
}
