using System;

namespace ex03
{
    public class VehicleFactory
    {
        public enum eVehicleType
        {
            Car = 1,
            MotorCycle = 2,
            Truck = 3
        }

        public Vehicle CreateVehicle(eVehicleType i_VehicleType,
            CustomerInfo i_CostumerAndVehicleInfo,
            string i_Model,
            string i_LicensePlate,
            eEnergySourceType i_EnergySourceType,
            float i_CurrentEnergy)
        {
            Vehicle vehicle;
            switch (i_VehicleType)
            {
                case eVehicleType.Car:
                    vehicle = createCar(i_CostumerAndVehicleInfo, i_Model, i_LicensePlate, i_EnergySourceType, i_CurrentEnergy);
                    break;
                case eVehicleType.MotorCycle:
                    vehicle = createMotorcycle(i_CostumerAndVehicleInfo, i_Model, i_LicensePlate, i_CurrentEnergy, i_EnergySourceType);
                    break;
                case eVehicleType.Truck:
                    vehicle = createTruck(i_CostumerAndVehicleInfo, i_Model, i_LicensePlate, i_CurrentEnergy);
                    break;
                default:
                    throw new ArgumentException("Vehicle type is not supported");
            }
            return vehicle;
        }

        private Car createCar(CustomerInfo i_CostumerAndVehicleInfo,
            string i_Model,
            string i_LicensePlate,
            eEnergySourceType i_EnergySourceType,
            float i_CurrentEnergy)
        {
            return new Car(i_CostumerAndVehicleInfo, i_Model, i_LicensePlate, i_EnergySourceType, i_CurrentEnergy);
        }

        private Truck createTruck(CustomerInfo i_CostumerInfo, string i_Model, string i_LicensePlate, float i_CurrentEnergy)
        {
            return new Truck(i_CostumerInfo, i_Model, i_LicensePlate, i_CurrentEnergy);
        }

        private Motorcycle createMotorcycle(CustomerInfo i_CostumerAndVehicleInfo,
            string i_Model,
            string i_LicensePlate,
            float i_CurrentEnergy,
            eEnergySourceType i_EnergySourceType)
        {
            return new Motorcycle(i_CostumerAndVehicleInfo, i_Model, i_LicensePlate, i_CurrentEnergy, i_EnergySourceType);
        }
    }
}