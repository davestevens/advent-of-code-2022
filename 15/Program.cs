namespace Day15
{
  public record Point(long X, long Y);
  public record Sensor(Point Location, Point BeaconLocation, long Distance);

  class Solution
  {
    List<Sensor> Sensors;

    public Solution(string input)
    {
      Sensors = input.Split("\n").Select((line) => {
        var parts = line.Split(new [] { '=', ',', ':' });
        var sensorLocation = new Point(long.Parse(parts[1]), long.Parse(parts[3]));
        var beaconLocation = new Point(long.Parse(parts[5]), long.Parse(parts[7]));
        return new Sensor(
          sensorLocation,
          beaconLocation,
          Math.Abs(sensorLocation.X - beaconLocation.X) + Math.Abs(sensorLocation.Y - beaconLocation.Y)
        );
      })
      .ToList();
    }

    public int Part1(long y)
    {
      HashSet<Point> used = new HashSet<Point>();

      foreach (var sensor in Sensors)
      {
        var yDiff = Math.Abs(sensor.Location.Y - y);
        var xDelta = sensor.Distance - yDiff;
        var from = sensor.Location.X - xDelta;
        for (var x = sensor.Location.X - xDelta; x <= sensor.Location.X + xDelta; ++x)
        {
          if (x != sensor.BeaconLocation.X || y != sensor.BeaconLocation.Y)
          {
            used.Add(new Point(x, y));
          }
        }
      }

      return used.Count;
    }

    public long Part2(long min, long max)
    {
      for (long y = min; y <= max; ++y)
      {
        var x = FindGap(y, min, max);
        if (x > -1)
        {
          return (x * 4000000) + y;
        }
      }

      return -1;
    }

    private long FindGap(long y, long min, long max)
    {
      List<long[]> sensorSpans = new List<long[]>();
      foreach (var sensor in Sensors)
      {
        var yDiff = Math.Abs(sensor.Location.Y - y);
        var xDelta = sensor.Distance - yDiff;
        if (xDelta > 0)
        {
          sensorSpans.Add(new [] {
            Math.Max(min, sensor.Location.X - xDelta),
            Math.Min(max, sensor.Location.X + xDelta)
          });
        }
      }
      sensorSpans.Sort((a, b) => a[0].CompareTo(b[0]));

      long value = sensorSpans[0][1];
      for (var i = 1; i < sensorSpans.Count; ++i)
      {
        if (sensorSpans[i][0] > value)
        {
          return value + 1;
        }
        value = Math.Max(value, sensorSpans[i][1]);
      }

      return -1;
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      string input = File.ReadAllText(@".\input.txt");

      var isTestData = true;

      var solution = new Solution(input);            
      Console.WriteLine($"Part1: {solution.Part1(isTestData ? 10 : 2000000)}");
      Console.WriteLine($"Part2: {solution.Part2(0, isTestData ? 20 : 4000000)}");
    }
  }
}
