using System;
using System.Collections.Generic;
using System.Text;

namespace ex03
{
    public abstract class Vehicle
    {
        public CustomerInfo m_CostumerInfo;
        public eVehicleState VehicleState { get; set; }
        public string Model { get; }
        public string LicensePlate { get; }
        protected VehicleFactory.eVehicleType Type { get; set; }
        public float MaxWheelAirPressure { get; set; }
        public int NumOfWheels { get; set; }
        public Wheel[] m_Wheels;
        protected EnergySource EnergySource { get; set; }

        internal Vehicle(CustomerInfo i_CostumerInfo, string i_Model, string i_LicensePlate)
        {
            m_CostumerInfo = i_CostumerInfo;
            Model = i_Model;
            LicensePlate = i_LicensePlate;
        }

        public Wheel[] Wheels
        {
            get
            {
                return m_Wheels;
            }
            set
            {
                ValidateNumberOfWheels(value);
                m_Wheels = value;
            }
        }
        
        public abstract void SetAddedFields(Dictionary<string, string> i_AddedFields);

        public abstract Dictionary<string, Type> GetAddedFields();

        protected eFuelType FuelType { set; get; }

        internal void ChangeVehicleState(eVehicleState i_NewState)
        {
            VehicleState = i_NewState;
        }

        protected void ValidateNumberOfWheels(Wheel[] i_Wheels)
        {
            if(i_Wheels.Length != NumOfWheels)
            {
                throw new ArgumentException(
                    string.Format(
                        "Invalid number of wheels for {0}. Expected {1}, but got {2})",
                        Type,
                        NumOfWheels,
                        Wheels.Length));
            }
        }

        public void FillWheelsAirToMax()
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.fillAirPressureToMax();
            }
        }

        public void ChargeBattery(float i_AmountOfMinutesToCharge)
        {
            if (EnergySource is ElectricMotor electricMotor)
            {
                electricMotor.ChargeBattery(i_AmountOfMinutesToCharge);
            }
            else
            {
                throw new ArgumentException("This vehicle is not electric powered");
            }
        }
        public void FillGas(float i_AmountOfGasToAdd, eFuelType i_FuelType)
        {
            if(EnergySource is GasEngine gasEngine)
            {
                gasEngine.FillGas(i_AmountOfGasToAdd, i_FuelType);
            }
            else
            {
                throw new ArgumentException("This vehicle is not gas powered");
            }
        }
        public override string ToString()
        {
            StringBuilder vehicleDetails = new StringBuilder();

            vehicleDetails.AppendLine(string.Format("License Plate: {0}", LicensePlate));
            vehicleDetails.AppendLine(string.Format("Model: {0}", Model));
            vehicleDetails.AppendLine(string.Format("Owner Name: {0}", m_CostumerInfo.CustomerName));
            vehicleDetails.AppendLine(string.Format("Owner Phone Number: {0}", m_CostumerInfo.CustomerPhoneNumber));
            vehicleDetails.AppendLine(string.Format("State In Garage: {0}", VehicleState));
            vehicleDetails.AppendLine(EnergySource.ToString());
            vehicleDetails.AppendLine(string.Format("Number of Wheels: {0}", NumOfWheels));
            for(int i = 0; i < m_Wheels.Length; i++)
            {
                vehicleDetails.AppendLine(string.Format("Wheel {0}#: {1}", i + 1, m_Wheels[i]));
            }

            return vehicleDetails.ToString();
        }
    }
}