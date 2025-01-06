using System.Text;

namespace ex03
{
    public class Truck : Vehicle
    {
        public const int k_TruckNumOfWheels = 14;
        public const float k_TruckMaxWheelAirPressure = 29;
        public const eEnergySourceType k_TruckEnergySourceType = eEnergySourceType.Fuel;
        public const eFuelType k_TruckFuelType = eFuelType.Soler;
        public const float k_TruckEnergyMaxCapacity = 125;
        internal bool m_IsTransportingRefrigeratedMaterials;
        internal float m_CargoVolume;
   
        public Truck(CustomerInfo i_costumerInfo,string i_Model,string i_LicensePlate, float i_CurrentEnergy, Wheel[] i_TruckWheels,
            bool i_TransportingRegrigeratedMaterials, float i_CargoVolume) : base(i_costumerInfo, i_Model, i_LicensePlate)
        {
            m_CargoVolume = i_CargoVolume;
            m_IsTransportingRefrigeratedMaterials = i_TransportingRegrigeratedMaterials;
            base.Type = eVehicleType.Truck;
            base.NumOfWheels = k_TruckNumOfWheels;
            base.MaxWheelAirPressure = k_TruckMaxWheelAirPressure;
            base.EnergySourceType = k_TruckEnergySourceType;
            base.FuelType = k_TruckFuelType;
            base.EnergyMaxCapacity = k_TruckEnergyMaxCapacity;
            base.CurrentEnergyCapacity = i_CurrentEnergy;

            validateWheels(i_TruckWheels, eVehicleType.Car, k_TruckNumOfWheels);
            base.m_Wheels = i_TruckWheels;
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
            truckDetails.AppendLine(string.Format("Energy Source Type: {0}", EnergySourceType));
            truckDetails.AppendLine(string.Format("Current Energy Capacity: {0}", CurrentEnergyCapacity));
            truckDetails.AppendLine(string.Format("Energy Percentage: {0}%", EnergyPrecentage));
            truckDetails.AppendLine(string.Format("Number of Wheels: {0}", NumOfWheels));
            truckDetails.AppendLine(string.Format("Max Wheel Air Pressure: {0}", MaxWheelAirPressure));

            for (int i = 0; i < m_Wheels.Length; i++)
            {
                truckDetails.AppendLine(string.Format("Wheel {0}#: {1}", i + 1, m_Wheels[i].ToString()));
            }

            return truckDetails.ToString();
        }
    }
}
