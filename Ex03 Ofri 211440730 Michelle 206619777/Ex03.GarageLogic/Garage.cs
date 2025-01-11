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

        public void ChangeVehicleStatus(string i_LicensePlate, eVehicleState i_NewVehicleState)
        {
            m_Vehicles[i_LicensePlate].VehicleState = i_NewVehicleState;
        }

        public void ChangeVehicleStatusToInRepair(string i_LicensePlate)
        {
            ChangeVehicleStatus(i_LicensePlate, eVehicleState.InRepair);
        }

        public List<string> GetAllLicensePlates()
        {
            return new List<string>(m_Vehicles.Keys);
        }

        public List<string> GetLicensePlatesByState(eVehicleState i_VehicleState)
        {
            List<string> licensePlates = new List<string>();
            foreach (Vehicle vehicle in m_Vehicles.Values)
            {
                if (vehicle.VehicleState == i_VehicleState)
                {
                    licensePlates.Add(vehicle.LicensePlate);
                }
            }

            return licensePlates;
        }

        public void FillWheelsAirToMax(string i_LicensePlate)
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
            m_Vehicles[i_LicensePlate].ChargeBattery(i_MinutesToCharge);
        }

        public string GetFullVehicleDetails(string i_LicensePlate)
        {
            return m_Vehicles[i_LicensePlate].ToString();
        }
    }
}