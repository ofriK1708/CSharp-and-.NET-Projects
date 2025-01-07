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
        protected float EnergyPercentage { get; set; }
        protected float MaxWheelAirPressure { get; set; }
        protected int NumOfWheels { get; set; }
        protected Wheel[] m_Wheels;
        protected eEnergySourceType EnergySourceType { get; set; }
        protected float EnergyMaxCapacity { get; set; }
        protected float m_CurrentEnergyCapacity;

        internal Vehicle(CustomerInfo i_CostumerInfo, string i_Model, string i_LicensePlate)
        {
            m_CostumerInfo = i_CostumerInfo;
            Model = i_Model;
            LicensePlate = i_LicensePlate;
        }

        internal float CurrentEnergyCapacity
        {
            get { return m_CurrentEnergyCapacity; }
            set
            {
                ValidateCurrentEnergyBelowMax(value, EnergyMaxCapacity);
                m_CurrentEnergyCapacity = value;
            }
        }

        public Wheel[] Wheels
        {
            get
            {
                return m_Wheels;
            }
            set
            {
                ValidateWheels(value);
                m_Wheels = value;
            }
        }
        
        public abstract void SetAddedFields(Dictionary<string, string> i_AddedFields);

        public abstract List<string> GetAddedFields();

        protected eFuelType FuelType { set; get; }

        public override bool Equals(object obj)
        {
            bool equals = false;
            Vehicle vehicleToCheck = obj as Vehicle;

            if (vehicleToCheck != null)
            {
                equals = LicensePlate == vehicleToCheck.LicensePlate;
            }

            return equals;
        }

        public override int GetHashCode()
        {
            return m_CostumerInfo.GetHashCode();
        }

        internal void ChangeVehicleState(eVehicleState i_NewState)
        {
            VehicleState = i_NewState;
        }

        public void FillEnergyToFull()
        {
            m_CurrentEnergyCapacity = EnergyMaxCapacity;
        }

        protected void ValidateCurrentEnergyBelowMax(float i_CurrentEnergy, float i_MaxEnergy)
        {
            if (i_CurrentEnergy > i_MaxEnergy || i_CurrentEnergy < 0)
            {
                throw new ValueOutOfRangeException(0, i_MaxEnergy,
                    string.Format("{0} is out of the valid range",
                        EnergySourceType == eEnergySourceType.Fuel ? "Fuel amount" : "battery charge"));
            }
        }

        protected void ValidateWheels(Wheel[] i_Wheels)
        {
            if (i_Wheels.Length != NumOfWheels)
            {
                throw new ArgumentException(string.Format(
                    "Invalid number of wheels for {0}. Expected {1}, but got {2})", Type, NumOfWheels, Wheels.Length));
            }

            foreach (Wheel wheel in i_Wheels)
            {
                wheel.ValidateWheel();
            }
        }

        public void FillWheelsAirToMax()
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.fillAirPressureToMax();
            }
        }

        public void FillEnergy(float i_AmountToFill, eFuelType i_FuelType)
        {
            if (i_FuelType != FuelType)
            {
                throw new ArgumentException(string.Format("Invalid fuel type, got {0} , expected {1}",
                    i_FuelType.ToString(), EnergySourceType.ToString()));
            }

            if (i_AmountToFill + CurrentEnergyCapacity > EnergyMaxCapacity)
            {
                throw new ValueOutOfRangeException(0, EnergyMaxCapacity - CurrentEnergyCapacity,
                    "Amount energy to fill out of range");
            }

            CurrentEnergyCapacity += i_AmountToFill;
            EnergyPercentage = (CurrentEnergyCapacity / EnergyMaxCapacity) * 100;
        }
    }
}