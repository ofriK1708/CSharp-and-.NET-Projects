using System;
using System.Collections;
using System.Collections.Generic;

namespace ex03
{
    public abstract class Vehicle
    {
        public CustomerInfo m_CostumerInfo;
        public eVehicleState VehicleState { get; set; }
        public string Model { get; }
        public string LicensePlate { get; }
        protected VehicleFactory.eVehicleType Type { get; set; }
        protected float MaxWheelAirPressure { get; set; }
        protected int NumOfWheels { get; set; }
        protected Wheel[] m_Wheels;
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

        public abstract List<string> GetAddedFields();

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
        public void fillGas(float i_AmountOfGasToAdd, eFuelType i_FuelType)
        {
            if(EnergySource is GasEngine gasEngine)
            {
                gasEngine.fillGas(i_AmountOfGasToAdd, i_FuelType);
            }
            else
            {
                throw new ArgumentException("This vehicle is not gas powered");
            }
        }
    }
}