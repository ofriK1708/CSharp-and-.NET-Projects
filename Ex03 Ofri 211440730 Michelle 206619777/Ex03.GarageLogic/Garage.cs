using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex03
{
    public class Garage
    {
        Dictionary<String, Vehicle> m_Vehicles;

        public bool IsVehicleInTheGarage(String i_LicensePlate)
        {
            return m_Vehicles.ContainsKey(i_LicensePlate);
        }

        public void AddVehicle(Vehicle i_Vehicle)
        {
            m_Vehicles.Add(i_Vehicle.LicensePlate, i_Vehicle);
        }

        public void ChangeVehicleStatus(String i_LicensePlate, eVehicleState i_newVheicleState)
        {
            m_Vehicles[i_LicensePlate].VehicleState = i_newVheicleState;
        }
        public void ChangeVehicleStatusToInRepare(String i_LicensePlate)
        {
            ChangeVehicleStatus(i_LicensePlate, eVehicleState.InRepair);
        }
        public void fillWheelsAirToMax(string i_LicensePlate)
        {
            m_Vehicles[i_LicensePlate].FillWheelsAirToMax();
        }
        public void fillFuel(string i_LicensePlate, float i_EnergyToAdd,eFuelType eFuelType) 
        {
            m_Vehicles[i_LicensePlate].FillEnergy(i_EnergyToAdd, eFuelType);
        }
        public void chargeBattery(string i_LicensePlate, float i_minutesToCharge)
        {
            m_Vehicles[i_LicensePlate].FillEnergy(i_minutesToCharge, eFuelType.Battery);
        }
        string getFullVehicleDetails(string i_LicensePlate)
        {
            return m_Vehicles[i_LicensePlate].ToString();
        }

    }
}
