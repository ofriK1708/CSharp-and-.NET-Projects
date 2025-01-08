using System;
using System.Text;

namespace ex03
{
    internal class ElectricMotor : EnergySource
    {
        internal ElectricMotor(float i_MaxElectricityCapacity,float i_CurrentElectricityCapacity)
        {
            if(i_MaxElectricityCapacity <= 0)
            {
                throw new ArgumentException("Max Electric Capacity must be postive!");
            }
            
            MaxEnergyCapacity = i_MaxElectricityCapacity;
            
            if(i_CurrentElectricityCapacity > MaxEnergyCapacity || i_CurrentElectricityCapacity < 0)
            {
                throw new ValueOutOfRangeException(0, MaxEnergyCapacity, "Electricity Capacity is out of range");
            }

            CurrentEnergyCapacity = i_CurrentElectricityCapacity;
        }
        public void ChargeBattery(float i_MinutesToChrage)
        {
           if(i_MinutesToChrage + CurrentEnergyCapacity > MaxEnergyCapacity || i_MinutesToChrage < 0)
           {
               throw new ValueOutOfRangeException(0, MaxEnergyCapacity, "Electricity Capacity to add is out of range");
           }

           CurrentEnergyCapacity += i_MinutesToChrage;
           EnergyPrecentage = (CurrentEnergyCapacity / MaxEnergyCapacity) * 100;
        }
        public override string ToString()
        {
            StringBuilder energyMotorDetails = new StringBuilder();
            energyMotorDetails.AppendLine("Electric Motor");
            energyMotorDetails.AppendLine(string.Format("Current Electricity Capacity: {0}", CurrentEnergyCapacity));
            energyMotorDetails.AppendLine(string.Format("Max Electricity Capacity: {0}", MaxEnergyCapacity));
            energyMotorDetails.AppendLine(string.Format("Electricity Precentage: {0}", EnergyPrecentage));

            return energyMotorDetails.ToString();
        }
    }
}
