namespace ex03
{
    public abstract class EnergySource
    {
        public float MaxEnergyCapacity { get; protected set; }
        public float CurrentEnergyCapacity { get; set; }
        public float EnergyPrecentage { get; protected set; }
    }
}
