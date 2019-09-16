namespace Work51
{
    class Computer
    {
        public int Cores { get; set; }
        public double Frequency { get; set; }
        public int Memory { get; set; }
        public int Hdd { get; set; }
        public override string ToString()
        {
            return $"Cores-{Cores} / Frequency-{Frequency} / Memory-{Memory} / Hdd-{Hdd}";
        }
    }
}
