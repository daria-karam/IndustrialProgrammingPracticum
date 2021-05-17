using Lab3sharp.TransportTypes;
using Lab3sharp.Enums;
using Lab3sharp.FactoryMethod;
using Lab3sharp.Visitor;

namespace Lab3sharp
{
    class Order
    {
        private double cost;
        private int volume;
        private string startPoint;
        private string finishPoint;
        private Type type;
        private Track track;

        public Order()
        {
            cost = 0;
            volume = 0;
            startPoint = "";
            finishPoint = "";
            track = new Track();
            type = Type.Economy;
        }

        public Order(Type deliv, string startP, string finishP, int vol)
        {
            startPoint = startP;
            finishPoint = finishP;
            type = deliv;
            track = Best(startP, finishP, deliv, vol);
            cost = track.Cost;
            volume = vol;
        }

        public int Decr(string str)
        {
            GlobalData data = GlobalData.GetInstance();
            foreach (var it in data.towns)
            {
                if (str.Equals(it.Key))
                    return it.Value;
            }
            return -1;
        }

        public int[][] MatrixUpd(Type type)
        {
            GlobalData data = GlobalData.GetInstance();
            int[][] matrix = new int[data.SIZE][];
            for (int i = 0; i < data.SIZE; i++)
            {
                matrix[i] = new int[data.SIZE];
                for (int j = 0; j < data.SIZE; j++)
                {
                    matrix[i][j] = data.matrixDist[i, j];
                }
            }
            switch (type)
            {
                case Type.Economy:
                    for (int i = 0; i < data.SIZE; i++)
                    {
                        if (data.points[i].Contains("TS"))
                            for (int j = 0; j < data.SIZE; j++)
                                matrix[i][j] = 99999;
                    }
                    break;
                case Type.Standart:
                    for (int i = 0; i < data.SIZE; i++)
                    {
                        if (data.points[i].Contains("AP"))
                            for (int j = 0; j < data.SIZE; j++)
                                matrix[i][j] = 99999;
                    }
                    break;
                case Type.Turbo:
                    for (int i = 0; i < data.SIZE; i++)
                    {
                        for (int j = 0; j < data.SIZE; j++)
                            if (matrix[i][j] == 0)
                                matrix[i][j] = 99999;
                    }
                    break;
            }
            return matrix;
        }

        public int[] Optim(int[][] arr, int beginPoint, int endPoint)
        {
            GlobalData data = GlobalData.GetInstance();
            int[] d = new int[data.SIZE];
            int[] v = new int[data.SIZE];
            int temp, minindex, min;
            int begin_index = beginPoint;

            for (int i = 0; i < data.SIZE; i++)
            {
                d[i] = 99999;
                v[i] = 1;
            }
            d[begin_index] = 0;

            do
            {
                minindex = 99999;
                min = 99999;
                for (int i = 0; i < data.SIZE; i++)
                {
                    if ((v[i] == 1) && (d[i] < min))
                    {
                        min = d[i];
                        minindex = i;
                    }
                }

                if (minindex != 99999)
                {
                    for (int i = 0; i < data.SIZE; i++)
                    {
                        if (arr[minindex][i] > 0)
                        {
                            temp = min + arr[minindex][i];
                            if (temp < d[i])
                            {
                                d[i] = temp;
                            }
                        }
                    }
                    v[minindex] = 0;
                }
            } while (minindex < 99999);

            int[] ver = new int[data.SIZE];
            int end = endPoint;
            ver[0] = end;
            int k = 1;
            int weight = d[end];

            while (end != begin_index)
            {
                for (int i = 0; i < data.SIZE; i++)
                    if (arr[end][i] != 0)
                    {
                        temp = weight - arr[end][i];
                        if (temp == d[i])
                        {
                            weight = temp;
                            end = i;
                            ver[k] = i + 1;
                            k++;
                        }
                    }
            }

            for (int i = 0; i < k / 2; i++)
            {
                temp = ver[i];
                ver[i] = ver[k - 1 - i];
                ver[k - 1 - i] = temp;
            }

            return ver;
        }

