using System;

namespace ex03
{
    public class VehicleFactory
    {
        public enum eVehicleType
        {
            ElectricCar = 1,
            FueledCar = 2,
            ElectricMotorcycle = 3,
            FueledMotorcycle = 4,
            Truck = 5
        }

        public Vehicle CreateVehicle(eVehicleType i_VehicleType, CustomerInfo i_CostumerAndVehicleInfo, string i_Model,
            string i_LicensePlate, float i_CurrentEnergy)
        {
            Vehicle vehicle;
            switch (i_VehicleType)
            {
                case eVehicleType.ElectricCar:
                case eVehicleType.FueledCar:
                    vehicle = new Car(i_VehicleType, i_CostumerAndVehicleInfo, i_Model, i_LicensePlate,
                        i_CurrentEnergy);
                    break;
                case eVehicleType.ElectricMotorcycle:
                case eVehicleType.FueledMotorcycle:
                    vehicle = new Motorcycle(i_VehicleType, i_CostumerAndVehicleInfo, i_Model, i_LicensePlate,
                        i_CurrentEnergy);
                    break;
                case eVehicleType.Truck:
                    vehicle = new Truck(i_CostumerAndVehicleInfo, i_Model, i_LicensePlate, i_CurrentEnergy);
                    break;
                default:
                    throw new ArgumentException("Vehicle type is not supported");
            }

            return vehicle;
        }
    }
}