using System;
using System.Text;
using System.Collections.Generic;

namespace ex03
{
    public class Car : Vehicle
    {
        public const int k_CarNumOfWheels = 4;
        public const float k_CarMaxWheelAirPressure = 34f;
        public const float k_ElectricCarMaxEnergyCapacity = 5.4f;
        public const float k_FuelCarMaxEnergyCapacity = 52f;
        public const eFuelType k_ElectricCarFuelType = eFuelType.Battery;
        public const eFuelType k_FuelCarFuelType = eFuelType.Octan95;
        internal eCarColor m_Color;
        internal eCarDoorsNum m_DoorsNum;

        internal Car(CustomerInfo i_CostumerAndVehicleInfo,
            string i_Model,
            string i_LicensePlate,
            eEnergySourceType i_EnergySourceType,
            float i_CurrentEnergy) : base(i_CostumerAndVehicleInfo, i_Model, i_LicensePlate)
        {
            base.Type = VehicleFactory.eVehicleType.Car;
            base.NumOfWheels = k_CarNumOfWheels;
            base.MaxWheelAirPressure = k_CarMaxWheelAirPressure;
            base.EnergySourceType = i_EnergySourceType;
            base.EnergyMaxCapacity = (float)(i_EnergySourceType == eEnergySourceType.Electric ? k_ElectricCarMaxEnergyCapacity : k_FuelCarMaxEnergyCapacity);
            base.FuelType = i_EnergySourceType == eEnergySourceType.Electric ? k_ElectricCarFuelType : k_FuelCarFuelType;
            base.CurrentEnergyCapacity = i_CurrentEnergy;
            base.EnergyPercentage = (CurrentEnergyCapacity / EnergyMaxCapacity) * 100;
        }
        
        public override string ToString()
        {
            StringBuilder carDetails = new StringBuilder();

            carDetails.AppendLine(string.Format("License Plate: {0}", LicensePlate));
            carDetails.AppendLine(string.Format("Model: {0}", Model));
            carDetails.AppendLine(string.Format("Owner Name: {0}", m_CostumerInfo.CustomerName));
            carDetails.AppendLine(string.Format("Owner Phone Number: {0}", m_CostumerInfo.CustomerPhoneNumber));
            carDetails.AppendLine(string.Format("State In Garage: {0}", VehicleState));
            carDetails.AppendLine(string.Format("Car Color: {0}", m_Color));
            carDetails.AppendLine(string.Format("Number of Doors: {0}", m_DoorsNum));
            carDetails.AppendLine(string.Format("Energy Source Type: {0}", EnergySourceType));
            carDetails.AppendLine(string.Format("Current Energy Capacity: {0}", CurrentEnergyCapacity));
            carDetails.AppendLine(string.Format("Energy Percentage: {0}%", EnergyPercentage));
            carDetails.AppendLine(string.Format("Number of Wheels: {0}", NumOfWheels));
            carDetails.AppendLine(string.Format("Max Wheel Air Pressure: {0}", MaxWheelAirPressure));

            for (int i = 0; i < m_Wheels.Length; i++)
            {
                carDetails.AppendLine(string.Format("Wheel {0}# : ", i + 1, m_Wheels[i].ToString()));
            }

            return carDetails.ToString();
        }

        public override void SetAddedFields(Dictionary<string, string> i_AddedFields)
        {
            //todo - add check is defined
            m_Color = (eCarColor)Enum.Parse( typeof(eCarColor), i_AddedFields["CarColor"]);
            m_DoorsNum = (eCarDoorsNum)Enum.Parse(typeof(eCarDoorsNum), i_AddedFields["CarDoorsNum"]);
        }

        public override List<string> GetAddedFields()
        {
            List<string> addedFields = new List<string>();
            addedFields.Add("CarColor");
            addedFields.Add("CarDoorsNum");
            return addedFields;
        }
    }
}
