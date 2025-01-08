namespace ex03
{
    public  class EnergySource
    {
        public float MaxEnergyCapacity { get; protected set; }
        public float CurrentEnergyCapacity { get; set; }

        public float EnergyPrecentage
        {
            get
            {
                return (CurrentEnergyCapacity / MaxEnergyCapacity) * 100;
            }
            protected set
            {
                EnergyPrecentage = value;
            }
        }
    }
}
