using System;
using Lab3sharp.TransportTypes;

namespace Lab3sharp.FactoryMethod
{
    //The pattern Singleton is also used
    class CarCreator : AbstractCreator
    {
        private static CarCreator instance;

        private CarCreator() { }

        public static CarCreator GetInstance()
        {
            if (instance == null)
                instance = new CarCreator();
            return instance;
        }

        public override Transport CreateTransport()
        {
            return new Car
            {
                Price = 100,
                Speed = 60,
                Volume = 50,
                Distance = 0
            };
        }

        public override Transport CreateTransport(Tuple<int, int, double> tuple, int dist)
        {
            return new Car
            {
                Volume = tuple.Item1,
                Speed = tuple.Item2,
                Price = tuple.Item3,
                Distance = dist
            };
        }
    }
}
