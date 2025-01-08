using System;
using System.Collections.Generic;
using System.Text;

namespace ex03
{
    public class Truck : Vehicle
    {
        public const int k_TruckNumOfWheels = 14;
        public const float k_TruckMaxWheelAirPressure = 29;
        public const eFuelType k_TruckFuelType = eFuelType.Soler;
        public const float k_TruckEnergyMaxCapacity = 125;
        internal bool m_IsTransportingRefrigeratedMaterials;
        const string k_IsTransportingRefrigeratedMaterialsField = "Is transporting refrigerated materials";
        const string k_CargoVolumeField = "Cargo volume";
        internal float m_CargoVolume;
   
        public Truck(CustomerInfo i_CostumerInfo,
            string i_Model,
            string i_LicensePlate,
            float i_CurrentEnergy) : base(i_CostumerInfo, i_Model, i_LicensePlate)
        {
            Type = VehicleFactory.eVehicleType.Truck;
            NumOfWheels = k_TruckNumOfWheels;
            MaxWheelAirPressure = k_TruckMaxWheelAirPressure;
            EnergySource = new GasEngine(k_TruckEnergyMaxCapacity, i_CurrentEnergy, k_TruckFuelType);
        }
        
        public override string ToString()
        {
            StringBuilder truckDetails = new StringBuilder();

            truckDetails.AppendLine("Truck Details: ");
            truckDetails.AppendLine(base.ToString());
            truckDetails.AppendLine(string.Format("{0}Carrying Hazardous Materials", m_IsTransportingRefrigeratedMaterials ? "" : "not "));
            truckDetails.AppendLine(string.Format("Cargo Volume: {0}", m_CargoVolume));
         
            return truckDetails.ToString();
        }

        public override void SetAddedFields(Dictionary<string, string> i_AddedFields)
        {
            m_IsTransportingRefrigeratedMaterials = bool.Parse(i_AddedFields[k_IsTransportingRefrigeratedMaterialsField]);
            m_CargoVolume = float.Parse(i_AddedFields[k_CargoVolumeField]);
        }

        public override Dictionary<string, Type> GetAddedFields()
        {
            Dictionary<string, Type> addedFields = new Dictionary<string, Type>();
            addedFields.Add(k_IsTransportingRefrigeratedMaterialsField, typeof(bool));
            addedFields.Add(k_CargoVolumeField, typeof(float));
         
            return addedFields;
        }
    }
}
