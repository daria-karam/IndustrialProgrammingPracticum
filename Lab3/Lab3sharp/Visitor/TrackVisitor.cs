using Lab3sharp.TransportTypes;

namespace Lab3sharp.Visitor
{
    //You can use the pattern Visitor to add functionality to a class without changing it.
    //In the original pattern, the extensible class is passed as a parameter (without 'this'),
    //but in C# this approach seems more convenient to me.
    static class TrackVisitor
    {
        public static void AddCar(this Track track, Car car, int volume)
        {
            track.Cost += car.SumCost(volume, car.GetDistance());
            track.Time += car.SumTime(car.GetDistance());
        }

        public static void AddTrain(this Track track, Train train, int volume)
        {
            track.Cost += train.SumCost(volume, train.GetDistance());
            track.Time += train.SumTime(train.GetDistance());
        }

        public static void AddPlane(this Track track, Plane plane, int volume)
        {
            track.Cost += plane.SumCost(volume, plane.GetDistance());
            track.Time += plane.SumTime(plane.GetDistance());
        }

        public static void AddTransport(this Track track, int volume, params Transport[] transports)
        {
            foreach (Transport transport in transports)
            {
                switch (transport)
                {
                    case Car car:
                        track.AddCar(car, volume);
                        break;
                    case Train train:
                        track.AddTrain(train, volume);
                        break;
                    case Plane plane:
                        track.AddPlane(plane, volume);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
