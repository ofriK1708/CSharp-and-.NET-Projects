using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex03
{
    internal abstract class Vehicle
    {
        protected eVehicleType Type { set; get; }
        protected string m_Model;
        protected string m_LicensePlate;
        protected float m_EnergyPrecentage;
        protected float MaxWheelAirPressure { set; get; }
        protected int NumOfWheels { set; get; }
        protected Wheel[] m_Wheels;
        protected eEnergySourceType EnergySourceType { set; get; }
        protected float EnergyMaxCapacity { set; get; }
        protected float m_EnergyCurrentCapacity;
        protected eFuelType FuelType { set; get; }
    }
}
