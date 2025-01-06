using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex03
{
    public abstract class Vehicle
    {
        public CustomerInfo m_CostumerInfo;
        public eVehicleState VehicleState { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }    
        protected eVehicleType Type { set; get; }

        protected float m_EnergyPrecentage;
        protected float? MaxWheelAirPressure { set; get; } = null;
        protected int NumOfWheels { set; get; }
        protected Wheel[] m_Wheels;
        protected eEnergySourceType EnergySourceType { set; get; }
        protected float? EnergyMaxCapacity { set; get; } = null;
        protected float m_CurrentEnergyCapacity;
        internal float CurrentEnergyCapacity
        {
            get
            {
                return m_CurrentEnergyCapacity;
            }
            set
            {
                validateCurrentEnergyBelowMax(value, EnergyMaxCapacity.Value);
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
            m_CurrentEnergyCapacity = EnergyMaxCapacity.Value;
        }
        protected void validateCurrentEnergyBelowMax(float i_CurrentEnergy,float i_MaxEnergy)
        {
            if (i_CurrentEnergy > i_MaxEnergy || i_CurrentEnergy < 0)
            {
                throw new ValueOutOfRangeException(0, i_MaxEnergy, "Current energy amount is out of the valid range");
            }
        }
        protected void validateWheels(Wheel[] i_Wheels, eVehicleType i_VehicleType, int i_ExpectedNumOfWheels)
        {
            if (i_Wheels.Length != i_ExpectedNumOfWheels)
            {
                throw new ArgumentException(string.Format("Invalid number of wheels for {0}. Expected {1}, but got {2}.)", i_VehicleType, i_ExpectedNumOfWheels, i_NumOfWheels));
            }
            
            foreach (Wheel wheel in i_Wheels)
            {
                wheel.ValidateWheel();
            }
        }
    }

}
