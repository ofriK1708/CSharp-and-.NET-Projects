using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex03
{
    internal class Motorcycle : Vehicle
    {
        const int k_MotorcycleWheels = 2;
        const float k_MotorcycleMaxWheelAirPressure = 32;
        const float k_ElectricMotorcycleMaxEnergy = 2.9f;
        const float k_FuelMotorcycleMaxEnergy = 6.2f;
        const eFuelType k_ElectricMotorcycleFuelType = eFuelType.Battery;
        const eFuelType k_FuelMotorcycleFuelType = eFuelType.Octan98;
        internal int m_EngineVolume;
        internal eMotorcycleLicenseType m_LicenseType;

        public Motorcycle(eEnergySourceType i_EnergySourceType, int i_EngineVolume, eMotorcycleLicenseType i_LicenseType)
        {
            m_EngineVolume = i_EngineVolume;
            m_LicenseType = i_LicenseType;
            base.Type = eVehicleType.MotorCycle;
            base.NumOfWheels = k_MotorcycleWheels;
            base.MaxWheelAirPressure = k_MotorcycleMaxWheelAirPressure;
            base.EnergySourceType = i_EnergySourceType;
            base.EnergyMaxCapacity = (i_EnergySourceType == eEnergySourceType.Electric ? k_ElectricMotorcycleMaxEnergy : k_FuelMotorcycleMaxEnergy);
            base.FuelType = i_EnergySourceType == eEnergySourceType.Electric ? k_ElectricMotorcycleFuelType : k_FuelMotorcycleFuelType;
        }
    }
}
