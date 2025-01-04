using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex03
{
    internal class Motorcycle : Vehicle
    {
        internal int m_EngineVolume;
        internal eMotorcycleLicenseType m_LicenseType;

        public Motorcycle(eEnergySourceType i_EnergySourceType, int i_EngineVolume, eMotorcycleLicenseType i_LicenseType)
        {
            m_EngineVolume = i_EngineVolume;
            m_LicenseType = i_LicenseType;
            base.Type = eVehicleType.MotorCycle;
            base.NumOfWheels = 2;
            base.MaxWheelAirPressure = 32;
            base.EnergySourceType = i_EnergySourceType;
            base.EnergyMaxCapacity = (float)(i_EnergySourceType == eEnergySourceType.Electric ? 2.9 : 6.2);
            base.FuelType = i_EnergySourceType == eEnergySourceType.Electric ? eFuelType.Battery : eFuelType.Octan98;
        }
    }
}
