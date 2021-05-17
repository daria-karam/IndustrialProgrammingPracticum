namespace Lab3sharp.TransportTypes
{
    class Transport
    {
		public int Speed { get; set; }
		public int Volume { get; set; }
		public double Price { get; set; }
		public int Distance { get; set; }

		public double SumCost(int mass, int dist)
		{
			return (mass / Volume) * SumTime(dist) * Price;
		}

		public double SumTime(int dist)
		{
			return dist / Speed;
		}

		public int GetDistance()
		{
			return Distance;
		}
	}
}
