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
        public const eFuelType k_FuelMotorcycleFuelType = eFuelType.Octan98;
        const string k_EngineVolumeField = "Engine volume";
        const string k_LicenseTypeField = "License type";
        internal int m_EngineVolume;
        internal eMotorcycleLicenseType m_LicenseType;

        public Motorcycle(VehicleFactory.eVehicleType i_VehicleType,
            CustomerInfo i_CostumerInfo,
            string i_Model,
            string i_LicensePlate,
            float i_CurrentEnergy) : base(i_CostumerInfo, i_Model, i_LicensePlate)
        {
            Type = i_VehicleType;
            NumOfWheels = k_MotorcycleNumOfWheels;
            MaxWheelAirPressure = k_MotorcycleMaxWheelAirPressure;

            if(Type == VehicleFactory.eVehicleType.ElectricMotorcycle)
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
            m_EngineVolume = int.Parse(i_AddedFields[k_EngineVolumeField]);
            m_LicenseType = (eMotorcycleLicenseType)Enum.Parse(typeof(eMotorcycleLicenseType),i_AddedFields[k_LicenseTypeField]);
        }

        public override Dictionary<string, Type> GetAddedFields()
        {
            Dictionary<string, Type> addedFields = new Dictionary<string, Type>
            {
                { k_EngineVolumeField, typeof(int) },
                { k_LicenseTypeField, typeof(eMotorcycleLicenseType) }
            };
            
            return addedFields;
        }
    }
}
