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

            truckDetails.AppendLine(string.Format("License Plate: {0}", LicensePlate));
            truckDetails.AppendLine(string.Format("Model: {0}", Model));
            truckDetails.AppendLine(string.Format("Owner Name: {0}", m_CostumerInfo.CustomerName));
            truckDetails.AppendLine(string.Format("Owner Phone Number: {0}", m_CostumerInfo.CustomerPhoneNumber));
            truckDetails.AppendLine(string.Format("State In Garage: {0}", VehicleState));
            truckDetails.AppendLine(string.Format("{0}Carrying Hazardous Materials", m_IsTransportingRefrigeratedMaterials ? "" : "not "));
            truckDetails.AppendLine(string.Format("Cargo Volume: {0}", m_CargoVolume));
            truckDetails.AppendLine(EnergySource.ToString());
            truckDetails.AppendLine(string.Format("Number of Wheels: {0}", NumOfWheels));
            truckDetails.AppendLine(string.Format("Max Wheel Air Pressure: {0}", MaxWheelAirPressure));

            for (int i = 0; i < m_Wheels.Length; i++)
            {
                truckDetails.AppendLine(string.Format("Wheel {0}#: {1}", i + 1, m_Wheels[i].ToString()));
            }

            return truckDetails.ToString();
        }

        public override void SetAddedFields(Dictionary<string, string> i_AddedFields)
        {
            //todo - add check is defined
            m_IsTransportingRefrigeratedMaterials = bool.Parse(i_AddedFields["IsTransportingRefrigeratedMaterials"]);
            m_CargoVolume = float.Parse(i_AddedFields["CargoVolume"]);
        }

        public override List<string> GetAddedFields()
        {
           List<string> addedFields = new List<string>
           {
               "IsTransportingRefrigeratedMaterials",
               "CargoVolume"
           };
            return addedFields;
        }
    }
}
