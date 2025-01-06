namespace ex03
{
    public class VehicleFactory
    {
        public Car createCar(CustomerInfo i_CostumerAndVehicleInfo, string i_Model, string i_LicensePlate, eEnergySourceType i_EnergySourceType,
            float i_CurrentEnergy, Wheel[] i_CarWheels, eCarColor i_CarColor, eCarDoorsNum i_CarDoorNum)
        {
            return new Car(i_CostumerAndVehicleInfo, i_Model, i_LicensePlate, i_EnergySourceType, i_CurrentEnergy, i_CarWheels, i_CarColor, i_CarDoorNum);
        }

        public Truck createTruck(CustomerInfo i_costumerInfo, string i_Model, string i_LicensePlate, float i_CurrentEnergy, Wheel[] i_TruckWheels,
            bool i_TransportingRegrigeratedMaterials, float i_CargoVolume)
        {
            return new Truck(i_costumerInfo, i_Model, i_LicensePlate, i_CurrentEnergy, i_TruckWheels, i_TransportingRegrigeratedMaterials, i_CargoVolume);
        }

        public Motorcycle createMotorcycle(CustomerInfo i_CostumerAndVehicleInfo, string i_Model, string i_LicensePlate, eEnergySourceType i_EnergySourceType,
            float i_CurrentEnergy, Wheel[] i_MotorcycleWheels, eMotorcycleLicenseType i_MotorcycleLicenseType, int i_EngineVolume)
        {
            return new Motorcycle(i_CostumerAndVehicleInfo, i_Model, i_LicensePlate, i_CurrentEnergy, i_MotorcycleWheels, i_EnergySourceType, i_EngineVolume, i_MotorcycleLicenseType);
        }
    }
}
