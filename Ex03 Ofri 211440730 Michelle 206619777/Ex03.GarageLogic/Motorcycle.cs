﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Motorcycle(CustomerInfo i_CostumerInfo,string i_Model,string i_LicensePlate, float i_CurrentEnergy, Wheel[] i_MotorcycleWheels,
            eEnergySourceType i_EnergySourceType, int i_EngineVolume, eMotorcycleLicenseType i_LicenseType) : base(i_CostumerInfo, i_Model, i_LicensePlate)
        {
            m_EngineVolume = i_EngineVolume;
            m_LicenseType = i_LicenseType;
            base.Type = eVehicleType.MotorCycle;
            base.NumOfWheels = k_MotorcycleNumOfWheels;
            base.MaxWheelAirPressure = k_MotorcycleMaxWheelAirPressure;
            base.EnergySourceType = i_EnergySourceType;
            base.EnergyMaxCapacity = (i_EnergySourceType == eEnergySourceType.Electric ? k_ElectricMotorcycleMaxEnergy : k_FuelMotorcycleMaxEnergy);
            base.FuelType = i_EnergySourceType == eEnergySourceType.Electric ? k_ElectricMotorcycleFuelType : k_FuelMotorcycleFuelType;
            base.CurrentEnergyCapacity = i_CurrentEnergy;
            validateWheels(i_MotorcycleWheels, eVehicleType.Car, k_MotorcycleNumOfWheels);
            base.m_Wheels = i_MotorcycleWheels;
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
            motorcycleDetails.AppendLine(string.Format("Energy Percentage: {0}%", EnergyPrecentage));
            motorcycleDetails.AppendLine(string.Format("Number of Wheels: {0}", NumOfWheels));
            motorcycleDetails.AppendLine(string.Format("Max Wheel Air Pressure: {0}", MaxWheelAirPressure));

            for (int i = 0; i < m_Wheels.Length; i++)
            {
                motorcycleDetails.AppendLine(string.Format("Wheel {0}#: {1}", i + 1, m_Wheels[i].ToString()));
            }

            return motorcycleDetails.ToString();
        }
    }
}
