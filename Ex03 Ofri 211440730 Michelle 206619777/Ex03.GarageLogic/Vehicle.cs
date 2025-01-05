using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex03
{
    public abstract class Vehicle
    {
        public CostumerInfo m_CostumerAndVehcialInfo;
        public eVehicleState VehicleState { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        protected eVehicleType Type { set; get; }

        protected float m_EnergyPrecentage;
        protected float MaxWheelAirPressure { set; get; }
        protected int NumOfWheels { set; get; }
        protected Wheel[] m_Wheels;
        protected eEnergySourceType EnergySourceType { set; get; }
        protected float EnergyMaxCapacity { set; get; }
        protected float m_CurrentEnergyCapacity;
        protected eFuelType FuelType { set; get; }

        public Vehicle(CostumerInfo i_CostumerAndVehcialInfo,string i_Model,string i_LicensePlate)
        {
            m_CostumerAndVehcialInfo = i_CostumerAndVehcialInfo;
            Model = i_Model;
            LicensePlate = i_LicensePlate;
        }

        public override bool Equals(object obj)
        {
            bool equals = false;
            Vehicle vehicleToCheck = obj as Vehicle;

            if(vehicleToCheck != null) 
            {
                equals = LicensePlate == vehicleToCheck.LicensePlate;
            }
            
            return equals;
        }
        public override int GetHashCode()
        {
            return m_CostumerAndVehcialInfo.GetHashCode();
        }
        internal void ChangeVehicleState(eVehicleState i_NewState)
        {
            VehicleState = i_NewState;
        }
        public void FillEnergyToFull() 
        {
            m_CurrentEnergyCapacity = EnergyMaxCapacity;
        }
    }
    
}
