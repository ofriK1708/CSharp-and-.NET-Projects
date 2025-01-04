using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex03
{
    internal class Truck : Vehicle
    {
        internal bool m_TransportingRegrigeratedMaterials;
        internal float m_CargoVolume;
   
        public Truck(bool i_TransportingRegrigeratedMaterials, float i_CargoVolume)
        {
            m_CargoVolume = i_CargoVolume;
            m_TransportingRegrigeratedMaterials = i_TransportingRegrigeratedMaterials;
            base.Type = eVehicleType.Truck;
            base.NumOfWheels = 14;
            base.MaxWheelAirPressure = 29;
            base.EnergySourceType = eEnergySourceType.Fuel;
            base.FuelType = eFuelType.Soler;
            base.EnergyMaxCapacity = 125;
        }
    }
}
