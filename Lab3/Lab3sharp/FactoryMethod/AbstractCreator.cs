using System;
using Lab3sharp.TransportTypes;

namespace Lab3sharp
{
    //All inheritors will also use the pattern Singleton
    abstract class AbstractCreator
    {
        public abstract Transport CreateTransport();
        public abstract Transport CreateTransport(Tuple<int, int, double> tuple, int dist);
    }
}
