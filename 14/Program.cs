using System.Text.Json.Nodes;

namespace Day14
{
  class Solution
  {
    int StartX = 500;
    int StartY = 0;

    public int Part1(string input)
    {
      var parsedInput = ParseInput(input);
      var count = 0;
      while (AddSand(parsedInput.caves, parsedInput.maxY, false)) {
        count++;
      }
      return count;
    }

    public int Part2(string input)
    {
      var parsedInput = ParseInput(input);
      var count = 0;
      while (AddSand(parsedInput.caves, parsedInput.maxY, true)) {
        count++;
      }
      return count;
    }

    private (HashSet<string> caves, int maxY) ParseInput(string input)
    {
      var caves = new HashSet<string>();
      var maxY = 0;
      foreach (var line in input.Split("\n"))
      {
        var points = line.Split(" -> ").Select((point) => point.Split(",").Select(int.Parse).ToArray()).ToArray();
        for (var i = 0; i < points.Count() - 1; ++i)
        {
          var x1 = Math.Min(points[i][0], points[i + 1][0]);
          var y1 = Math.Min(points[i][1], points[i + 1][1]);
          var x2 = Math.Max(points[i][0], points[i + 1][0]);
          var y2 = Math.Max(points[i][1], points[i + 1][1]);
          for (var x = x1; x <= x2; ++x)
          {
            for (var y = y1; y <= y2; ++y)
            {
              caves.Add($"{x},{y}");
              maxY = Math.Max(maxY, y);
            }
          }
        }
      }
      return (caves, maxY);
    }

    private bool AddSand(HashSet<string> caves, int maxY, bool hasFloor)
    {
      var x = StartX;
      var y = StartY;
      var floor = maxY + 1;

      while (true)
      {
        if (!hasFloor && y > maxY) // Overflowing
        {
          return false;
        }
        if (caves.Contains($"{x},{y}")) // Full
        {
          return false;
        }
        if (!caves.Contains($"{x},{y + 1}") && y < floor) // Down
        {
          y += 1;
        }
        else if (!caves.Contains($"{x - 1},{y + 1}") && y < floor) // Down + Left
        {
          x -= 1;
          y += 1;
        }
        else if (!caves.Contains($"{x + 1},{y + 1}") && y < floor) // Down + Right
        {
          x += 1;
          y += 1;
        }
        else
        {
          caves.Add($"{x},{y}");
          return true;
        }
      }
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      string input = File.ReadAllText(@".\input.txt");

      var solution = new Solution();
      Console.WriteLine($"Part1: {solution.Part1(input)}");
      Console.WriteLine($"Part2: {solution.Part2(input)}");
    }
  }
}
