using System.Collections.Generic;

namespace ex03
{
    public class Garage
    {
        public Dictionary<string, Vehicle> m_Vehicles = new Dictionary<string, Vehicle>();

        public bool IsVehicleInTheGarage(string i_LicensePlate)
        {
            return m_Vehicles.ContainsKey(i_LicensePlate);
        }

        public void AddVehicle(Vehicle i_Vehicle)
        {
            m_Vehicles.Add(i_Vehicle.LicensePlate, i_Vehicle);
        }

        public void ChangeVehicleStatus(string i_LicensePlate, eVehicleStatus i_NewVehicleStatus)
        {
            m_Vehicles[i_LicensePlate].VehicleStatus = i_NewVehicleStatus;
        }

        public List<string> GetAllLicensePlates()
        {
            return new List<string>(m_Vehicles.Keys);
        }

        public List<string> GetLicensePlatesByVehicleStatus(eVehicleStatus i_VehicleStatus)
        {
            List<string> licensePlates = new List<string>();
            foreach (Vehicle vehicle in m_Vehicles.Values)
            {
                if (vehicle.VehicleStatus == i_VehicleStatus)
                {
                    licensePlates.Add(vehicle.LicensePlate);
                }
            }

            return licensePlates;
        }
        
        public void FillWheelsAirPressureToMax(string i_LicensePlate)
        {
            m_Vehicles[i_LicensePlate].FillWheelsAirToMax();
        }

        public void RefuelVehicle(string i_LicensePlate, float i_EnergyToAdd, eFuelType i_FuelType)
        {
            m_Vehicles[i_LicensePlate].FillGas(i_EnergyToAdd, i_FuelType);
        }

        public void ChargeBattery(string i_LicensePlate, float i_MinutesToCharge)
        {
            m_Vehicles[i_LicensePlate].ChargeBattery(i_MinutesToCharge * ElectricMotor.k_MinutesInHour);
        }

        public string GetFullVehicleDetails(string i_LicensePlate)
        {
            return m_Vehicles[i_LicensePlate].ToString();
        }
    }
}