using System;
using System.Text;

namespace ex03
{
    internal class GasEngine : EnergySource
    {
        private readonly eFuelType r_FuelType;

        internal GasEngine(float i_MaxFuelCapacity, float i_CurrentFuelCapacity, eFuelType i_FuelType)
        {
            if (i_MaxFuelCapacity <= 0)
            {
                throw new ArgumentException("Max Fuel Capacity must be postive!");
            }
            MaxEnergyCapacity = i_MaxFuelCapacity;
            if (i_CurrentFuelCapacity > MaxEnergyCapacity || i_CurrentFuelCapacity < 0)
            {
                throw new ValueOutOfRangeException(0, MaxEnergyCapacity, "Fuel Capacity is out of range");
            }
            CurrentEnergyCapacity = i_CurrentFuelCapacity;
            r_FuelType = i_FuelType;
        }
        internal void FillGas(float i_AmountOfGasToAdd, eFuelType i_FuelType)
        {
            if (i_AmountOfGasToAdd + CurrentEnergyCapacity > MaxEnergyCapacity || i_AmountOfGasToAdd < 0)
            {
                throw new ValueOutOfRangeException(0, MaxEnergyCapacity, "Fuel Capacity to add is out of range");
            }
            if (i_FuelType != r_FuelType)
            {
                throw new ArgumentException(string.Format("Wrong fuel type, should be {0}, got {1} ",i_FuelType.ToString(),r_FuelType.ToString()));
            }
            CurrentEnergyCapacity += i_AmountOfGasToAdd;
            EnergyPrecentage = (CurrentEnergyCapacity / MaxEnergyCapacity) * 100;
        }
        public override string ToString()
        {
            StringBuilder energyMotorDetails = new StringBuilder();

            energyMotorDetails.AppendLine("Gas Engine");
            energyMotorDetails.AppendLine(string.Format("Current Fuel Capacity: {0}", CurrentEnergyCapacity));
            energyMotorDetails.AppendLine(string.Format("Max Fuel Capacity: {0}", MaxEnergyCapacity));
            energyMotorDetails.AppendLine(string.Format("Fuel Precentage: {0}", EnergyPrecentage));

            return energyMotorDetails.ToString();
        }
    }
}
