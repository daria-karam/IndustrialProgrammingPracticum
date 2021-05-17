namespace Lab3sharp
{
    class Track
    {
		private int volume;

		public double Cost { get; set; }
		public double Time { get; set; }

		public Track()
		{
			Cost = 0;
			Time = 0;
			volume = 0;
		}

		//Instead of the other constructors, the methods of the Visitor class are used.
	}
}
