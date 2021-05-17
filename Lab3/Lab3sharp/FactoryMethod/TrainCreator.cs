using System;
using Lab3sharp.TransportTypes;

namespace Lab3sharp.FactoryMethod
{
    //The pattern Singleton is also used
    class TrainCreator : AbstractCreator
    {
        private static TrainCreator instance;

        private TrainCreator() { }

        public static TrainCreator GetInstance()
        {
            if (instance == null)
                instance = new TrainCreator();
            return instance;
        }

        public override Transport CreateTransport()
        {
            return new Train
            {
                Price = 200,
                Speed = 100,
                Volume = 500,
                Distance = 0
            };
        }

        public override Transport CreateTransport(Tuple<int, int, double> tuple, int dist)
        {
            return new Train
            {
                Volume = tuple.Item1,
                Speed = tuple.Item2,
                Price = tuple.Item3,
                Distance = dist
            };
        }
    }
}
