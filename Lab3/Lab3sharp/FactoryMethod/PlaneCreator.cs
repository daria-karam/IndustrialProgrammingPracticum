using System;
using Lab3sharp.TransportTypes;

namespace Lab3sharp.FactoryMethod
{
    //The pattern Singleton is also used
    class PlaneCreator : AbstractCreator
    {
        private static PlaneCreator instance;

        private PlaneCreator() { }

        public static PlaneCreator GetInstance()
        {
            if (instance == null)
                instance = new PlaneCreator();
            return instance;
        }

        public override Transport CreateTransport()
        {
            return new Plane()
            {
                Price = 500,
                Speed = 300,
                Volume = 100,
                Distance = 0
            };
        }

        public override Transport CreateTransport(Tuple<int, int, double> tuple, int dist)
        {
            return new Plane
            {
                Volume = tuple.Item1,
                Speed = tuple.Item2,
                Price = tuple.Item3,
                Distance = dist
            };
        }
    }
}
