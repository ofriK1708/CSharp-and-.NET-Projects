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

        public void ChangeVehicleStatus(string i_LicensePlate, eVehicleStatus iNewVehicleStatus)
        {
            m_Vehicles[i_LicensePlate].VehicleStatus = iNewVehicleStatus;
        }

        public List<string> GetAllLicensePlates()
        {
            return new List<string>(m_Vehicles.Keys);
        }

        public List<string> GetLicensePlatesByVehicleStatus(eVehicleStatus iVehicleStatus)
        {
            List<string> licensePlates = new List<string>();
            foreach (Vehicle vehicle in m_Vehicles.Values)
            {
                if (vehicle.VehicleStatus == iVehicleStatus)
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

        public void ChargeBattery(string i_LicensePlate, float i_HoursToCharge)
        {
            m_Vehicles[i_LicensePlate].ChargeBattery(i_HoursToCharge);
        }

        public string GetFullVehicleDetails(string i_LicensePlate)
        {
            return m_Vehicles[i_LicensePlate].ToString();
        }
    }
}