using System;

namespace ex03
{
    public abstract class Vehicle
    {
        public CustomerInfo m_CostumerInfo;
        public eVehicleState VehicleState { get; set; }
        public string Model { get; }
        public string LicensePlate { get; }
        protected eVehicleType Type { get; set; }

        protected float EnergyPrecentage { get; set; }
        protected float MaxWheelAirPressure { get; set; }
        protected int NumOfWheels { get; set; }
        protected Wheel[] m_Wheels;
        protected eEnergySourceType EnergySourceType { get; set; }
        protected float EnergyMaxCapacity { get; set; }
        protected float m_CurrentEnergyCapacity;
        internal float CurrentEnergyCapacity
        {
            get
            {
                return m_CurrentEnergyCapacity;
            }
            set
            {
                validateCurrentEnergyBelowMax(value, EnergyMaxCapacity);
                m_CurrentEnergyCapacity = value;
            }
        }


        protected eFuelType FuelType { set; get; }

        internal Vehicle(CustomerInfo i_CostumerAndVehcialInfo, string i_Model, string i_LicensePlate)
        {
            m_CostumerInfo = i_CostumerAndVehcialInfo;
            Model = i_Model;
            LicensePlate = i_LicensePlate;
        }

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
        protected void validateCurrentEnergyBelowMax(float i_CurrentEnergy, float i_MaxEnergy)
        {
            if (i_CurrentEnergy > i_MaxEnergy || i_CurrentEnergy < 0)
            {
                throw new ValueOutOfRangeException(0, i_MaxEnergy, string.Format("{0} is out of the valid range",EnergySourceType == eEnergySourceType.Fuel ? "Fuel amount" : "battery charge"));
            }
        }
        protected void validateWheels(Wheel[] i_Wheels, eVehicleType i_VehicleType, int i_ExpectedNumOfWheels)
        {
            if (i_Wheels.Length != i_ExpectedNumOfWheels)
            {
                throw new ArgumentException(string.Format("Invalid number of wheels for {0}. Expected {1}, but got {2})", i_VehicleType, i_ExpectedNumOfWheels, i_Wheels.Length));
            }

            foreach (Wheel wheel in i_Wheels)
            {
                wheel.ValidateWheel();
            }
        }
        public void changeVehicleState(eVehicleState newState)
        {
            VehicleState = newState;
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
                throw new ArgumentException(string.Format("Invalid fuel type, got {0} , expected {1}",i_FuelType.ToString(),EnergySourceType.ToString()));
            }
            else
            {
                if (i_AmountToFill + CurrentEnergyCapacity > EnergyMaxCapacity)
                {
                    throw new ValueOutOfRangeException(0, EnergyMaxCapacity - CurrentEnergyCapacity, "Amount energy to fill out of range");
                }
                else
                {
                    CurrentEnergyCapacity += i_AmountToFill;
                    EnergyPrecentage = (CurrentEnergyCapacity / EnergyMaxCapacity) * 100;
                }
            }
        }
    }
}
