﻿using System;
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
        public const eFuelType k_ElectricMotorcycleFuelType = eFuelType.Battery;
        public const eFuelType k_FuelMotorcycleFuelType = eFuelType.Octan98;
        internal int m_EngineVolume;
        internal eMotorcycleLicenseType m_LicenseType;

        public Motorcycle(CustomerInfo i_CostumerInfo,
            string i_Model,
            string i_LicensePlate,
            float i_CurrentEnergy,
            eEnergySourceType i_EnergySourceType) : base(i_CostumerInfo, i_Model, i_LicensePlate)
        {
            base.Type = VehicleFactory.eVehicleType.MotorCycle;
            base.NumOfWheels = k_MotorcycleNumOfWheels;
            base.MaxWheelAirPressure = k_MotorcycleMaxWheelAirPressure;
            base.EnergySourceType = i_EnergySourceType;
            base.EnergyMaxCapacity = (i_EnergySourceType == eEnergySourceType.Electric ? k_ElectricMotorcycleMaxEnergy : k_FuelMotorcycleMaxEnergy);
            base.FuelType = i_EnergySourceType == eEnergySourceType.Electric ? k_ElectricMotorcycleFuelType : k_FuelMotorcycleFuelType;
            base.CurrentEnergyCapacity = i_CurrentEnergy;
        }
        
        public override string ToString()
        {
            StringBuilder motorcycleDetails = new StringBuilder();

            motorcycleDetails.AppendLine(string.Format("License Plate: {0}", LicensePlate));
            motorcycleDetails.AppendLine(string.Format("Model: {0}", Model));
            motorcycleDetails.AppendLine(string.Format("Owner Name: {0}", m_CostumerInfo.CustomerName));
            motorcycleDetails.AppendLine(string.Format("Owner Phone Number: {0}", m_CostumerInfo.CustomerPhoneNumber));
            motorcycleDetails.AppendLine(string.Format("State In Garage: {0}", VehicleState));
            motorcycleDetails.AppendLine(string.Format("Engine Volume: {0}", m_EngineVolume));
            motorcycleDetails.AppendLine(string.Format("License Type: {0}", m_LicenseType));
            motorcycleDetails.AppendLine(string.Format("Energy Source Type: {0}", EnergySourceType));
            motorcycleDetails.AppendLine(string.Format("Current Energy Capacity: {0}", CurrentEnergyCapacity));
            motorcycleDetails.AppendLine(string.Format("Energy Percentage: {0}%", EnergyPercentage));
            motorcycleDetails.AppendLine(string.Format("Number of Wheels: {0}", NumOfWheels));
            motorcycleDetails.AppendLine(string.Format("Max Wheel Air Pressure: {0}", MaxWheelAirPressure));

            for (int i = 0; i < m_Wheels.Length; i++)
            {
                motorcycleDetails.AppendLine(string.Format("Wheel {0}#: {1}", i + 1, m_Wheels[i].ToString()));
            }

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
            List<string> addedFields = new List<string>();
            addedFields.Add("EngineVolume");
            addedFields.Add("LicenseType");
            return addedFields;
        }
    }
}
