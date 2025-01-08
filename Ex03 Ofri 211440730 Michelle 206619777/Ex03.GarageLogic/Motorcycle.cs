using System;
using System.Collections.Generic;
using System.Text;

namespace ex03
{
    public class Motorcycle : Vehicle
    {
        public const int k_MotorcycleNumOfWheels = 2;
        public const float k_MotorcycleMaxWheelAirPressure = 32;
        public const float k_ElectricMotorcycleMaxEnergy = 2.9f;
        public const float k_FuelMotorcycleMaxEnergy = 6.2f;
        public const eFuelType k_FuelMotorcycleFuelType = eFuelType.Octan98;
        internal int m_EngineVolume;
        internal eMotorcycleLicenseType m_LicenseType;

        public Motorcycle(CustomerInfo i_CostumerInfo,
            string i_Model,
            string i_LicensePlate,
            float i_CurrentEnergy,
            eEnergySourceType i_EnergySourceType) : base(i_CostumerInfo, i_Model, i_LicensePlate)
        {
            Type = VehicleFactory.eVehicleType.MotorCycle;
            NumOfWheels = k_MotorcycleNumOfWheels;
            MaxWheelAirPressure = k_MotorcycleMaxWheelAirPressure;
            if(i_EnergySourceType == eEnergySourceType.Electric)
            {
                EnergySource = new ElectricMotor(k_ElectricMotorcycleMaxEnergy, i_CurrentEnergy);
            }
            else
            {
                EnergySource = new GasEngine(k_FuelMotorcycleMaxEnergy, i_CurrentEnergy, k_FuelMotorcycleFuelType);
            }
        }
        
        public override string ToString()
        {
            StringBuilder motorcycleDetails = new StringBuilder();

            motorcycleDetails.AppendLine("Motorcycle Details: ");
            motorcycleDetails.AppendLine(base.ToString());
            motorcycleDetails.AppendLine(string.Format("Engine Volume: {0}", m_EngineVolume));
            motorcycleDetails.AppendLine(string.Format("License Type: {0}", m_LicenseType));

            return motorcycleDetails.ToString();
        }

        public override void SetAddedFields(Dictionary<string, string> i_AddedFields)
        {
            m_EngineVolume = int.Parse(i_AddedFields["EngineVolume"]);
            //todo - add check is defined
            m_LicenseType = (eMotorcycleLicenseType)Enum.Parse(typeof(eMotorcycleLicenseType),i_AddedFields["LicenseType"]);
        }

        public override List<string> GetAddedFields()
        {
            List<string> addedFields = new List<string> { "EngineVolume", "LicenseType" };
            return addedFields;
        }
    }
}