        public Track Best(string startP, string finishP, Type deliv, int volume)
        {
            GlobalData data = GlobalData.GetInstance();
            int start = Decr(startP);
            int finish = Decr(finishP);

            int[][] mat = new int[data.SIZE][];
            for (int i = 0; i < data.SIZE; i++)
            {
                mat[i] = new int[data.SIZE];
            }

            mat = MatrixUpd(deliv);
            int[] path = new int[data.SIZE];
            for (int i = 0; i < data.SIZE; i++)
            {
                path[i] = -1;
            }

            path = Optim(mat, start, finish);
            int count = 0;
            for (int i = 0; i < data.SIZE; i++)
            {
                if (path[i] != -1)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            CarCreator carCreator = CarCreator.GetInstance();
            TrainCreator trainCreator = TrainCreator.GetInstance();
            PlaneCreator planeCreator = PlaneCreator.GetInstance();

            switch (count)
            {
                case 1:
                    {
                        Car car1 = carCreator.CreateTransport() as Car;
                        foreach (var it in data.table)
                        {
                            if (it.Key.Contains(data.points[path[0]]))
                            {
                                car1 = carCreator.CreateTransport(
                                    data.tableCost[it.Value * 3 + 2],
                                    data.matrixDist[path[0], path[1]])
                                    as Car;
                            }
                        }

                        Track track = new Track();
                        track.AddTransport(volume, car1);
                        return track;
                    }
                case 3:
                    {
                        Car car1 = carCreator.CreateTransport() as Car;
                        foreach (var it in data.table)
                        {
                            if (it.Key.Contains(data.points[path[0]]))
                            {
                                car1 = carCreator.CreateTransport(
                                    data.tableCost[it.Value * 3 + 2],
                                    data.matrixDist[path[0], path[1]])
                                    as Car;
                            }
                        }

                        Car car2 = carCreator.CreateTransport() as Car;
                        foreach (var it in data.table)
                        {
                            if (it.Key.Contains(data.points[path[2]]))
                            {
                                car2 = carCreator.CreateTransport(
                                    data.tableCost[it.Value * 3 + 2],
                                    data.matrixDist[path[2], path[3]])
                                    as Car;
                            }
                        }

                        Train train1 = trainCreator.CreateTransport() as Train;
                        Plane plane1 = planeCreator.CreateTransport() as Plane;
                        Track track = new Track();

                        foreach (var it in data.table)
                        {
                            if (it.Key.Contains(data.points[path[1]])
                                && data.points[path[1]].Contains("TS"))
                            {
                                train1 = trainCreator.CreateTransport(
                                    data.tableCost[it.Value * 3 + 1],
                                    data.matrixDist[path[1], path[2]])
                                    as Train;
                                track.AddTransport(volume, car1, car2, train1);
                            }

                            if (it.Key.Contains(data.points[path[1]])
                                && data.points[path[1]].Contains("AP"))
                            {
                                plane1 = planeCreator.CreateTransport(
                                    data.tableCost[it.Value * 3],
                                    data.matrixDist[path[1], path[2]])
                                    as Plane;
                                track.AddTransport(volume, car1, car2, plane1);
                            }
                        }

                        return track;
                    }
                case 5:
                    {
                        Car car1 = carCreator.CreateTransport() as Car;
                        foreach (var it in data.table)
                        {
                            if (it.Key.Contains(data.points[path[0]]))
                            {
                                car1 = carCreator.CreateTransport(
                                    data.tableCost[it.Value * 3 + 2],
                                    data.matrixDist[path[0], path[1]])
                                    as Car;
                            }
                        }

                        Train train1 = trainCreator.CreateTransport() as Train;
                        Plane plane1 = planeCreator.CreateTransport() as Plane;
                        foreach (var it in data.table)
                        {
                            if (it.Key.Contains(data.points[path[1]])
                                && data.points[path[1]].Contains("TS"))
                            {
                                train1 = trainCreator.CreateTransport(
                                    data.tableCost[it.Value * 3 + 1],
                                    data.matrixDist[path[1], path[2]])
                                    as Train;
                            }
                            if (it.Key.Contains(data.points[path[1]])
                                && data.points[path[1]].Contains("AP"))
                            {
                                plane1 = planeCreator.CreateTransport(
                                    data.tableCost[it.Value * 3],
                                    data.matrixDist[path[1], path[2]])
                                    as Plane;
                            }
                        }

                        Car car2 = carCreator.CreateTransport() as Car;
                        foreach (var it in data.table)
                        {
                            if (it.Key.Contains(data.points[path[2]]))
                            {
                                car2 = carCreator.CreateTransport(
                                    data.tableCost[it.Value * 3 + 2],
                                    data.matrixDist[path[2], path[3]])
                                    as Car;
                            }
                        }

                        foreach (var it in data.table)
                        {
                            if (it.Key.Contains(data.points[path[3]])
                                && data.points[path[3]].Contains("AP"))
                            {
                                plane1 = planeCreator.CreateTransport(
                                    data.tableCost[it.Value * 3],
                                    data.matrixDist[path[3], path[4]])
                                    as Plane;
                            }
                            if (it.Key.Contains(data.points[path[3]])
                                && data.points[path[3]].Contains("TS"))
                            {
                                train1 = trainCreator.CreateTransport(
                                    data.tableCost[it.Value * 3 + 1],
                                    data.matrixDist[path[3], path[4]])
                                    as Train;
                            }
                        }

                        Car car3 = carCreator.CreateTransport() as Car;
                        foreach (var it in data.table)
                        {
                            if (it.Key.Contains(data.points[path[4]]))
                            {
                                car3 = carCreator.CreateTransport(
                                    data.tableCost[it.Value * 3 + 2],
                                    data.matrixDist[path[4], path[5]])
                                    as Car;
                            }
                        }

                        Track track = new Track();
                        track.AddTransport(volume, car1, car2, car3, train1, plane1);
                        return track;
                    }

                case 7:
                    {
                        Car car1 = carCreator.CreateTransport() as Car;
                        foreach (var it in data.table)
                        {
                            if (it.Key.Contains(data.points[path[0]]))
                            {
                                car1 = carCreator.CreateTransport(
                                    data.tableCost[it.Value * 3 + 2],
                                    data.matrixDist[path[0], path[1]])
                                    as Car;
                            }
                        }

                        Train train1 = trainCreator.CreateTransport() as Train;
                        foreach (var it in data.table)
                        {
                            if (it.Key.Contains(data.points[path[1]]))
                            {
                                train1 = trainCreator.CreateTransport(
                                    data.tableCost[it.Value * 3 + 1],
                                    data.matrixDist[path[1], path[2]])
                                    as Train;
                            }
                        }

                        Car car2 = carCreator.CreateTransport() as Car;
                        foreach (var it in data.table)
                        {
                            if (it.Key.Contains(data.points[path[2]]))
                            {
                                car2 = carCreator.CreateTransport(
                                    data.tableCost[it.Value * 3 + 2],
                                    data.matrixDist[path[2], path[3]])
                                    as Car;
                            }
                        }

                        Plane plane1 = planeCreator.CreateTransport() as Plane;
                        foreach (var it in data.table)
                        {
                            if (it.Key.Contains(data.points[path[3]]))
                            {
                                plane1 = planeCreator.CreateTransport(
                                    data.tableCost[it.Value * 3],
                                    data.matrixDist[path[3], path[4]])
                                    as Plane;
                            }
                        }

                        Car car3 = carCreator.CreateTransport() as Car;
                        foreach (var it in data.table)
                        {
                            if (it.Key.Contains(data.points[path[4]]))
                            {
                                car3 = carCreator.CreateTransport(
                                    data.tableCost[it.Value * 3 + 2],
                                    data.matrixDist[path[4], path[5]])
                                    as Car;
                            }
                        }

                        Train train2 = trainCreator.CreateTransport() as Train;
                        foreach (var it in data.table)
                        {
                            if (it.Key.Contains(data.points[path[5]]))
                            {
                                train2 = trainCreator.CreateTransport(
                                    data.tableCost[it.Value * 3 + 1],
                                    data.matrixDist[path[5], path[6]])
                                    as Train;
                            }
                        }

                        Car car4 = carCreator.CreateTransport() as Car;
                        foreach (var it in data.table)
                        {
                            if (it.Key.Contains(data.points[path[6]]))
                            {
                                car4 = carCreator.CreateTransport(
                                    data.tableCost[it.Value * 3 + 2],
                                    data.matrixDist[path[6], path[7]])
                                    as Car;
                            }
                        }

                        Track track = new Track();
                        track.AddTransport(volume, car1, car2, car3, car4, train1, train2, plane1);

                        return track;
                    }
                default:
                    {
                        return null;
                    }
            }
        }
    }
}
