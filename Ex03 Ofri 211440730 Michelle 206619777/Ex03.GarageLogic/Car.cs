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
        public const eFuelType k_FuelCarFuelType = eFuelType.Octan95;
        const string k_carColorField = "Car color";
        const string k_carDoorsNumField = "Car doors number";
        internal eCarColor m_Color;
        internal eCarDoorsNum m_DoorsNum;

        internal Car(CustomerInfo i_CostumerAndVehicleInfo,
            string i_Model,
            string i_LicensePlate,
            eEnergySourceType i_EnergySourceType,
            float i_CurrentEnergy) : base(i_CostumerAndVehicleInfo, i_Model, i_LicensePlate)
        {
            Type = VehicleFactory.eVehicleType.Car;
            NumOfWheels = k_CarNumOfWheels;
            MaxWheelAirPressure = k_CarMaxWheelAirPressure;
            if (i_EnergySourceType == eEnergySourceType.Battery)
            {
                EnergySource = new ElectricMotor(k_ElectricCarMaxEnergyCapacity, i_CurrentEnergy);
            }
            else
            {
                EnergySource = new GasEngine(k_FuelCarMaxEnergyCapacity, i_CurrentEnergy, k_FuelCarFuelType);
            }
        }
        
        public override string ToString()
        {
            StringBuilder carDetails = new StringBuilder();

            carDetails.AppendLine("Car Details: ");
            carDetails.AppendLine(base.ToString());
            carDetails.AppendLine(string.Format("Car Color: {0}", m_Color));
            carDetails.AppendLine(string.Format("Number of Doors: {0}", m_DoorsNum));

            return carDetails.ToString();
        }

        public override void SetAddedFields(Dictionary<string, string> i_AddedFields)
        {
            m_Color = (eCarColor)Enum.Parse( typeof(eCarColor), i_AddedFields[k_carColorField]);
            m_DoorsNum = (eCarDoorsNum)Enum.Parse(typeof(eCarDoorsNum), i_AddedFields[k_carDoorsNumField]);
        }

        public override Dictionary<string, Type> GetAddedFields()
        {
            Dictionary<string, Type> addedFields = new Dictionary<string, Type>();
            addedFields.Add(k_carColorField, typeof(eCarColor));
            addedFields.Add(k_carDoorsNumField, typeof(eCarDoorsNum));
            return addedFields;
        }
    }
}
